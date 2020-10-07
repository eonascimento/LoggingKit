using LoggingKit.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace LoggingKit.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var builder = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json");

            //var configuration = builder.Build();

            var logDetail = GetLogDetail("Iniciando a Aplicação", null);
            Logger.WriteDiagnostic(logDetail,true);

            var tracker = new PerformaceTracker("LoggingConsoloe_execution", logDetail.UserId, logDetail.UserName, logDetail.Location, logDetail.Product, logDetail.Layer);

            try
            {
                var ex = new Exception("Algo ruim aconteceu");
                ex.Data.Add("parâmetro de entrada", "nada para ver aqui");
                throw ex;
            }
            catch (Exception ex)
            {
                logDetail = GetLogDetail("", ex);
                Logger.WriteError(logDetail);
            }

            logDetail = GetLogDetail("Usar detalhes do Log", null);
            Logger.WriteUsage(logDetail);

            logDetail = GetLogDetail("Parando a aplicação", null);
            Logger.WriteDiagnostic(logDetail,true);

            tracker.Stop();
        }

        private static LogDetail GetLogDetail(string message, Exception ex)
        {
            return new LogDetail
            {
                Product = "Logger",
                Location = "LoggerConsole", //Nome da Aplicação
                Layer = "Job", // Atividade executada
                UserName = Environment.UserName,
                Hostname = Environment.MachineName,
                Message = message,
                Exception = ex
            };
        }
    }
}
