using System.Text;
using System.Threading.Tasks;
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


                // Sending the access token in the query string is required due to
                // a limitation in Browser APIs. We restrict it to only calls to the
                // SignalR hub in this code.
                // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
                // for more information about security considerations when using
                // the query string to transmit the access token.
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        const string AUTH = "Authorization";
                        // Read the token out of the headers / query / message
                        if (context.Request.Path.StartsWithSegments("/subscription"))
                        {
                            context.Token =
                                (string)context.Request.Headers[AUTH] ??
                                (string)context.Request.Query[AUTH] ??
                                (string)context.HttpContext.Items[AUTH];
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
