using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Middlewares;
using FeatureLibrary.Database;
using FeatureLibrary.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.ConfigureCors();
            services.ConfigureSwagger();
            services.ConfigureDatabase(Configuration.GetSection("Database").Get<DatabaseConfiguration>());

            // Add feature module services.
            services.ConfigureFeatureServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.ConfigureSwagger();

                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<FeatureContext>();
                    // Seed the database.
                    context.Database.EnsureCreated();
                }
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
