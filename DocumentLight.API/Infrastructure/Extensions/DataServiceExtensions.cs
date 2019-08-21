using DocumentLight.Core.Entities;
using DocumentLight.Core.Interfaces;
using DocumentLight.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentLight.API.Infrastructure.Extensions
{
    public static class DataServiceExtensions
    {
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<File>, Repository<File>>();
            services.AddScoped<IRepository<Template>, Repository<Template>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
