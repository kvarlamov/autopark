using Generator.UseCases.FillEnterprise;
using Microsoft.Extensions.DependencyInjection;

namespace Generator.UseCases
{
    internal static class UseCasesDiExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<FillEnterpriseUseCase>();
        }
    }
}