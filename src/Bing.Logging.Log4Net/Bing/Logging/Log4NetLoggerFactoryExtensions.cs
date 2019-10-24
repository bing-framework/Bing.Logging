using Bing.Logging.Log4Net;

namespace Bing.Logging
{
    /// <summary>
    /// Log4Net日志工厂(<see cref="ILoggerFactory"/>) 扩展
    /// </summary>
    public static class Log4NetLoggerFactoryExtensions
    {
        /// <summary>
        /// 添加Log4Net日志记录器
        /// </summary>
        /// <param name="factory">日志工厂</param>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory)
        {
            factory.AddProvider(new Log4NetLoggerProvider());
            return factory;
        }
    }
}
