using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoPark.Svc.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Vehicle.Contract.Enums;

namespace AutoPark.Svc.Infrastructure.TestData
{
    public sealed class VehicleInitializer
    {
        private static int _currentPointer = 0;
        private static List<(double, double)> _coordinates = new List<(double, double)>
        {
            (51.5074, -0.1278),   // London, United Kingdom
            (48.8566, 2.3522),    // Paris, France
            (41.9028, 12.4964),   // Rome, Italy
            (52.5200, 13.4050),   // Berlin, Germany
            (55.6761, 12.5683),   // Copenhagen, Denmark
            (59.3293, 18.0686),   // Stockholm, Sweden
            (52.3702, 4.8952),    // Amsterdam, Netherlands
            (53.3498, -6.2603),   // Dublin, Ireland
            (38.7223, -9.1393),   // Lisbon, Portugal
            (41.3851, 2.1734),    // Barcelona, Spain
            (48.2082, 16.3738),   // Vienna, Austria
            (50.0755, 14.4378),   // Prague, Czech Republic
            (47.4979, 19.0402),   // Budapest, Hungary
            (52.2297, 21.0122),   // Warsaw, Poland
            (59.9343, 30.3351),   // Saint Petersburg, Russia
            (60.1699, 24.9384),   // Helsinki, Finland
            (59.4369, 24.7536),   // Tallinn, Estonia
            (56.9496, 24.1052),   // Riga, Latvia
            (54.6872, 25.2797),   // Vilnius, Lithuania
            (41.0082, 28.9784),   // Istanbul, Turkey
            (38.7223, -9.1393),   // Lisbon, Portugal
            (53.9045, 27.5615)    // Minsk, Belarus
        };
        
        public static void Initialize(VehicleContext db, UserManager<Manager> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            DatabaseFacade database = db.Database;
            database.EnsureDeleted();
            database.EnsureCreated();

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
                Brand = mersedes,
                OrderTime = GetOrderTime(50, 10, 3)
            };

            var vehicle2 = new Entities.Vehicle
            {
                Color = "Black",
                Cost = 20000,
                VehicleState = VehicleState.Normal,
                Mileage = 25000,
                ManufactureYear = 2019,
                Transmission = Transmission.Automatic, 
                Brand = kamaz,
                OrderTime = GetOrderTime(100, 20, 50)
            };

            var vehicle3 = new Entities.Vehicle
            {
                Color = "White",
                Cost = 30000,
                VehicleState = VehicleState.Normal,
                Mileage = 15000,
                ManufactureYear = 2020,
                Transmission = Transmission.Automatic,
                Brand = mazda,
                OrderTime = GetOrderTime(20, 10, 5)
            };
            
            var vehicle4 = new Entities.Vehicle
            {
                Color = "Red",
                Cost = 35000,
                VehicleState = VehicleState.Normal,
                Mileage = 10000,
                ManufactureYear = 2021,
                Transmission = Transmission.Automatic,
                Brand = mazda,
                OrderTime = GetOrderTime(300, 5, 1)
            };
            
            var vehicle5 = new Entities.Vehicle
            {
                Color = "Test1",
                Cost = 35000,
                VehicleState = VehicleState.Normal,
                Mileage = 10000,
                ManufactureYear = 2021,
                Transmission = Transmission.Automatic,
                Brand = mazda,
                OrderTime = GetOrderTime(600, 25, 41)
            };
            
