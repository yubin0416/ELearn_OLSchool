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
using OrderCenter.Application;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderCenter.Infrastructure;
using OrderCenter.Application.Integration;
using OrderCenter.Application.Integration.Handlers;
using OrderCenter.Application.Integration.Events;

namespace OrderCenter.API
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
            services.AddAuthentication("Bearer")
                         .AddIdentityServerAuthentication(options => {
                             options.ApiName = "OrderCenter";
                             options.ApiSecret = "secret";
                             options.RequireHttpsMetadata = false;
                             options.Authority = "http://localhost:5000";
                         });

            services.AddDbContext<OrderContext>(options=> {
                options.UseSqlServer("server=./;database=EL_Order;uid=sa;pwd=123", s => s.MigrationsAssembly("OrderCenter.API"));
            });

            services.AddTransient(typeof(IIntegrationHandler<PaidOrderIntegrationEvent>),typeof(PaidOrderIntegrationEventHandler));

            services.AddCap(Options => {
                Options.UseEntityFramework<OrderContext>();
                Options.UseRabbitMQ(O => {
                    O.HostName = "47.99.221.32";
                    O.UserName = "yubin";
                    O.Password = "yubin0416";
                });
            });

            services.AddMvc();
            services.AddAutoMapper();
            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new ApplicationMoudel());
            return new AutofacServiceProvider(container.Build());
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
