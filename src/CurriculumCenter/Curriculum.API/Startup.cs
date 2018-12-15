using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Curriculum.Application;
using Curriculum.Infrastruction;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Curriculum.Application.IntegrationEvent;
using Curriculum.Application.IntegrationEvent.Handlers;
using Curriculum.Application.Services;
using ResilienceHttpClient;
using HttpClient.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Curriculum.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddSingleton(typeof(ResilienceHttpClientFactory), sp => {

                var logger = sp.GetRequiredService<ILogger<ResilienceClient>>();
                var httpcontextaccessor = sp.GetRequiredService<IHttpContextAccessor>();
                return new ResilienceHttpClientFactory(5, 6, logger, httpcontextaccessor);

            });
            services.AddSingleton<IHttpClient>(sp =>
            {
                return sp.GetRequiredService<ResilienceHttpClientFactory>().CreateResilienceClient();
            });

            services.AddAuthentication("Bearer")
                         .AddIdentityServerAuthentication(options=> {
                             options.ApiName = "CurriculumCenter";
                             options.ApiSecret = "secret";
                             options.RequireHttpsMetadata = false;
                             options.Authority = "http://localhost:5000";
                         });

            services.AddDbContext<CurriculumContext>(options=> {
                options.UseSqlServer("server=./;database=EL_Curriculum;uid=sa;pwd=123", b => b.MigrationsAssembly("Curriculum.API"));
            });

            services.AddTransient(typeof(IIntegrationHandler<DispatchOrderIntegrationEvent>),typeof(DispatchOrderIntegrationEventHandler));
            services.AddTransient(typeof(IIntegrationHandler<StudentUpdateIntegrationEvent>), typeof(StudentUpdateIntegrationEventHandler));

            services.AddCap(Options => {
                Options.UseEntityFramework<CurriculumContext>();
                Options.UseRabbitMQ(O=> {
                    O.HostName = "47.99.221.32";
                    O.UserName = "yubin";
                    O.Password = "yubin0416";
                });
            });

            services.AddAutoMapper();
            services.AddMvc();

            var Container = new ContainerBuilder();
            Container.Populate(services);
            Container.RegisterModule(new ApplicationMoudle());
            return new AutofacServiceProvider(Container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
