using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddIdentityServer(opt =>
            {
                //opt.Discovery.ShowApiScopes = false;
                //opt.Discovery.ShowClaims = false;
                //opt.Discovery.ShowEndpoints = false;
                //opt.Discovery.ShowExtensionGrantTypes = false;
                //opt.Discovery.ShowGrantTypes = false;
                //opt.Discovery.ShowIdentityScopes = false;
                //opt.Discovery.ShowKeySet = false;
                //opt.Discovery.ShowResponseModes = false;
                //opt.Discovery.ShowResponseTypes = false;
                //opt.Discovery.ShowTokenEndpointAuthenticationMethods = false;
            })
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Id4Config.GetIdentityResources())
                .AddInMemoryApiResources(Id4Config.GetApiResources())
                .AddInMemoryClients(Id4Config.GetClients())
                .AddTestUsers(Id4Config.GetUsers());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
