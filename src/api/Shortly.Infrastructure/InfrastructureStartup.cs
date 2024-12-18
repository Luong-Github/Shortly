using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shortly.Infrastructure.Abstractions;
using Shortly.Infrastructure.Abstractions.DataAccess;
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
        }

        public static void AddInterceptorPersistence(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
