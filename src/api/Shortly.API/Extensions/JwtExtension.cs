using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shortly.Infrastructure.DependencyInjections.Options;
using System.Text;

namespace Shortly.API.Extensions
{
    public static class JwtExtension
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(
                options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                // Add JWT
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwt =>
                {
                    var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

                    var key = Encoding.UTF8.GetBytes(jwtOptions.Key);
                    jwt.SaveToken = true; // save to authentication properties

                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false, // on prod make it true
                        ValidateAudience = false, // on prod make it true
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                    };

                    jwt.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            // Additional token validation logic (e.g, check token in blacklist)
                        },

                        OnAuthenticationFailed = context =>
                        {
                            if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                Log.Logger.Error("Token expired");
                                context.Response?.Headers?.Add("IS-TOKEN-EXPIRED", "true");
                            }

                            Log.Logger.Error("Authentication failed: {0}", context.Exception.Message);
                            return Task.CompletedTask;
                        }
                    };
                });
            // Add Oauth
            // Add MFA

            //services.AddAuthorization();
        }
    }
}
