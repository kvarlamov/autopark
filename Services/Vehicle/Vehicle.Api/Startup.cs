using System;
using AutoPark.Api.Authentication;
using AutoPark.Svc;
using AutoPark.Svc.Infrastructure;
using AutoPark.Svc.Infrastructure.Entities;
using AutoPark.Svc.Infrastructure.TestData;
using Driver.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Vehicle.Api.Module.Tools;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo {Title = "Vehicle.Api", Version = "v1"});
                
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.AddVehicleDependencies(Configuration);
            
            services
                .AddAuthentication()
                .AddScheme<JwtSchemeOptions, JwtSchemeHandler>(AuthSchemas.Jwt, options => { options.IsActive = true; });
            services.AddAuthorization(options =>
            {
                // Создаём политику проверяющую аутентификацию пользователя
                options.AddPolicy(
                    Policies.AuthorizedOnly,
                    policy => policy.RequireAuthenticatedUser()
                );
                
                options.AddPolicy(
                    Policies.IsManager,
                    policy => policy.AddRequirements(new IsManagerRequirement())
                    );
            });
            
            // Add logging services
            services.AddLogging(builder =>
            {
                builder.AddConsole(); // Log to the console
                builder.AddDebug(); // Log to the debug output window
            });

            // Add Identity
            services.AddIdentity<Manager, IdentityRole<long>>()
                .AddEntityFrameworkStores<VehicleContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IAuthorizationHandler, IsManagerPolicyHandler>();
            
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

            app.UseAuthentication();
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
                var userManager = services.GetRequiredService<UserManager<Manager>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<long>>>();
                VehicleInitializer.Initialize(context, userManager, roleManager);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}