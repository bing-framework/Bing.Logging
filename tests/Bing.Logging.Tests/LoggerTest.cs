using Bing.Logging.Console;
using Xunit;
using Xunit.Abstractions;

namespace Bing.Logging.Tests
{
    /// <summary>
    /// 日志操作 测试
    /// </summary>
    public class LoggerTest : TestBase
    {
        /// <summary>
        /// 初始化一个<see cref="TestBase"/>类型的实例
        /// </summary>
        /// <param name="output">输出</param>
        public LoggerTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// 测试 能否添加日志提供程序以及创建日志操作实例
        /// </summary>
        [Fact]
        public void Test_Can_AddProvider_Create_Logger()
        {
            var factory = new LoggerFactory();
            var provider1 = new ConsoleLoggerProvider(LogLevel.Error);
            var provider2 = new ConsoleLoggerProvider(LogLevel.Debug);

            factory.AddProvider(provider1, provider2);

            var logger = factory.CreateLogger(this.GetType().Name);
            Assert.True(logger.IsEnabled(LogLevel.Debug));
        }

        /// <summary>
        /// 测试 能否创建日志操作以及追加日志提供程序
        /// </summary>
        [Fact]
        public void Test_Can_Create_Logger_And_Append_Provider()
        {
            var factory = new LoggerFactory();
            var provider1 = new ConsoleLoggerProvider(LogLevel.Debug);
            factory.AddProvider(provider1);

            var logger = factory.CreateLogger<LoggerTest>();
            Assert.True(logger.IsEnabled(LogLevel.Debug));

            var provider2 = new ConsoleLoggerProvider(LogLevel.Warning);
            factory.AddProvider(provider2);
            Assert.True(logger.IsEnabled(LogLevel.Warning));
        }
    }
}
