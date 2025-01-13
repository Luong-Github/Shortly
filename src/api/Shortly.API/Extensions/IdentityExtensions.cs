using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shortly.Domain.Entities.Identity;
using Shortly.Persistence.Contexts;
using Shortly.Persistence.Options;

namespace Shortly.API.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddIdentityExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind IdentityOptions from appsettings.json
            services.Configure<IdentityOptions>(configuration.GetSection("IdentityOptions"));

            // Configure Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
                //.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            // Load PasswordValidatorOptions from appsettings.json
            var serviceProvider = services.BuildServiceProvider();
            var passwordValidatorOptions = serviceProvider.GetService<IOptionsMonitor<PasswordValidatorOptions>>();

            services.Configure<PasswordValidatorOptions>(configuration.GetSection("PasswordValidatorOptions"));

        }
    }
}
