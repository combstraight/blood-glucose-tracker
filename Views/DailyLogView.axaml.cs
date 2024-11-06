using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BloodGlucoseTracker.Models;

namespace BloodGlucoseTracker.Views
{
    public partial class DailyLogView : UserControl
    {
        private ObservableCollection<GlucoseReading> _readings;
        
        public ObservableCollection<GlucoseReading> Readings 
        {
            get => _readings;
            set => _readings = value;
        }
    
        public DailyLogView()
        {
            InitializeComponent();
    
            _readings = new ObservableCollection<GlucoseReading>();
            DataContext = this;

            var grid = this.FindControl<DataGrid>("ReadingsGrid");
            if (grid != null)
            {
                grid.ItemsSource = App.MainViewModel.Readings;
            }
            
            Console.WriteLine($"Number of readings: {App.MainViewModel.Readings.Count}");  // Debug line
            
            if (App.MainViewModel.Readings.Count == 0)
            {
                App.MainViewModel.Readings.Add(new GlucoseReading 
                { 
                    Date = "",
                    Time = "",
                    FastingGlucose = null,
                    PreMealGlucose = null,
                    PostMeal1Hr = null,
                    PostMeal2Hr = null,
                    Meal = null,
                    ExerciseDone = null,
                    SleepHours = null,
                    Stress = null,
                    Notes = string.Empty
                });
            }
        }

        private void OnAddRow(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            App.MainViewModel.Readings.Add(new GlucoseReading 
            { 
                Date = "",
                Time = "",
                FastingGlucose = null,
                PreMealGlucose = null,
                PostMeal1Hr = null,
                PostMeal2Hr = null,
                Meal = null,
                ExerciseDone = null,
                SleepHours = null,
                Stress = null,
                Notes = string.Empty
            });
        }
    }
}