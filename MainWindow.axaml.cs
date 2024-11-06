using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BloodGlucoseTracker.Views;

namespace BloodGlucoseTracker
{
    public partial class MainWindow : Window
    {
        private readonly ContentControl _contentArea;

        public MainWindow()
        {
            InitializeComponent();
            
            // Get reference to content area
            _contentArea = this.FindControl<ContentControl>("ContentArea");

            // Set up button click handlers
            var stackPanel = this.FindControl<StackPanel>("NavPanel");
            if (stackPanel == null) return;
            foreach (var child in stackPanel.Children)
            {
                if (child is Button button)
                {
                    button.Click += NavigationButton_Click;
                }
            }

            // Start with empty content for now
            // We'll add views as we create them
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void NavigationButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            var viewName = button.Content?.ToString();

            _contentArea.Content = viewName switch
            {
                "Daily Log" => new DailyLogView(),
                "Weekly Log" => new WeeklyLogView(),
                _ => new TextBlock
                {
                    Text = $"Selected: {viewName}",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                }
            };
        }
    }
}