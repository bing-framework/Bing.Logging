using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Logging.Samples.Console.Demos
{
    /// <summary>
    /// NLog 关于Xml配置例子
    /// </summary>
    public class NLogXmlConfigureDemo
    {
        /// <summary>
        /// 日志工厂
        /// </summary>
        private readonly LoggerFactory _loggerFactory;

        /// <summary>
        /// 初始化一个<see cref="NLogXmlConfigureDemo"/>类型的实例
        /// </summary>
        public NLogXmlConfigureDemo()
        {
            _loggerFactory = new LoggerFactory();
            _loggerFactory.AddNLog();
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

        /// <summary>
        /// 写日志
        /// </summary>
        [Demo]
        public void WriteLog2()
        {
            var log = _loggerFactory.CreateLogger("ConsoleDemo");
            var menu = new ManualResetEvent(false);
            for (int i = 0; i < 1000; i++)
            {
                Task.Run(() =>
                {
                    menu.WaitOne();
                    log.Trace("Trace....");
                    log.Debug("Debug...");
                    log.Info("Information....");
                    log.Error("Error...");
                    log.Warn("Warning...");
                    log.Fatal("Fatal...");

                    var exception = new InvalidOperationException("Invalid value");
                    log.Error(exception);
                });
            }
            menu.Set();
            Thread.Sleep(2000);
        }
    }
}
