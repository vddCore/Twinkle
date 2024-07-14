namespace Twinkle.API.Logging.Sinks;

using System.Text;

public class StreamSink : Sink, IDisposable
{
    public Stream Stream { get; }
    public bool OwnsStream { get; }

    public StreamSink(Stream stream, bool ownsStream) 
    {
        Stream = stream;
        OwnsStream = ownsStream;
    }

    public void Dispose()
    {
        if (OwnsStream)
        {
            Stream.Dispose();
        }
    }

    public override void Write(LogMessage message)
    {
        Stream.Write(Encoding.UTF8.GetBytes(message.ToString()));
        Stream.Flush();
    }
}