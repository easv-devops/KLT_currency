using Serilog;
using ILogger = Serilog.ILogger;

namespace Monitoring;

public static class MonitorService
{
    public static ILogger log => Serilog.Log.Logger;
    
    static MonitorService()
    {
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();
    }
}