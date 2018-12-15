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
using Payment.API.Data;
using Microsoft.EntityFrameworkCore;
using Payment.API.Integration.Handlers;
using Payment.API.Integration.Events;

namespace Payment.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                         .AddIdentityServerAuthentication(options => {
                             options.ApiName = "Payment";
                             options.ApiSecret = "secret";
                             options.RequireHttpsMetadata = false;
                             options.Authority = "http://localhost:5000";
                         });

            services.AddMvc();
            services.AddDbContext<PaymentContext>(options=> {
                options.UseSqlServer("server=./;database=El_Payment;uid=sa;pwd=123");
            });

            services.AddTransient(typeof(IIntegrationEventHandler<CreateOrderIntegrationEvent>), typeof(CreateOrderIntegrationEventHandler));
            services.AddCap(options=> {
                options.UseEntityFramework<PaymentContext>();
                options.UseRabbitMQ(O => {
                    O.HostName = "47.99.221.32";
                    O.UserName = "yubin";
                    O.Password = "yubin0416";
                });
            });
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
