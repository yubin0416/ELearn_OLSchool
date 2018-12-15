using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.API.GrantValidator;
using IdentityServer.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using HttpClient.Abstraction;
using ResilienceHttpClient;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Consul;
using IdentityServer.API.Dtos;
using Microsoft.Extensions.Options;
using DnsClient;
using System.Net;

namespace IdentityServer.API
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
            services.AddOptions();
            services.Configure<DiscoveryServicesOptions>(Configuration.GetSection("DiscoveryServices"));

            services.AddSingleton<IConsulClient>(sp=> {
                var servicesOptions = sp.GetRequiredService<IOptions<DiscoveryServicesOptions>>().Value;
                return new ConsulClient(config=> {
                    config.Address = new Uri(servicesOptions.Consul.HttpEndpoint);
                });
            });

            services.AddSingleton<IDnsQuery>(sp=> {
                var servicesOptions = sp.GetRequiredService<IOptions<DiscoveryServicesOptions>>().Value;
                return new LookupClient(IPAddress.Parse(servicesOptions.Consul.DnsEndpoint.Address), servicesOptions.Consul.DnsEndpoint.Port);
            });

            services.AddTransient<IUserService,UserService>();

            services.AddIdentityServer()
                         .AddDeveloperSigningCredential()
                         .AddConfigurationStore(option=> {
                             option.ConfigureDbContext = builder => {
                                 builder.UseSqlServer("server=./;database=El_IdentityServer;uid=sa;pwd=123", b => b.MigrationsAssembly("IdentityServer.API"));
                             };
                         })
                        .AddOperationalStore(options=> {
                            options.ConfigureDbContext = builder =>
                            {
                                builder.UseSqlServer("server=./;database=El_IdentityServer;uid=sa;pwd=123", b => b.MigrationsAssembly("IdentityServer.API"));
                            };
                            options.EnableTokenCleanup = true;
                            options.TokenCleanupInterval = 30;
                        })
                         .AddExtensionGrantValidator<Stu_Sms_CodeGrantValidator>()
                         .AddExtensionGrantValidator<Tc_Sms_CodeGrantValidator>()
                         .AddProfileService<ProfileService>();

            services.AddMvc();

            services.AddSingleton(typeof(ResilienceHttpClientFactory),sp=> {

                    var logger = sp.GetRequiredService<ILogger<ResilienceClient>>();
                    var httpcontextaccessor = sp.GetRequiredService<IHttpContextAccessor>();
                    return new ResilienceHttpClientFactory(5, 6, logger, httpcontextaccessor);
                
            });

            services.AddSingleton<IHttpClient>(sp =>
            {
                    return sp.GetRequiredService<ResilienceHttpClientFactory>().CreateResilienceClient();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, IConsulClient _ConsulClient)
        {
            InitializeSeed.InitializeDatabase(app);
            lifetime.ApplicationStarted.Register(()=>ConsulRegister(_ConsulClient));
            lifetime.ApplicationStopping.Register(()=>ConsulRegister(_ConsulClient));

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConsulRegister(IConsulClient client)
        {
            var healthcheck = new AgentCheckRegistration()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                HTTP = "http://localhost:5000/healthcheck",
                Interval =TimeSpan.FromSeconds(30)
            };

            var agent = new AgentServiceRegistration()
            {
                ID ="IdentityServer1",
                Name="IdentityServer",
                Address="127.0.0.1",
                Port =5000,
                Check = healthcheck
            };

            client.Agent.ServiceRegister(agent).ConfigureAwait(false);
        }

        private void ConsulDeregister(IConsulClient client)
        {
            client.Agent.ServiceDeregister("IdentityServer1").ConfigureAwait(false);
        }
    }
}
