using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BloodGlucoseTracker.Views;

namespace BloodGlucoseTracker
{
    public partial class MainWindow : Window
    {
        private readonly ContentControl? _contentArea;

        public MainWindow()
        {
            InitializeComponent();
            
            // Get reference to content area
            _contentArea = this.FindControl<ContentControl>("ContentArea");
            if (_contentArea != null) _contentArea.Content = new DailyLogView();

            // Set up button click handlers
            var stackPanel = this.FindControl<StackPanel>("NavPanel");
            if (stackPanel == null) return;
            foreach (var child in stackPanel.Children)
            {
                if (child is Button button)
                {
                    button.Click += NavigationButtonClick;
                }
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void NavigationButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            var viewName = button.Content?.ToString();

            if (_contentArea != null)
                _contentArea.Content = viewName switch
                {
                    "Daily Log" => new DailyLogView(),
                    "Weekly Log" => new WeeklyLogView(),
                    "Dashboard" => new DashboardView(),
                    _ => throw new ArgumentOutOfRangeException()
                };
        }
    }
}