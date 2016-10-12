using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResourceBasedAuthorization.Api.Authorization.Handlers;
using ResourceBasedAuthorization.Models.Interfaces;
using ResourceBasedAuthorization.Repositories.PostAggregate;

namespace ResourceBasedAuthorization.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //adds the authN handler to the services DI container
            services.AddTransient<IAuthorizationHandler, PostAuthorizationHandler>();

            services.AddTransient<IPostRepository, PostRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            
            //you might want to add some sort of middleware that populates the principle
            //a common one to use for JWT validation is the JwtBearerAuthentication M/W


            app.UseMvc();
        }
    }
}
