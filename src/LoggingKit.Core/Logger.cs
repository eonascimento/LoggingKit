using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace LoggingKit.Core
{
    public class Logger
    {
        private static readonly ILogger _perfomaceLogger;
        private static readonly ILogger _usageLogger;
        private static readonly ILogger _errorLogger;
        private static readonly ILogger _diagnosticLogger;

        static Logger()
        {
            var pathprefix = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent.Parent.Parent.Parent.FullName,"Logs\\");

            _perfomaceLogger = new LoggerConfiguration()
                .WriteTo.File(path: $"{pathprefix}perfomace.txt")
                .CreateLogger();

            _usageLogger = new LoggerConfiguration()
                .WriteTo.File(path: $"{pathprefix}usage.txt")
                .CreateLogger();

            _errorLogger = new LoggerConfiguration()
                .WriteTo.File(path: $"{pathprefix}error.txt")
                .CreateLogger();

            _diagnosticLogger = new LoggerConfiguration()
                .WriteTo.File(path: $"{pathprefix}diagnostic.txt")
                .CreateLogger();
        }

        public static void WritePerfomace(LogDetail infoToLog)
        {
            _perfomaceLogger.Write(LogEventLevel.Information, "{@LogDetail}", infoToLog);
        }
        public static void WriteUsage(LogDetail infoToLog)
        {
            _usageLogger.Write(LogEventLevel.Information, "{@LogDetail}", infoToLog);
        }
        public static void WriteError(LogDetail infoToLog)
        {
            _errorLogger.Write(LogEventLevel.Information, "{@LogDetail}", infoToLog);
        }
        public static void WriteDiagnostic(LogDetail infoToLog, bool writeDiagnostic )
        {
            if (!writeDiagnostic)
                return;
            _diagnosticLogger.Write(LogEventLevel.Information, "{@LogDetail}", infoToLog);
        }

    }
}
