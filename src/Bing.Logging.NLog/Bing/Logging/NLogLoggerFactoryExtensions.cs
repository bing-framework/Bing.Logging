using System.IO;
using System.Reflection;
using Bing.Logging.NLog;
using Bing.Logging.NLog.Internal;
using NLog;
using NLog.Config;

namespace Bing.Logging
{
    /// <summary>
    /// NLog日志工厂(<see cref="ILoggerFactory"/>) 扩展
    /// </summary>
    public static class NLogLoggerFactoryExtensions
    {
        /// <summary>
        /// 添加NLog日志记录器
        /// </summary>
        /// <param name="factory">日志工厂</param>
        /// <param name="fileName">配置文件名称</param>
        public static ILoggerFactory AddNLog(this ILoggerFactory factory, string fileName = InternalConst.DefaultConfigFile)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                LogManager.Configuration = new XmlLoggingConfiguration(fileName, true);
            LogManager.AddHiddenAssembly(typeof(NLogLoggerFactoryExtensions).GetTypeInfo().Assembly);
            using (var provider = new NLogLoggerProvider())
                factory.AddProvider(provider);
            return factory;
        }

        /// <summary>
        /// 添加NLog日志记录器
        /// </summary>
        /// <param name="factory">日志工厂</param>
        /// <param name="logFactory">NLog日志工厂</param>
        public static ILoggerFactory AddNLog(this ILoggerFactory factory, LogFactory logFactory)
        {
            factory.AddProvider(new NLogLoggerProvider(logFactory));
            return factory;
        }
    }
}
