using Xunit.Abstractions;

namespace Bing.Logging.Tests
{
    /// <summary>
    /// ���Ի���
    /// </summary>
    public class TestBase
    {
        /// <summary>
        /// ���
        /// </summary>
        protected ITestOutputHelper Output { get; }

        /// <summary>
        /// ��ʼ��һ��<see cref="TestBase"/>���͵�ʵ��
        /// </summary>
        /// <param name="output">���</param>
        public TestBase(ITestOutputHelper output) => Output = output;
    }
}
