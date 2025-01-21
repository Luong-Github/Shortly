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
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Configure lockout options
                var lockoutOptions = configuration.GetSection("IdentityOptions:Lockout");
                options.Lockout.DefaultLockoutTimeSpan = lockoutOptions.GetValue<TimeSpan>("DefaultLockoutTimeSpan");
                options.Lockout.MaxFailedAccessAttempts = lockoutOptions.GetValue<int>("MaxFailedAccessAttempts");
                options.Lockout.AllowedForNewUsers = lockoutOptions.GetValue<bool>("AllowedForNewUsers");

                // Configure password options
                var passwordOptions = configuration.GetSection("IdentityOptions:Password");
                options.Password.RequireDigit = passwordOptions.GetValue<bool>("RequireDigit");
                options.Password.RequireLowercase = passwordOptions.GetValue<bool>("RequireLowercase");
                options.Password.RequireNonAlphanumeric = passwordOptions.GetValue<bool>("RequireNonAlphanumeric");
                options.Password.RequireUppercase = passwordOptions.GetValue<bool>("RequireUppercase");
                options.Password.RequiredLength = passwordOptions.GetValue<int>("RequiredLength");
                options.Password.RequiredUniqueChars = passwordOptions.GetValue<int>("RequiredUniqueChars");

                // Configure user options
                var userOptions = configuration.GetSection("IdentityOptions:User");
                options.User.RequireUniqueEmail = userOptions.GetValue<bool>("RequireUniqueEmail");

                // Configure sign-in options
                var signInOptions = configuration.GetSection("IdentityOptions:SignIn");
                options.SignIn.RequireConfirmedEmail = signInOptions.GetValue<bool>("RequireConfirmedEmail");
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
