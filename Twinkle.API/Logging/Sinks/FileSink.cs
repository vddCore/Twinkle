namespace Twinkle.API.Logging.Sinks;

public sealed class FileSink : StreamSink
{
    private int MaximumRecentLogFiles { get; }
    
    public DirectoryInfo Directory { get; }
    public string FileName { get; }
    

    public FileSink(string directory, string fileName, int maximumRecentLogFiles = 5)
        : base(new FileStream(Path.Combine(directory, fileName), FileMode.Create), true)
    {
        Directory = new DirectoryInfo(directory);
        FileName = fileName;
        MaximumRecentLogFiles = maximumRecentLogFiles;

        RemoveOldFilesIfNeeded();
    }
    
    private void RemoveOldFilesIfNeeded()
    {
        var logFiles = Directory.GetFiles("*.log");

        if (logFiles.Length < MaximumRecentLogFiles)
            return;
            
        var toRemove = logFiles
            .OrderByDescending(x => x.CreationTime)
            .Skip(MaximumRecentLogFiles);
        
        foreach (var file in toRemove) 
            file.Delete();
    }
}