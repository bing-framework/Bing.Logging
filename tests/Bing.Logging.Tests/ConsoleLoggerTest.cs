using Bing.Logging.Console;
using Xunit;
using Xunit.Abstractions;

namespace Bing.Logging.Tests
{
    /// <summary>
    /// 控制台日志操作 测试
    /// </summary>
    public class ConsoleLoggerTest : TestBase
    {
        /// <summary>
        /// 初始化一个<see cref="TestBase"/>类型的实例
        /// </summary>
        /// <param name="output">输出</param>
        public ConsoleLoggerTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// 测试 创建控制台提供程序以及日志级别
        /// </summary>
        [Fact]
        public void Test_Create_ConsoleProvider_With_Level()
        {
            var provider = new ConsoleLoggerProvider(LogLevel.Debug);
            var logger = provider.CreateLogger(this.GetType().FullName);

            Assert.False(logger.IsEnabled(LogLevel.Trace));
        }
    }
}
