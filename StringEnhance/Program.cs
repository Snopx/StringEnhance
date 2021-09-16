using System;
using System.Reflection;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace StringEnhance
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchMarkTest>();

            //BenchMarkTest test = new();
            //test.StringCreateUse();
        }
    }

    [MemoryDiagnoser()]
    public class BenchMarkTest
    {
        private const string TestValue = "sdfsdfsdfsfdsdf";


        [Benchmark]
        public string StringPlusUse()
        {
            var first = TestValue.Substring(0, 3);
            var lenSubRest = TestValue.Length - 3;
            for (var i = 0; i < lenSubRest; i++)
            {
                first += "*";
            }
            return first;
        }

        [Benchmark]
        public string StringBuilderUse()
        {
            var first = TestValue.Substring(0, 3);
            var lenSubRest = TestValue.Length - 3;
            var strBuilder = new StringBuilder(first);
            for (var i = 0; i < lenSubRest; i++)
            {
                strBuilder.Append('*');
            }

            return strBuilder.ToString();
        }
        [Benchmark]
        public string StringNewUse()
        {
            var first = TestValue.Substring(0, 3);
            var lenSubRest = TestValue.Length - 3;
            return first + new string('*', lenSubRest);
        }

        [Benchmark]
        public string StringCreateUse()
        {
            return string.Create(TestValue.Length, TestValue, (sp, v) =>
            {
                v.AsSpan().CopyTo(sp);
                sp[3..].Fill('*');
            });
        }
    }
}