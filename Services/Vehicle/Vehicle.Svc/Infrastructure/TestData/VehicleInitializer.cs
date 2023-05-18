using System.Collections.Generic;
using AutoPark.Svc.Infrastructure.Entities;
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

            Brand mersedes = new Brand
            {
                Name = "Mersedes-Benz",
                Tank = 400,
                LoadCapacity = 3000,
                VehicleType = VehicleType.Bus,
                NumberOfSeats = 50
            };
            
            Brand kamaz = new Brand
            {
                Name = "Kamaz",
                Tank = 300,
                LoadCapacity = 8000,
                VehicleType = VehicleType.Truck,
                NumberOfSeats = 3
            };
            
            Brand mazda = new Brand
            {
                Name = "Mazda",
                Tank = 50,
                LoadCapacity = 1500,
                VehicleType = VehicleType.Car,
                NumberOfSeats = 5
            };
            
            // _db.AddRange(mersedes, kamaz, mazda);

            var vehicle1 = new Entities.Vehicle
            {
                Color = "Black",
                Cost = 90000,
                VehicleState = VehicleState.Normal,
                Mileage = 35000,
                ManufactureYear = 2018,
                Transmission = Transmission.Manual,
                Brand = mersedes
            };

            var vehicle2 = new Entities.Vehicle
            {
                Color = "Black",
                Cost = 20000,
                VehicleState = VehicleState.Normal,
                Mileage = 25000,
                ManufactureYear = 2019,
                Transmission = Transmission.Automatic, 
                Brand = kamaz
            };

            var vehicle3 = new Entities.Vehicle
            {
                Color = "White",
                Cost = 30000,
                VehicleState = VehicleState.Normal,
                Mileage = 15000,
                ManufactureYear = 2020,
                Transmission = Transmission.Automatic,
                Brand = mazda
            };
            
            var vehicle4 = new Entities.Vehicle
            {
                Color = "Red",
                Cost = 35000,
                VehicleState = VehicleState.Normal,
                Mileage = 10000,
                ManufactureYear = 2021,
                Transmission = Transmission.Automatic,
                Brand = mazda
            };
            // _db.Vehicles.AddRange(vehicle1, vehicle2, vehicle3);

            var driver1 = new Entities.Driver
            {
                Name = "Ivan Petrov",
                Age = 45,
                Salary = 50000
            };
            driver1.Vehicles = new List<Entities.Vehicle> {vehicle1, vehicle2};
            vehicle1.ActiveDriver = driver1;

            var driver2 = new Entities.Driver
            {
                Name = "Oleg Sidorov",
                Age = 40,
                Salary = 55000
            };
            driver2.Vehicles = new List<Entities.Vehicle> {vehicle1, vehicle2};
            
            var driver3 = new Entities.Driver
            {
                Name = "Semen Sidorov",
                Age = 46,
                Salary = 56000
            };
            driver3.Vehicles = new List<Entities.Vehicle> {vehicle3};
            
            var manager1 = new Manager()
            {
                Email = "manager1@man.com",
                UserName = "manager1"
            };
            var manager2 = new Manager()
            {
                Email = "manager2@man.com",
                UserName = "manager2"
            };

            // with cars and drivers
            var enterprise1 = new Entities.Enterprise
            {
                Name = "Stroy Invest",
                City = "Kaliningrad",
                Code = 112005,
                NumberOfStaff = 1_000
            };
            enterprise1.Vehicles.AddRange(new []{vehicle1, vehicle2});
            enterprise1.Drivers.AddRange(new []{driver1, driver2});
            enterprise1.Managers.Add(manager1);

            // with cars and drivers
            var enterprise2 = new Entities.Enterprise
            {
                Name = "Garaj Rent",
                City = "Moskow",
                Code = 715105,
                NumberOfStaff = 10_000
            };
            enterprise2.Vehicles.Add(vehicle3);
            enterprise2.Drivers.Add(driver3);
            enterprise2.Managers.AddRange(new []{manager1, manager2});
            
            // with cars but without drivers
            var enterprise3 = new Entities.Enterprise
            {
                Name = "Arenda avto",
                City = "Novgorod",
                Code = 377005,
                NumberOfStaff = 500
            };
            enterprise3.Vehicles.Add(vehicle4);
            enterprise3.Managers.Add(manager2);
            
            _db.Enterprises.AddRange(enterprise1, enterprise2, enterprise3);

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