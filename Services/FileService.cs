using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using BloodGlucoseTracker.Models;

namespace BloodGlucoseTracker.Services;

public class FileService
{
    private const string LastFilePath = "LastFilePath";

    private readonly string _settingsPath = "settings.json";

    public class Settings
    {
        public string LastFilePath { get; set; }
    }

    public async Task SaveReadings(ObservableCollection<GlucoseReading?> readings, string filePath)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize(readings, options);
            await File.WriteAllTextAsync(filePath, json);
            SaveLastFilePath(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }

    public async Task<ObservableCollection<GlucoseReading>> LoadReadings(string filePath)
    {
        if (!File.Exists(filePath)) return new ObservableCollection<GlucoseReading>();
        var json = await File.ReadAllTextAsync(filePath);
        var readings = JsonSerializer.Deserialize<ObservableCollection<GlucoseReading>>(json);
        SaveLastFilePath(filePath);
        return readings ?? new ObservableCollection<GlucoseReading>();

    }

    public class LoadResult
    {
        public ObservableCollection<GlucoseReading> Readings { get; set; }
        public string LastFilePath { get; set; }
    }
    
    public async Task<LoadResult> LoadFromLastFilePath()
    {
       
        var json = File.ReadAllText(_settingsPath);
        var filePath = JsonSerializer.Deserialize<Settings>(json)?.LastFilePath;
        var readings = string.IsNullOrEmpty(filePath) ? new ObservableCollection<GlucoseReading>() : await LoadReadings(filePath);
        return new LoadResult
        {
            Readings = readings,
            LastFilePath = filePath ?? string.Empty
        };
    }

    public async Task SaveSettingsAsync(ObservableCollection<GlucoseReading> readings, string path)
    {
        var json = JsonSerializer.Serialize(readings);
        await File.WriteAllTextAsync(path, json);
        SaveLastFilePath(path);
    }

    private void SaveLastFilePath(string filePath)
    {
        try
        {
            var settings = new Dictionary<string, string>
            {
                { LastFilePath, filePath }
            };
            File.WriteAllText(_settingsPath, JsonSerializer.Serialize(settings));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving settings: {ex.Message}");
        }
    }

  
}