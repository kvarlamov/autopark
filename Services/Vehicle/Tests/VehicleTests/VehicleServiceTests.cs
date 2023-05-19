using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoPark.Svc;
using AutoPark.Svc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Vehicle.Contract;
using Xunit;
using Moq;
using Newtonsoft.Json;
using Vehicle.Contract.Dto;

namespace VehicleTests
{
    public class VehicleServiceTests : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;
        private readonly VehicleContext _dbContext;
        private IVehicleService _vehicleService;

        public VehicleServiceTests(TestFixture fixture)
        {
            _client = fixture.Client;
            _dbContext = fixture.DbContext;
            _vehicleService = new VehicleService(_dbContext);
        }

        #region UnitTests

        // [Fact]
        // public async Task GetVehicle_EntityNotFound_ThrowException()
        // {
        //     // Arrange
        //     // Arrange
        //     var mockDbSet = new Mock<DbSet<AutoPark.Svc.Infrastructure.Entities.Vehicle>>();
        //     mockDbSet.As<IQueryable<AutoPark.Svc.Infrastructure.Entities.Vehicle>>()
        //         .Setup(x => x.Provider)
        //         .Returns(new TestAsyncQueryProvider<AutoPark.Svc.Infrastructure.Entities.Vehicle>(new List<AutoPark.Svc.Infrastructure.Entities.Vehicle>().AsQueryable().Provider));
        //
        //     var mockContext = new Mock<VehicleContext>();
        //     mockContext.Setup(x => x.Vehicles)
        //         .Returns(() => null);
        //
        //     _vehicleService = new VehicleService(mockContext.Object);
        //
        //     // Act - Assert
        //     await Assert.ThrowsAsync<Exception>(() => _vehicleService.GetVehicle(1));
        // }

        #endregion

        #region IntegrationTests

        [Theory]
        [MemberData(nameof(VehicleTestData.GetVehicleByIdData), MemberType = typeof(VehicleTestData))]
        public async Task GetVehicle_Exist_CorrectDto(VehicleDto expectedDto)
        {
            // Act
            var actualDto = await _vehicleService.GetVehicle(1);
            
            Assert.NotNull(actualDto);
            var expected = JsonConvert.SerializeObject(expectedDto);
            var actual = JsonConvert.SerializeObject(actualDto);
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetVehicle_EntityNotFound_ThrowException()
        {
            await Assert.ThrowsAsync<Exception>(() => _vehicleService.GetVehicle(100));
        }

        #endregion
        
    }
}