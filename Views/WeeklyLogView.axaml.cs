using System;
using System.Linq;
using Avalonia.Controls;
using BloodGlucoseTracker.Models;

namespace BloodGlucoseTracker.Views;

public partial class WeeklyLogView : UserControl
{
 
    public WeeklyLogView()
    {
        InitializeComponent();
        
        var grid = this.FindControl<DataGrid>("WeeklyGrid");
        if (grid != null)
        {
            var weeklyStats = App.MainViewModel.Readings.GroupBy(r => r.Date != null ? GetWeekStart(r.Date) : default).Select(g => new WeeklyStats
            {
                WeekStartDate = g.Key,
                AvgFasting = g.Average(r => r.FastingGlucose ?? 0),
                AvgPreMeal = g.Average(r => r.PreMealGlucose ?? 0),
                AvgPost1Hr = g.Average(r => r.PostMeal1Hr ?? 0),
                AvgPost2Hr = g.Average(r => r.PostMeal2Hr ?? 0),
                ReadingsCount = g.Count(),
            });
            
            grid.ItemsSource = weeklyStats;
        }
    }

    private string GetWeekStart(string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            return date;
        }
        
        try
        {
            var dateTime = DateTime.Parse(date);
            Console.WriteLine(dateTime);
            var diff = (7 + (dateTime.DayOfWeek - DayOfWeek.Monday)) % 7;
            Console.WriteLine($"Days to subtract: {diff}");
            DateTime weekStart = dateTime.AddDays(-1 * diff).Date;
            Console.WriteLine($"Week start date: {weekStart:yyyy-MM-dd} ({weekStart.DayOfWeek})\n");
            return dateTime.AddDays(-1 * diff).ToString("MM/dd/yyyy");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing date: {date}. Error: {ex.Message}");
            return DateTime.Now.ToString();  // or handle error differently
        }
    }

  
}