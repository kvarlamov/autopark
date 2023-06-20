using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vehicle.Contract;
using Vehicle.Contract.Dto;

namespace AutoPark.Svc
{
    public class TrackGeneratorHelper : ITrackGeneratorHelper
    {
        private readonly ITripService _tripService;
        private readonly HttpClient _client;
        private static ConcurrentQueue<TrackPointDto> _queue = new();
        private static decimal _avSpeed;
        private static decimal _distance;

        public TrackGeneratorHelper(ITripService tripService, IHttpClientFactory clientFactory)
        {
            _tripService = tripService;
            _client = clientFactory.CreateClient();
        }
        
        public async Task GenerateTrack()
        {
            // if queue not empty - get point from queue
            if (_queue.TryDequeue(out var point))
            {
                await _tripService.AddTrackPointToTrip(point, _avSpeed, _distance);
                return;
            }
            
            string json = @"{
                ""coordinates"": [[8.681495,49.41461],[8.686507,49.41943]],
                ""maximum_speed"": 90,
                ""units"":""km"",
                ""attributes"":[""avgspeed""]
            }";

            // Create the request message
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openrouteservice.org/v2/directions/driving-car/geojson"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Set the headers
            request.Headers.Add("Accept", "application/json, application/geo+json, application/gpx+xml, img/png");
            request.Headers.Add("Authorization", "5b3ce3597851110001cf624828cd08a61d7f42749b64341ea735b971");

            // Send the request
            HttpResponseMessage response = await _client.SendAsync(request);

            // Read the response
            string responseContent = await response.Content.ReadAsStringAsync();
            
            
            var jsonObject = JObject.Parse(responseContent);
            var geometryCoordinates = jsonObject["features"][0]["geometry"]["coordinates"];
            var properties = jsonObject["features"][0]["properties"]["segments"][0];
            _avSpeed = decimal.Parse(properties["avgspeed"].ToString());
            _distance = decimal.Parse(properties["distance"].ToString());

            foreach (var coordinate in geometryCoordinates)
            {
                var lat = coordinate[0].ToString();
                var lon = coordinate[1].ToString();
                _queue.Enqueue(new TrackPointDto()
                {
                    Latitude = lat,
                    Longitude = lon,
                    VehicleId = 2,
                    TrackTime = DateTimeOffset.Now
                });
            }

            if (_queue.TryDequeue(out var newPoint))
                await _tripService.AddTrackPointToTrip(newPoint, _avSpeed, _distance);
            
            // curl -X POST \
            // 'https://api.openrouteservice.org/v2/directions/driving-car' \
            // -H 'Content-Type: application/json; charset=utf-8' \
            // -H 'Accept: application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8' \
            // -H 'Authorization: yourtoken' \
            // -d '{"coordinates":[[8.681495,49.41461],[8.686507,49.41943],[8.687872,49.420318]],"maximum_speed":90}'
        }
    }
}