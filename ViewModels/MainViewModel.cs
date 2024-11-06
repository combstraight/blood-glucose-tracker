using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BloodGlucoseTracker.Models;
using BloodGlucoseTracker.Services;
using CommunityToolkit.Mvvm.Input;

namespace BloodGlucoseTracker.ViewModels;

public class MainViewModel
{
    private readonly FileService _fileService;
    private string? _currentFilePath;
    public ObservableCollection<GlucoseReading> Readings { get; private set; }

    public ICommand OpenFileCommand { get; }
    public ICommand SaveFileCommand { get; }

    public MainViewModel()
    {
        _fileService = new FileService();
        Readings = new ObservableCollection<GlucoseReading>();

        OpenFileCommand = new RelayCommand(async () =>
        {
            var dialog = new OpenFileDialog
            {
                Filters = new List<FileDialogFilter>
                {
                    new FileDialogFilter
                    {
                        Name = "JSON files (*.json)|*.json",
                    }
                }
            };
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime
                {
                    MainWindow: not null
                } desktop)
            {
                var result = await dialog.ShowAsync(desktop.MainWindow);
                if (result != null && result.Length > 0)
                {
                    await LoadFile(result[0]);
                }
            }
        });

        SaveFileCommand = new RelayCommand(async () =>
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                var dialog = new SaveFileDialog
                {
                    DefaultExtension = "json",
                    Filters = new List<FileDialogFilter>
                    {
                        new FileDialogFilter
                        {
                            Name = "JSON files (*.json)|*.json",
                        }
                    }
                };
                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime
                    {
                        MainWindow: not null
                    } desktop)
                {
                    var result = await dialog.ShowAsync(desktop.MainWindow);
                    if (string.IsNullOrEmpty(result)) return;
                    _currentFilePath = result;

                }
            }
            await _fileService.SaveReadings(Readings, _currentFilePath);
        });
    }

    private async Task LoadFile(string path)
    {
        var loadedReadings = await _fileService.LoadReadings(path);
        Readings.Clear();
        foreach (var reading in loadedReadings)
        {
            Readings.Add(reading);
        }
        _currentFilePath = path;
    }
}