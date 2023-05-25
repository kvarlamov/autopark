using System;
using System.IO;
using Generator.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vehicle.Api.Module.Tools;


namespace Generator
{
    internal static class DependencyInjectionExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            Console.WriteLine("Initializing dependencies ..");
            
            var configuration = GetConfiguration();
            services.AddVehicleDependencies(configuration);
            services.AddUseCases();

            Console.WriteLine(".. DONE\n");
        }

        private static IConfigurationRoot GetConfiguration()
        {
            string appsettingsPath = Path.GetFullPath(Path.Combine(@"..\..\..\..\..\Services\Vehicle\Vehicle.Api", "appsettings.json"));

            return new ConfigurationBuilder()
                .AddJsonFile(appsettingsPath, true, true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}