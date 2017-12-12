using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public static class LogWrapper
    {
        public static string LogTemplate { get; set; } = "{Timestamp:yyyy-MMM-dd HH:mm:ss.fff zzz} [{Level}] {SourceContext:l} ({ThreadId}){Message}{NewLine}{Exception}";

        public static ILogger Logger { get; private set; }

        static LogWrapper()
        {
            Logger = new LoggerConfiguration()
               .WriteTo.RollingFile("logfile.txt", LogEventLevel.Information, outputTemplate: LogTemplate)
               .Enrich.With(new ThreadIdEnricher())
               .WriteTo.Console(LogEventLevel.Information, outputTemplate: LogTemplate)
               .Enrich.With(new ThreadIdEnricher())
                .CreateLogger();
        }
    }
}
