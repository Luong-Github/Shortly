using Microsoft.Extensions.DependencyInjection;
using Shortly.Application.Features.V1.Urls.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ApplicationServiceConfigure(this IServiceCollection services)
        {
            services.AddScoped<IUrlServices, UrlServices>();
        }
    }
}
