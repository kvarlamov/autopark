using System.Collections.Generic;
using Vehicle.Contract.Dto;
using Vehicle.Contract.Enums;

namespace VehicleTests
{
    public class VehicleTestData
    {
        public static IEnumerable<object[]> GetVehicleByIdData()
        {
            long vehicleId = 1;
            yield return new object[]
            {
                new VehicleDto()
                {
                    Id = vehicleId,
                    Color = "Black",
                    Cost = 90000,
                    VehicleState = VehicleState.Normal,
                    Mileage = 35000,
                    ManufactureYear = 2018,
                    Transmission = Transmission.Manual,
                    BrandId = 1,
                    BrandName = "Mersedes-Benz",
                    Drivers = new List<long>(){1,2},
                    Enterprise = 1,
                    ActiveDriver = 1
                }
            };
        }
    }
}