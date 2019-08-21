using AutoMapper;
using DocumentLight.API.Infrastructure.Extensions;
using DocumentLight.API.Infrastructure.Middlewares;
using DocumentLight.Application.Settings;
using DocumentLight.Data;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace DocumentLight.API
{
    public class Startup
    {
        // Settings
        TemplatesApiSettings TemplatesApiSettings => Configuration.GetSection("TemplatesApiSettings").Get<TemplatesApiSettings>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureAutoMapper();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("docker-db")));
            services.AddHttpContextAccessor();
            services.AddSingleton(TemplatesApiSettings);
            services.ConfigureDataServices();
            services.AddSystemsServices();
            services.AddMvc();
            services.AddSingleton(RabbitHutch.CreateBus($"host={Configuration.GetSection("RabbitMqHost").Value}"));
            services.AddSwaggerDocumentation();
            services.AddAuthorizationServices();
            services.AddDocumentServices();
            services.ConfigureCors();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerDocumentation();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
