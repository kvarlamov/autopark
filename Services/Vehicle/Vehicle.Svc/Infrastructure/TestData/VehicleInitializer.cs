using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Vehicle.Contract.Enums;

namespace AutoPark.Svc.Infrastructure.TestData
{
    public sealed class VehicleInitializer
    {
        public static void Initialize(VehicleContext _db)
        {
            DatabaseFacade db = _db.Database;
            db.EnsureDeleted();
            db.EnsureCreated();
        
            _db.Vehicles.AddRange(FakeDataFactory.GetVehicles());

            _db.SaveChanges();
        }
    }
    
    public static class FakeDataFactory
    {
        public static List<Entities.Vehicle> GetVehicles() =>
            new()
            {
                new Entities.Vehicle()
                {
                    Color = "Black",
                    Cost = 10000,
                    VehicleState = VehicleState.Normal,
                    Mileage = 35000,
                    ManufactureYear = 2018,
                    Transmission = Transmission.Manual
                },
                new Entities.Vehicle()
                {
                    Color = "Black",
                    Cost = 15000,
                    VehicleState = VehicleState.Normal,
                    Mileage = 25000,
                    ManufactureYear = 2019,
                    Transmission = Transmission.Automatic
                },
                new Entities.Vehicle()
                {
                    Color = "White",
                    Cost = 30000,
                    VehicleState = VehicleState.Normal,
                    Mileage = 15000,
                    ManufactureYear = 2020,
                    Transmission = Transmission.Automatic
                }
            };
    }  
}