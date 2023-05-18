using System;
using System.Net.Http;
using AutoPark.Api;
using AutoPark.Svc.Infrastructure;
using AutoPark.Svc.Infrastructure.TestData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;

namespace VehicleTests
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }
        
        public VehicleContext DbContext { get; }
        
        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>() // Replace 'Startup' with your application's startup class
                .UseEnvironment("Testing");
            
            var testServer = new TestServer(builder);
            Client = testServer.CreateClient();

            var options = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase("any connection string because TestDB").Options;

            DbContext = new VehicleContext(options);

            // seed InMemory Database with test data
            VehicleInitializer.Initialize(DbContext);
        }

        public void Dispose()
        {
            _testServer?.Dispose();
            Client?.Dispose();
        }
    }
}