using System.Text;
using CoreLibrary.Configuration;
using FeatureLibrary.Repositories;
using FeatureLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FeatureLibrary.Extensions
{
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureFeatureServices(this IServiceCollection services)
        {
            services.AddScoped<ICodingSkillRepository, CodingSkillRepository>();
            services.AddScoped<ICodingSkillService, CodingSkillService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }

        /// <summary>
        /// Configures JWT authentication.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureJWTAuthentication(this IServiceCollection services, JWTConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
