using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bing.Logging.Samples.Console
{
    /// <summary>
    /// 例子
    /// </summary>
    public class DemoAttribute : Attribute
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 初始化一个<see cref="DemoAttribute"/>类型的实例
        /// </summary>
        public DemoAttribute() { }

        /// <summary>
        /// 初始化一个<see cref="DemoAttribute"/>类型的实例
        /// </summary>
        /// <param name="description">描述</param>
        public DemoAttribute(string description) => Description = description;
    }

    /// <summary>
    /// 例子执行器
    /// </summary>
    public static class DemoActuator
    {
        /// <summary>
        /// 空格
        /// </summary>
        private const string Space = "  ";

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="type">类型</param>
        public static void Execute(Type type)
        {
            var assembly = type.GetTypeInfo().Assembly;
            IList<ExecuteFunc> list = new List<ExecuteFunc>();
            foreach (var t in assembly.GetTypes())
            {
                var clazz = t.GetConstructor(Type.EmptyTypes);
                if (clazz == null)
                    continue;
                foreach (var method in t.GetMethods(BindingFlags.Public|BindingFlags.Instance))
                {
                    if (method.GetCustomAttributes(typeof(DemoAttribute), false).FirstOrDefault() is DemoAttribute
                        attribute)
                    {
                        object instance = Activator.CreateInstance(t);
                        ExecuteFunc func = new ExecuteFunc(instance, method, attribute.Description);
                        list.Add(func);
                    }
                }
            }

            if (list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                LrTag("选择用例", "-", 20);
                for (var i = 0; i < list.Count; i++)
                    sb.AppendLine($"[{i + 1}] {list[i]}");
                sb.AppendLine("\r\n[0] \texit.");
                var display = sb.ToString();

                System.Console.Out.WriteLine(ConsoleColor.Green, display);
                System.Console.Write("select>");
                var input = System.Console.ReadLine();
                while (input!="0"&&input!="quit"&&input!="q"&&input!="exit")
                {
                    if (input.Equals("cls", StringComparison.OrdinalIgnoreCase))
                        System.Console.Clear();
                    if (int.TryParse(input, out var idx))
                    {
                        if (idx > 0 && idx <= list.Count)
                        {
                            System.Console.Clear();
                            System.Console.Out.WriteLine(ConsoleColor.DarkCyan,list[idx-1]+" Running...");
                            list[idx - 1].Execute();
                            System.Console.Out.WriteLine(ConsoleColor.DarkCyan, list[idx - 1] + " Complete...");
                        }
                    }
                    System.Console.Out.WriteLine();
                    LrTag("选择用例", "-", 20);
                    System.Console.Out.WriteLine(ConsoleColor.Green, display);
                    System.Console.Write("select>");
                    input = System.Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// LR 标签
        /// </summary>
        /// <param name="view">视图</param>
        /// <param name="tag">标签</param>
        /// <param name="size">尺寸</param>
        private static void LrTag(string view, string tag, int size)
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < size; i++)
                sb.Append(tag);
            System.Console.Out.WriteLine(ConsoleColor.Yellow, sb + Space + view + Space + sb);
        }

        /// <summary>
        /// 写入并换行
        /// </summary>
        /// <param name="writer">文本写入器</param>
        /// <param name="color">颜色</param>
        /// <param name="format">格式化内容</param>
        /// <param name="args">格式化参数</param>
        private static void WriteLine(this TextWriter writer, ConsoleColor color, string format, params object[] args)
        {
            System.Console.ForegroundColor = color;
            writer.WriteLine(format,args);
            System.Console.ResetColor();
        }

        /// <summary>
        /// 执行函数
        /// </summary>
        private class ExecuteFunc
        {
            /// <summary>
            /// 实例
            /// </summary>
            private readonly object _instance;

            /// <summary>
            /// 方法
            /// </summary>
            private readonly MethodInfo _method;

            /// <summary>
            /// 描述
            /// </summary>
            private readonly string _description;

            /// <summary>
            /// 初始化一个<see cref="ExecuteFunc"/>类型的实例
            /// </summary>
            /// <param name="instance">实例</param>
            /// <param name="method">方法</param>
            /// <param name="description">描述</param>
            public ExecuteFunc(object instance, MethodInfo method, string description = "")
            {
                _instance = instance;
                _method = method;
                if (string.IsNullOrWhiteSpace(description))
                    _description = string.Concat("\t", instance.GetType().FullName, ".", method.Name);
                else
                    _description = string.Concat("\t", instance.GetType().FullName, "." + method.MemberType,
                        Environment.NewLine, "\t", description);
            }

            /// <summary>
            /// 执行
            /// </summary>
            public void Execute() => _method.Invoke(_instance, null);

            /// <summary>
            /// 输出字符串
            /// </summary>
            public override string ToString()
            {
                return _description;
            }
        }
    }
}
