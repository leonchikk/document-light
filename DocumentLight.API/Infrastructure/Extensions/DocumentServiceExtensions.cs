using DocumentLight.Application.Interfaces;
using DocumentLight.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentLight.API.Infrastructure.Extensions
{
    public static class DocumentServiceExtensions
    {
        public static IServiceCollection AddDocumentServices(this IServiceCollection services)
        {
            services.AddScoped<ITemplateService, TemplateService>();

            return services;
        }
    }
}
