using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AutoPark.Svc
{
    public class TrackGeneratorHostedService : BackgroundService
    {
        private readonly ITrackGeneratorHelper _generator;
        //todo - move to config
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(10);
        private static bool _isStart = true;

        public TrackGeneratorHostedService(ITrackGeneratorHelper generator)
        {
            _generator = generator;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!_isStart)
                    await _generator.GenerateTrack();
                else
                    _isStart = false;

                await Task.Delay(_delay, stoppingToken);
            }
        }
    }
}