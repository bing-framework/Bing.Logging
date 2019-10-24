﻿using System;
using System.Collections.Generic;
using System.Text;
using Bing.Logging.Log4Net.Internal;

namespace Bing.Logging.Log4Net
{
    /// <summary>
    /// Log4Net 日志提供程序
    /// </summary>
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() => log4net.LogManager.Shutdown();

        /// <summary>
        /// 创建一个新的<see cref="ILogger"/>实例
        /// </summary>
        /// <param name="name">日志名称</param>
        public ILogger CreateLogger(string name) => new Log4NetLogger(log4net.LogManager.GetLogger("Default", name));
    }
}
