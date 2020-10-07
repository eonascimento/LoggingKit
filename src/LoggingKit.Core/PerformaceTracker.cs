using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace LoggingKit.Core
{
    public class PerformaceTracker
    {
        private readonly Stopwatch _stopwatch;
        private readonly LogDetail _infoToLog;
        public PerformaceTracker(string name, string userId, string userName,
            string location, string product, string layer)
        {
            _stopwatch = Stopwatch.StartNew();
            _infoToLog = new LogDetail()
            {
                Message = name,
                UserId = userId,
                UserName = userName,
                Product = product,
                Layer = layer,
                Location = location,
                Hostname = Environment.MachineName
            };

            var beginTIme = DateTime.Now;
            _infoToLog.AdditionalInfo = new Dictionary<string, object>()
            {
                {"Started", beginTIme.ToString(CultureInfo.InvariantCulture) }
            };
        }

        public PerformaceTracker(string name, string userId, string userName,
           string location, string product, string layer,
           Dictionary<string,object> perfomaceParams)
            : this(name,userId,userName,location,product,layer)
        {
            foreach (var item in perfomaceParams)
            {
                _infoToLog.AdditionalInfo.Add($"input - {item.Key}", item.Value);
            }
        }
        public void Stop()
        {
            _stopwatch.Stop();
            _infoToLog.ElapsedMiliseconds = _stopwatch.ElapsedMilliseconds;
            Logger.WritePerfomace(_infoToLog);
        }

    }
}
