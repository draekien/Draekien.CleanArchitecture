using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;

using Hellang.Middleware.ProblemDetails;

using MediatR.Extensions.FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Serilog;

using Swashbuckle.AspNetCore.Swagger;

using WeatherForecast.Api.OnStart;
using WeatherForecast.Application;

namespace WeatherForecast.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddApplication();
            services.AddCustomControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherForecast.Api", Version = "v1" });

                var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);

                var applicationXmlFile = $"{typeof(Application.DependencyInjection).Assembly.GetName().Name}.xml";
                var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);

                c.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
                c.IncludeXmlComments(applicationXmlPath, includeControllerXmlComments: true);

                c.DocInclusionPredicate((_,_) => true);
                c.AddFluentValidationRules();
            });

            services.AddProblemDetails(ConfigureProblemDetails.Defaults);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecast.Api v1"));
            }

            app.UseProblemDetails();
            app.UseExceptionHandler(ConfigureExceptionHandler.Defaults);
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
