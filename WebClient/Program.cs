using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebClient.Services;

namespace WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton<ApiConfiguration>(sp => new ApiConfiguration()
                {BaseAddress = "http://localhost:5005"});

            builder.Services.AddScoped(
                sp =>
                {
                    var apiConfig = sp.GetRequiredService<ApiConfiguration>();
                    return new HttpClient {BaseAddress = new Uri(apiConfig.BaseAddress)};
                });
            builder.Services.AddScoped<AuthService>();

            await builder.Build().RunAsync();
        }
    }
}