using System.Linq;
using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace BloodGlucoseTracker.Views;

public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();
        SetupCharts();
    }

    private void SetupCharts()
    {
        var readings = App.MainViewModel.Readings.OrderBy(r => r.Date).ToList();
        var fastingChart = this.FindControl<CartesianChart>("FastingChart");
        if (fastingChart != null)
        {
            fastingChart.Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = readings.Select(r => r.FastingGlucose ?? 0).ToArray(),
                    Stroke = new SolidColorPaint(SKColors.LightBlue, 2),
                    GeometryStroke = new SolidColorPaint(SKColors.White, 2),
                    GeometrySize = 8,
                    Name = "Fasting Glucose"
                }
            };

            fastingChart.XAxes = new[]
            {
                new Axis
                {
                    Labels = readings.Select(r => r.Date ?? "0").ToArray(),
                    LabelsRotation = 45
                }
            };
        }
    }
}