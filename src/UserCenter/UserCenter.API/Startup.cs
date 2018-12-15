using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserCenter.API.Data;
using UserCenter.API.Filter;
using UserCenter.API.Services;
using IdentityServer4.AccessTokenValidation;
using DotNetCore.CAP;
using Consul;

namespace UserCenter.API
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
            services.AddDbContext<UserContext>(Options=> {
                Options.UseSqlServer("server=./;database=El_UserCenter;uid=sa;pwd=123");
            });
            services.AddTransient<ISmsCodeValidatorService, SmsCodeValidatorService>();

            ///添加Cap
            services.AddCap(options=> {
                options.UseEntityFramework<UserContext>();
                options.UseRabbitMQ(O=> {
                    O.HostName = "47.99.221.32";
                    O.UserName = "yubin";
                    O.Password = "yubin0416";
                });
            });

            services.AddMvc(Options=> {
                Options.Filters.Add(typeof(GlobalExecptionFilter));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IApplicationLifetime lifetime)
        {
            lifetime.ApplicationStarted.Register(ConsulRegister);
            lifetime.ApplicationStopping.Register(ConsulLogout);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        /// <summary>
        /// 注册服务到consul
        /// </summary>
        private void ConsulRegister()
        {
            var client = new ConsulClient(ConfiguationConsul);
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                Interval = TimeSpan.FromSeconds(30),
                HTTP = "http://localhost:5001/HealthCheck"
            };

            var agentReg = new AgentServiceRegistration()
            {
                ID = "UserCenter1",
                Check = httpCheck,
                Address = "127.0.0.1",
                Name = "UserCenter",
                Port = 5001
            };

            client.Agent.ServiceRegister(agentReg).ConfigureAwait(false);
        }

        /// <summary>
        /// 取消服务
        /// </summary>
        private void ConsulLogout()
        {
            var client = new ConsulClient(ConfiguationConsul);
            client.Agent.ServiceDeregister("UserCenter1").ConfigureAwait(false);
        }

        private void ConfiguationConsul(ConsulClientConfiguration client)
        {
            client.Address = new Uri("http://127.0.0.1:8500");
        }
    }
}
