using System;

namespace Bing.Logging.Samples.Console.Demos
{
    /// <summary>
    /// Log4Net Demo
    /// </summary>
    public class Log4NetDemo
    {
        /// <summary>
        /// 日志工厂
        /// </summary>
        private readonly LoggerFactory _loggerFactory;

        /// <summary>
        /// 初始化一个<see cref="Log4NetDemo"/>类型的实例
        /// </summary>
        public Log4NetDemo()
        {
            _loggerFactory = new LoggerFactory();
            _loggerFactory.AddLog4Net();
        }

        /// <summary>
        /// 写日志
        /// </summary>
        [Demo]
        public void WriteLog()
        {
            var log = _loggerFactory.CreateLogger("Log4NetDemo");
            log.Trace("Trace....");
            log.Debug("Debug...");
            log.Info("Information....");
            log.Error("Error...");
            log.Warn("Warning...");
            log.Fatal("Fatal...");

            var exception = new InvalidOperationException("Invalid value");
            log.Error(exception);
        }
    }
}
