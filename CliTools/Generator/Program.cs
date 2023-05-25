using CommandLine;
using Generator.UseCases.Abstract;
using Generator.UseCases.FillEnterprise;
using Microsoft.Extensions.DependencyInjection;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<FillEnterpriseOption>(args)
                .WithParsed<FillEnterpriseOption>(Run<FillEnterpriseUseCase, FillEnterpriseOption>);
        }

        private static void Run<TUseCase, TOption>(TOption option)
            where TUseCase: IUseCase<TOption>
        {
            var services = new ServiceCollection();
            services.AddDependencies();
            
            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();

            var useCase = serviceProvider.GetRequiredService<TUseCase>();
            useCase.Execute(option);
        }
    }
}