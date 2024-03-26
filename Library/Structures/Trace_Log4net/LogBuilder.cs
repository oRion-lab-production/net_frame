using Microsoft.Extensions.Logging;

namespace Structures.Trace_Log4net
{
    public static class LogBuilder
    {
        static LogBuilder() { }

        public static ILoggerFactory AddLog4net(this ILoggerFactory loggerFactory, string configFile, string configName)
        {
            loggerFactory.AddProvider(new LogProvider(configFile, configName));
            return loggerFactory;
        }
    }
}
