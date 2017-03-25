using System;
using GAIT.Utilities.DI.Attributes;
using NLog;


namespace GAIT.Utilities.Logging
{
    [NoIoC]
    public static class LoggingFactory
    {
        public static ILogger Create<T>()
        {
            var type = typeof(T);
            var name = type.IsInterface ? type.Name.Substring(1) : type.Name;
            return LogManager.GetLogger(name);
        }
        public static ILogger Create(Type type)
        {
            var name = type.IsInterface ? type.Name.Substring(1) : type.Name;
            return LogManager.GetLogger(name);
        }
        public static ILogger Create(string name)
        {
            return LogManager.GetLogger(name);
        }
    }
}
