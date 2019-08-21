using DocumentLight.System.Interfaces;
using DocumentLight.System.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentLight.API.Infrastructure.Extensions
{
    public static class SystemServiceExtensions
    {
        public static IServiceCollection AddSystemsServices(this IServiceCollection services)
        {
            services.AddScoped<IFileSystem, FileSystem>();

            return services;
        }
    }
}
