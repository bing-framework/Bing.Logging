using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Bing.Logging.Samples.Console.Demos
{
    /// <summary>
    /// NLog Demo
    /// </summary>
    public class NLogDemo
    {
        /// <summary>
        /// 日志工厂
        /// </summary>
        private readonly LoggerFactory _loggerFactory;

        /// <summary>
        /// 初始化一个<see cref="NLogDemo"/>类型的实例
        /// </summary>
        public NLogDemo()
        {
            _loggerFactory = new LoggerFactory();
            // NLog 配置
            var config = new LoggingConfiguration();
            ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);
            var rule = new LoggingRule("*", global::NLog.LogLevel.Trace, consoleTarget);
            config.LoggingRules.Add(rule);
            var factory = new LogFactory(config);

            _loggerFactory.AddNLog(factory);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        [Demo]
        public void WriteLog()
        {
            var log = _loggerFactory.CreateLogger("NLogDemo");
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
