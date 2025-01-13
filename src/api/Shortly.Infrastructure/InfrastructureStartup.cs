using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shortly.Application.Abstractions;
using Shortly.Contract.Abstractions;
using Shortly.Domain.Entities.Identity;
using Shortly.Infrastructure.Abstractions;
using Shortly.Infrastructure.Abstractions.DataAccess;
using Shortly.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Infrastructure
{
    public static class InfrastructureStartup
    {
        public static void AddInfrastructureConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IJwtTokenService,  JwtTokenService>();

            services.AddTransient<IUserServices, UserServices>();
            // configure for application user
            //services.AddIdentity<ApplicationUser, IdentityUser<Guid>>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
        }

        public static void AddInterceptorPersistence(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
