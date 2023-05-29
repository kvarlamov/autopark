using System;
using System.Collections.Generic;
using System.Linq;
using Driver.Contract;
using Driver.Contract.Dto;
using Generator.UseCases.Abstract;
using Vehicle.Contract;
using Vehicle.Contract.Dto;
using Vehicle.Contract.Enums;

namespace Generator.UseCases.FillEnterprise
{
    internal class FillEnterpriseUseCase: BaseUseCase<FillEnterpriseOption, string>
    {
        private readonly IVehicleService _vehicleService;
        private readonly IEnterpriseService _enterpriseService;
        private readonly IDriverService _driverService;
        private const int ActiveDriverStep = 10;

        public FillEnterpriseUseCase(IVehicleService vehicleService, IEnterpriseService enterpriseService, IDriverService driverService)
        {
            _vehicleService = vehicleService;
            _enterpriseService = enterpriseService;
            _driverService = driverService;
        }
        
        public override string ExecuteBusinessLogic(FillEnterpriseOption options)
        {
            if (options.EnterpriseList == null || !options.EnterpriseList.Any())
                return "List of enterprises is empty. Seeding not performed";
            
            var enterprises = _enterpriseService.GetEnterprisesByIdsAsync(options.EnterpriseList.ToList()).GetAwaiter().GetResult().ToList();
            // для каждого предприятия сформировать заданное количества машинок
            // b водителей (чтобы примерно каждая 10-я машинка была с активным водителем)

            foreach (var enterprise in enterprises)
            {
                var createdVehicles = new List<long>();
                for (int i = 0; i < options.NumberOfVehicles; i++)
                {
                    Random rnd = new Random();
                    
                    var newVehicleDto = GenerateNewVehicle(enterprise.Id, rnd);
                    var createdVehicle = _vehicleService.CreateAsync(newVehicleDto).GetAwaiter().GetResult();

                    createdVehicles.Add(createdVehicle.Id);
                }

                for (int i = 0; i < options.NumberOfVehicles; i++)
                {
                    Random rnd = new Random();
                    
                    var newDriverDto = GenerateNewDriver(rnd, enterprise.Id);
                    newDriverDto.Vehicles.AddRange(createdVehicles);
                    if (i % ActiveDriverStep == 0)
                        newDriverDto.OnVehicle = createdVehicles[i];

                    _driverService.CreateAsync(newDriverDto).GetAwaiter().GetResult();
                }
            }

            string result = $"Seed done for {enterprises.Count}";
            
            return result;
        }

        private DriverDto GenerateNewDriver(Random random, long enterpriseId)
        {
            return new DriverDto()
            {
                Name = GenerateDriverName(random),
                Enterprise = enterpriseId,
                Age = GenerateAge(random),
                Salary = GenerateSalary(random)
            };
        }

        private string GenerateDriverName(Random random)
        {
            string[] firstNames = { "John", "Emma", "Michael", "Sophia", "James", "Olivia", "William", "Ava", "Alexander", "Isabella" };
            string[] lastNames = { "Smith", "Johnson", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas" };

            string firstName = firstNames[random.Next(firstNames.Length)];
            string lastName = lastNames[random.Next(lastNames.Length)];

            return $"{firstName} {lastName}";
        }

        private int GenerateAge(Random random)
        {
            return random.Next(20, 70);
        }

        private decimal GenerateSalary(Random random)
        {
            return Convert.ToDecimal(random.Next(10_000, 150_000));
        }

        private VehicleDto GenerateNewVehicle(long enterpriseId, Random rnd)
        {
            return new VehicleDto()
            {
                Color = GenerateColor(rnd),
                Cost = GenerateCost(rnd),
                Mileage = GenerateMileage(rnd),
                Transmission = GenerateTransmission(rnd),
                Enterprise = enterpriseId,
                ManufactureYear = GenerateManufactureYear(rnd),
                VehicleState = GenerateVehicleState(rnd),
                BrandId = GenerateBrand(rnd)
            };
        }

        private long GenerateBrand(Random random)
        {
            var brands = _vehicleService.GetBrands().GetAwaiter().GetResult();
            var brandIds = brands.Select(x => x.Id).ToArray();

            var index = random.Next(brandIds.Length);
            return brandIds[index];
        }

        private VehicleState GenerateVehicleState(Random rnd)
        {
            return (VehicleState) rnd.Next(1,2);
        }

        private int GenerateMileage(Random random)
        {
            return random.Next(1000, 200000);
        }

        private string GenerateColor(Random random)
        {
            string[] colors = { "Red", "Blue", "Green", "Yellow", "Black", "White", "Silver", "Gray" };
            return colors[random.Next(colors.Length)];
        }

        private decimal GenerateCost(Random random)
        {
            return Convert.ToDecimal(random.Next(1000, 10000));
        }

        public Transmission GenerateTransmission(Random random)
        {
            return (Transmission)random.Next(1,2);
        }

        public int GenerateManufactureYear(Random random)
        {
            return random.Next(1980, 2024);
        }

        public override void HandleResult(string result) => Console.WriteLine(result);
    }
}