            var vehicle6 = new Entities.Vehicle
            {
                Color = "Test2",
                Cost = 35000,
                VehicleState = VehicleState.Normal,
                Mileage = 10000,
                ManufactureYear = 2021,
                Transmission = Transmission.Automatic,
                Brand = mazda,
                OrderTime = GetOrderTime(640, 15, 1)
            };
            var vehicle7 = new Entities.Vehicle
            {
                Color = "Test3",
                Cost = 35000,
                VehicleState = VehicleState.Normal,
                Mileage = 10000,
                ManufactureYear = 2021,
                Transmission = Transmission.Automatic,
                Brand = mazda,
                OrderTime = GetOrderTime(640, 15, 1)
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

            string password = "Pass123$";
            var manager1 = new Manager()
            {
                Email = "manager1@man.com",
                UserName = "manager1@man.com",
                
            };
            var manager2 = new Manager()
            {
                Email = "manager2@man.com",
                UserName = "manager2@man.com"
            };

            if (roleManager.FindByNameAsync("manager").GetAwaiter().GetResult() == null)
            {
                roleManager.CreateAsync(new IdentityRole<long>("manager")).GetAwaiter().GetResult();
            }

            IdentityResult result1 = userManager.CreateAsync(manager1, password).GetAwaiter().GetResult();
            if (result1.Succeeded)
            {
                userManager.AddToRoleAsync(manager1, "manager").GetAwaiter().GetResult();
            }
            
            IdentityResult result2 = userManager.CreateAsync(manager2, password).GetAwaiter().GetResult();
            if (result2.Succeeded)
            {
                userManager.AddToRoleAsync(manager2, "manager").GetAwaiter().GetResult();
            }

            // with cars and drivers
            var enterprise1 = new Entities.Enterprise
            {
                Name = "Stroy Invest",
                City = "Kaliningrad",
                Code = 112005,
                NumberOfStaff = 1_000,
                TimezoneOffset = 1
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
                NumberOfStaff = 10_000,
                TimezoneOffset = 3
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
                NumberOfStaff = 500,
                TimezoneOffset = 2
            };
            enterprise3.Vehicles.Add(vehicle4);
            enterprise3.Managers.Add(manager2);
            
            var enterprise4 = new Entities.Enterprise
            {
                Name = "Test enterprise1",
                City = "Vladivostok",
                Code = 278105,
                NumberOfStaff = 1500,
                TimezoneOffset = 10
            };
            
            var enterprise5 = new Entities.Enterprise
            {
                Name = "Test enterprise2",
                City = "London",
                Code = 971505,
                NumberOfStaff = 1200
            };
            
            var enterprise6 = new Entities.Enterprise
            {
                Name = "Test enterprise3",
                City = "StrangePlace",
                Code = 325691,
                NumberOfStaff = 200,
                TimezoneOffset = -1
            };
            enterprise4.Managers.AddRange(new []{manager1, manager2});
            enterprise4.Vehicles.Add(vehicle5);
            enterprise5.Managers.AddRange(new []{manager1, manager2});
            enterprise5.Vehicles.Add(vehicle6);
            enterprise6.Managers.AddRange(new []{manager1, manager2});
            enterprise6.Vehicles.Add(vehicle7);
            
            db.Enterprises.AddRange(enterprise1, enterprise2, enterprise3, enterprise4, enterprise5, enterprise6);

            Trip trip1 = new Trip()
            {
                Vehicle = vehicle6,
                StartTime = GetTripTime(30),
                EndTime = null
            };

            Trip trip2 = new Trip()
            {
                Vehicle = vehicle6,
                StartTime = GetTripTime(29, 5),
                EndTime = GetTripTime(19, 4)
            };
            
            Trip trip3 = new Trip()
            {
                Vehicle = vehicle6,
                StartTime = GetTripTime(20, 1, 5),
                EndTime = GetTripTime(10, 5, 1)
            };
            
            Trip trip4 = new Trip()
            {
                Vehicle = vehicle6,
                StartTime = GetTripTime(10, 1, 5),
                EndTime = null,
            };
            
            Trip trip5 = new Trip()
            {
                Vehicle = vehicle6,
                StartTime = GetTripTime(1, 1, 5),
                EndTime = GetTripTime(0, 5, 7),
            };
            
            trip1.Points.AddRange(GetTrackPointsForTrip(trip1, vehicle6));
            trip2.Points.AddRange(GetTrackPointsForTrip(trip2, vehicle6, 3));
            trip3.Points.AddRange(GetTrackPointsForTrip(trip3, vehicle6, 4));
            trip4.Points.AddRange(GetTrackPointsForTrip(trip4, vehicle6));
            trip5.Points.AddRange(GetTrackPointsForTrip(trip5, vehicle6));
            
            db.Trips.AddRange(trip1, trip2, trip3, trip4, trip5);

            db.SaveChanges();
            
            // // Автомобиль и водитель могут принадлежать только одному предприятию.
            // enterprise2.Vehicles.Add(vehicle1);
            // _db.SaveChanges();
            //
            // // Автомобиль и водитель могут принадлежать только одному предприятию.
            // enterprise2.Drivers.Add(driver1);
            // _db.SaveChanges();
            //
            // // Активный водитель может работать только на одной машине (не может быть назначен активным на второй автомобиль).
            // vehicle2.ActiveDriver = driver1;
            // _db.SaveChanges();
        }

