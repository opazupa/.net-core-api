using System;
using API.Extensions;
using API.GraphQL.Extensions;
using API.Models;
using AutoMapper;
using CoreLibrary.Configuration;
using CoreLibrary.Extensions;
using FeatureLibrary.Extensions;
using FeatureLibrary.Models;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private readonly DatabaseConfiguration _databaseConfiguration;
        private readonly JWTConfiguration _jwtConfiguration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
            _databaseConfiguration = Configuration.GetSection(nameof(DatabaseConfiguration)).Get<DatabaseConfiguration>();
            _jwtConfiguration = Configuration.GetSection(nameof(JWTConfiguration)).Get<JWTConfiguration>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));
            services.ConfigureCors();
            services.AddSwaggerDoc();

            services.Configure<JWTConfiguration>(Configuration.GetSection(nameof(JWTConfiguration)));
            services.ConfigureJWTAuthentication(_jwtConfiguration);

            // Configure database and persistence
            services.ConfigureDatabase<FeatureContext>(_databaseConfiguration);

            // Add feature module services.
            services.ConfigureFeatureServices();

            // Configure GraphQL
            services.ConfigureGraphQL(debugMode: !_env.IsProduction());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.EnvironmentName.Equals("Testing"))
            {
                app.ConfigureDevelopmentEnvironment(_databaseConfiguration);
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                throw new NotImplementedException("Production configuration not yet supported.");
            }

            app.ConfigureMiddlewares();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebSockets();
            app.UseGraphQL("/graphql");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
