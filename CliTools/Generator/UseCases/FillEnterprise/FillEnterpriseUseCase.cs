using System;
using System.Linq;
using Generator.UseCases.Abstract;
using Vehicle.Contract;

namespace Generator.UseCases.FillEnterprise
{
    internal class FillEnterpriseUseCase: BaseUseCase<FillEnterpriseOption,int>
    {
        private readonly IVehicleService _vehicleService;
        private readonly IEnterpriseService _enterpriseService;

        public FillEnterpriseUseCase(IVehicleService vehicleService, IEnterpriseService enterpriseService)
        {
            _vehicleService = vehicleService;
            _enterpriseService = enterpriseService;
        }
        
        public override int ExecuteBusinessLogic(FillEnterpriseOption options)
        {
            if (options.EnterpriseList is not {Count: > 0})
                return 0;
            
            var enterprises = _enterpriseService.GetEnterprisesByIdsAsync(options.EnterpriseList).GetAwaiter().GetResult().ToDictionary(x => x.Id);
            // для каждого предприятия сформировать заданное количества машинок
            // b водителей (чтобы примерно каждая 10-я машинка была с активным водителем)
            

            return 0;
        }

        public override void HandleResult(int result) => Console.WriteLine($"Filled {result} enterprises");
    }
}