        private static List<TrackPoint> GetTrackPointsForTrip(Trip trip, Entities.Vehicle vehicle, int numberOfPoints = 5)
        {
            var points = new List<TrackPoint>();

            if (trip.EndTime is null)
            {
                Random rnd = new Random();
                for (int i = 0; i < numberOfPoints; i++)
                {
                    if (_currentPointer > _coordinates.Count - 1)
                        _currentPointer = 0;
                    
                    points.Add(new TrackPoint()
                    {
                        Vehicle = vehicle,
                        TrackTime = GetTripTime(rnd.Next(0, 100), rnd.Next(0,23), rnd.Next(0,59)),
                        Latitude = _coordinates[_currentPointer].Item1.ToString(CultureInfo.InvariantCulture),
                        Longitude = _coordinates[_currentPointer].Item2.ToString(CultureInfo.InvariantCulture),
                    });
                    _currentPointer++;
                }

                points = points.OrderBy(x => x.TrackTime).ToList();
                points.First().TrackTime = trip.StartTime;

                return points;
            }

            for (int i = 0; i < numberOfPoints; i++)
            {
                if (_currentPointer > _coordinates.Count - 1)
                    _currentPointer = 0;
                
                points.Add(new TrackPoint()
                {
                    Vehicle = vehicle,
                    TrackTime = GetRandomDateTimeOffsetBetween(trip.StartTime, trip.EndTime.Value),
                    Latitude = _coordinates[_currentPointer].Item1.ToString(CultureInfo.InvariantCulture),
                    Longitude = _coordinates[_currentPointer].Item2.ToString(CultureInfo.InvariantCulture),
                });
                _currentPointer++;
            }
            
            points = points.OrderBy(x => x.TrackTime).ToList();
            points.First().TrackTime = trip.StartTime;
            points.Last().TrackTime = trip.EndTime.Value;


            return points;
        }

        private static DateTimeOffset GetOrderTime(int days, int hours, int minutes)
        {
            return DateTimeOffset.Now;
        }
        
        private static DateTimeOffset GetTripTime(int? days = null, int? hours = null, int? minutes = null)
        {
            return DateTimeOffset.Now - TimeSpan.FromDays(days??0)- TimeSpan.FromHours(hours??0) - TimeSpan.FromMinutes(minutes??0);
        }
        
        private static DateTimeOffset GetFutureTripTime(int? days = null, int? hours = null, int? minutes = null)
        {
            return DateTimeOffset.Now + TimeSpan.FromDays(days??0) + TimeSpan.FromHours(hours??0) + TimeSpan.FromMinutes(minutes??0);
        }
        
        private static DateTimeOffset GetRandomDateTimeOffsetBetween(DateTimeOffset start, DateTimeOffset end)
        {
            // Get the time difference between start and end
            TimeSpan difference = end - start;

            // Generate a random number of ticks within the range of ticks between start and end
            long randomTicks = (long)(new Random().NextDouble() * difference.Ticks);

            // Add the random number of ticks to start
            DateTimeOffset randomDateTimeOffset = start + TimeSpan.FromTicks(randomTicks);

            return randomDateTimeOffset;
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
                },
                new Entities.Vehicle()
                {
                    Color = "Test1",
                    Cost = 40000,
                    VehicleState = VehicleState.Normal,
                    Mileage = 15000,
                    ManufactureYear = 2010,
                    Transmission = Transmission.Automatic
                },
                new Entities.Vehicle()
                {
                    Color = "Test2",
                    Cost = 40000,
                    VehicleState = VehicleState.Normal,
                    Mileage = 15000,
                    ManufactureYear = 2010,
                    Transmission = Transmission.Automatic
                },
            };
    }  
}