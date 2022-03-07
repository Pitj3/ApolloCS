using System.Diagnostics;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace Apollo.Core
{
    public static class Log
    {
        #region Internal Data
        private static ILog ALog => LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.ToString() ?? "Unknown");
        #endregion

        static Log()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        #region Public API
        public static void Info(object message)
        {
            ALog.Info(message);
        }

        public static void Warning(object message)
        {
            ALog.Warn(message);
        }

        public static void Error(object message)
        {
            ALog.Error(message);
        }

        public static void Fatal(object message)
        {
            ALog.Fatal(message);
            Debugger.Break();
        }

        public static void Debug(object message)
        {
            ALog.Debug(message);
        }
        #endregion
    }
}
