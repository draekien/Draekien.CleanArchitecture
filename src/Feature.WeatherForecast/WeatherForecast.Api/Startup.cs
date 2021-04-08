using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

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
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddApplication();
            services.AddCustomControllers();
            services.AddCustomSwaggerGen();

            services.AddProblemDetails(ConfigureProblemDetails.Defaults);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecast.Api v1"));
                app.UseReDoc(c =>
                {
                    c.RoutePrefix = "docs";
                    c.DocumentTitle = "WeatherForecast API";
                    c.SpecUrl = "/swagger/v1/swagger.json";
                    c.ExpandResponses("200,201");
                    c.RequiredPropsFirst();
                    c.SortPropsAlphabetically();
                });
            }

            app.UseSerilogRequestLogging();
            app.UseProblemDetails();
            app.UseExceptionHandler(ConfigureExceptionHandler.Defaults);
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
