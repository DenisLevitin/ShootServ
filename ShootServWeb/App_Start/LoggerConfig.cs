using System.Configuration;
using Serilog;

namespace ShootServ
{
    public static class LoggerConfig
    {
        public static void RegisterLogger()
        {
            var configuration = new LoggerConfiguration();
            configuration.MinimumLevel.Debug();
            configuration.WriteTo.File(ConfigurationManager.AppSettings["LogFilePath"]);
            Log.Logger = configuration.CreateLogger();
        }
    }
}