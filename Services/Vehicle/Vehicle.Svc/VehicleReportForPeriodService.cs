using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoPark.Svc.Infrastructure;
using Vehicle.Contract;
using Vehicle.Contract.Dto;
using Vehicle.Contract.Enums;

namespace AutoPark.Svc
{
    public class VehicleReportForPeriodService : IVehicleReportForPeriodService
    {
        private readonly VehicleContext _db;
        private readonly ITripService _tripService;

        public VehicleReportForPeriodService(VehicleContext db, ITripService tripService)
        {
            _db = db;
            _tripService = tripService;
        }
        
        public async Task<VehicleReportForPeriodResponseDto> GetReport(VehicleReportForPeriodRequestDto dto)
        {
            // get all points with time
            var points = await _tripService.GetTripPointsForReport(new TripRequestDto(dto.VehicleId, dto.StartTime, dto.EndTime));

            var res = CalculateResult(dto.Interval, points);

            res.VehicleId = dto.VehicleId;
            res.ReportType = dto.ReportType;
            res.Name = "VehicleReport";

            return res;
        }

        private VehicleReportForPeriodResponseDto CalculateResult(Interval interval, List<TrackPointDto> points)
        {
            VehicleReportForPeriodResponseDto report = new VehicleReportForPeriodResponseDto();
            
            switch (interval)
            {
                case Interval.Day:
                    report.Result = CalculateDayResult(points);
                    break;
                
                case Interval.Month:
                    report.Result = CalculateMonthResult(points);
                    break;
                
                case Interval.Year:
                    report.Result = CalculateYearResult(points);
                    break;
            }

            return report;
        }

        private List<string> CalculateYearResult(List<TrackPointDto> points)
        {
            var result = new List<string>();
            if (points.Count < 2)
                return result;

            var yearGpoup = points.GroupBy(p => p.TrackTime.Year.ToString())
                .ToDictionary(g => g.Key, g => g.ToList());
            foreach (var yearPoints in yearGpoup)
            {
                bool isFirst = true;
                if (yearPoints.Value.Count < 2)
                    continue;

                double distance = 0;
                bool isFirstPoint = true;
                TrackPointDto previousPoint = null;
                foreach (TrackPointDto yearPoint in yearPoints.Value)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        previousPoint = yearPoint;
                        continue;
                    }
                    
                    distance += CalculateDistance(yearPoint.Latitude, yearPoint.Longitude, previousPoint.Latitude, previousPoint.Longitude);
                    previousPoint = yearPoint;
                }
                
                result.Add($"Year: {yearPoints.Key} - {Math.Round(distance*1000)} km");
            }

            return result;
        }

        private List<string> CalculateMonthResult(List<TrackPointDto> points)
        {
            var result = new List<string>();
            if (points.Count < 2)
                return result;

            Dictionary<string, List<TrackPointDto>> monthGroup = points.GroupBy(p => p.TrackTime.ToString("yyyy-MM"))
                .ToDictionary(g => g.Key, g => g.ToList());
            Console.WriteLine(monthGroup.Count());
            foreach (var monthPoints in monthGroup)
            {
                bool isFirst = true;
                if (monthPoints.Value.Count < 2)
                    continue;

                double distance = 0;
                TrackPointDto previousPoint = null;
                foreach (var monthPoint in monthPoints.Value)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        previousPoint = monthPoint;
                        continue;
                    }
                    
                    distance += CalculateDistance(monthPoint.Latitude, monthPoint.Longitude, previousPoint.Latitude, previousPoint.Longitude);
                    previousPoint = monthPoint;
                }
                
                result.Add($"{monthPoints.Key} - {Math.Round(distance*1000)} km");
            }

            return result;
        }

        private List<string> CalculateDayResult(List<TrackPointDto> points)
        {
            var result = new List<string>();
            if (points.Count < 2)
                return result;

            Dictionary<string, List<TrackPointDto>> dayGroup = points.GroupBy(p => p.TrackTime.ToString("yyyy-MM-dd"))
                .ToDictionary(g => g.Key, g => g.ToList());
            foreach (var dayPoints in dayGroup)
            {
                bool isFirst = true;
                if (dayPoints.Value.Count < 2)
                    continue;

                double distance = 0;
                bool isFirstPoint = true;
                TrackPointDto previousPoint = null;
                foreach (TrackPointDto dayPoint in dayPoints.Value)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        previousPoint = dayPoint;
                        continue;
                    }
                    
                    distance += CalculateDistance(dayPoint.Latitude, dayPoint.Longitude, previousPoint.Latitude, previousPoint.Longitude);
                    previousPoint = dayPoint;
                }
                
                result.Add($"{dayPoints.Key} - {Math.Round(distance*1000)} km");
            }

            return result;
        }
        
        private static double CalculateDistance(string x1, string y1, string x2, string y2)
        {
            double deltaX = Double.Parse(x2) - Double.Parse(x1);
            double deltaY = Double.Parse(y2) - Double.Parse(y1);
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            
            return Math.Abs(distance);
        }
    }
}