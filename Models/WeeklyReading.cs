using System;

namespace BloodGlucoseTracker.Models
{
    public class WeeklyStats
    {
        public string? WeekStartDate { get; set; }
        public double AvgFasting { get; set; }
        public double AvgPreMeal { get; set; }
        public double AvgPost1Hr { get; set; }
        public double AvgPost2Hr { get; set; }
        public int ReadingsCount { get; set; }
    }
}