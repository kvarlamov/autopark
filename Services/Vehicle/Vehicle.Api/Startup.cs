using System;
using AutoPark.Svc;
using AutoPark.Svc.Infrastructure;
using AutoPark.Svc.Infrastructure.TestData;
using Driver.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Vehicle.Contract;

namespace AutoPark.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Vehicle.Api", Version = "v1"});
            });

            services.AddDbContext<VehicleContext>(options => options.UseNpgsql(Configuration.GetConnectionString("AutoParkDB")));

            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IEnterpriseService, EnterpriseService>();
            services.AddScoped<IDriverService, DriverService>();
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorClient",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:6003")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicle.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseCors("AllowBlazorClient");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            if (env.IsDevelopment())
            {
                InitializeByTestData(app);
            }
        }

        private static void InitializeByTestData(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<VehicleContext>();
                VehicleInitializer.Initialize(context);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}