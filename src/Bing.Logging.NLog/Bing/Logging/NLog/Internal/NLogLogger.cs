using System;
using System.Globalization;
using NLogs = NLog;

namespace Bing.Logging.NLog.Internal
{
    /// <summary>
    /// NLog 日志操作
    /// </summary>
    internal class NLogLogger : ILogger
    {
        /// <summary>
        /// NLog 日志操作
        /// </summary>
        private NLogs.Logger _logger;

        /// <summary>
        /// 初始化一个<see cref="NLogLogger"/>类型的实例
        /// </summary>
        /// <param name="logger">NLog 日志操作</param>
        public NLogLogger(NLogs.Logger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 检查给定日志级别是否启用
        /// </summary>
        /// <param name="level">日志级别</param>
        public bool IsEnabled(LogLevel level) => _logger.IsEnabled(GetLevel(level));

        /// <summary>
        /// 获取日志级别
        /// </summary>
        /// <param name="level">日志级别</param>
        private NLogs.LogLevel GetLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    return NLogs.LogLevel.Trace;
                case LogLevel.Debug:
                    return NLogs.LogLevel.Debug;
                case LogLevel.Information:
                    return NLogs.LogLevel.Info;
                case LogLevel.Warning:
                    return NLogs.LogLevel.Warn;
                case LogLevel.Error:
                    return NLogs.LogLevel.Error;
                case LogLevel.Fatal:
                    return NLogs.LogLevel.Fatal;
                default:
                    return NLogs.LogLevel.Off;
            }
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常信息</param>
        public void Log(LogLevel level, string message, Exception exception)
        {
            if(!IsEnabled(level))
                return;
            if (string.IsNullOrWhiteSpace(message) && exception == null)
                return;
            var eventInfo = NLogs.LogEventInfo.Create(GetLevel(level), _logger.Name, exception,
                CultureInfo.CurrentCulture, message);
            _logger.Log(eventInfo);
        }
    }
}
