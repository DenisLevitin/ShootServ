using System.Configuration;
using System.IO;
using System.Web;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ShootServ
{
    public static class LoggerConfig
    {
        public static void RegisterLogger()
        {
            var configuration = new LoggerConfiguration();
            configuration.MinimumLevel.Debug();
            var path = Path.Combine( HttpRuntime.AppDomainAppPath, ConfigurationManager.AppSettings["LogFilePath"]);          
            configuration.WriteTo.File(path, levelSwitch: new LoggingLevelSwitch(LogEventLevel.Error));
            Log.Logger = configuration.CreateLogger();
        }
    }
}