using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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
        }

        private void OnAddRow(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            App.MainViewModel.Readings.Add(new GlucoseReading 
            { 
                Date = DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                Time = DateTime.Now.ToString("t"),
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

        private void OnDeleteRow(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (!App.MainViewModel.Readings.Any())
            {
                return;
            }
            App.MainViewModel.Readings.RemoveAt(App.MainViewModel.Readings.Count() - 1);
        }
    }
}