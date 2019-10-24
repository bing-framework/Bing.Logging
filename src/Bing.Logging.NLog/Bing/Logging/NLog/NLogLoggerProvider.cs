using Bing.Logging.NLog.Internal;
using NLog;

namespace Bing.Logging.NLog
{
    /// <summary>
    /// NLog 日志提供程序
    /// </summary>
    public class NLogLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// NLog 日志工厂
        /// </summary>
        private readonly LogFactory _factory;

        /// <summary>
        /// 是否已释放
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// 初始化一个<see cref="NLogLoggerProvider"/>类型的实例
        /// </summary>
        public NLogLoggerProvider() { }

        /// <summary>
        /// 初始化一个<see cref="NLogLoggerProvider"/>类型的实例
        /// </summary>
        /// <param name="factory">NLog 日志工厂</param>
        public NLogLoggerProvider(LogFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_factory != null && !_disposed)
            {
                _factory.Flush();
                _factory.Dispose();
                _disposed = true;
            }
        }

        /// <summary>
        /// 创建一个新的<see cref="ILogger"/>实例
        /// </summary>
        /// <param name="name">日志名称</param>
        public ILogger CreateLogger(string name)
        {
            if (_factory == null)
            {
                // 使用：XmlLoggingConfiguration
                // 例如：LogManager.Configuration = new XmlLoggingConfiguration(fileName, true);
                return new NLogLogger(LogManager.GetLogger(name));
            }
            return new NLogLogger(_factory.GetLogger(name));
        }
    }
}
