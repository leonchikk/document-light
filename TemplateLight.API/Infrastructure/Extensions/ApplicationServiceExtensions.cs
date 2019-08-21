using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateLight.Core.Interfaces;
using TemplateLight.Core.Services;

namespace TemplateLight.API.Infrastructure.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITemplateService, TemplateService>();
            return services;
        }
    }
}
