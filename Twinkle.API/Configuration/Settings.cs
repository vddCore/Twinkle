namespace Twinkle.API.Configuration;

using System.ComponentModel;
using System.Text.Json;

public class Settings<T> where T: INotifyPropertyChanged, new()
{
    public string FilePath { get; }
    public T Model { get; private set; } = default!;

    internal Settings(string filePath)
    {
        FilePath = filePath;
        
        Load();
        Model.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Save();
    }

    public void Load()
    {
        try
        {
            using (var sr = new StreamReader(FilePath))
            {
                Model = JsonSerializer.Deserialize<T>(sr.ReadToEnd())!;
            }
        }
        catch (Exception)
        {
            Model = new();
            Save();
        }
    }

    public void Save()
    {
        using (var fs = new FileStream(FilePath, FileMode.Create))
        {
            JsonSerializer.Serialize(fs, Model);
        }
    }
}