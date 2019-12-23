using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// 字符串拼接对比
    /// </summary>
    public static class StringSplice
    {
        /// <summary>
        /// 测试1
        /// </summary>
        public static void StartSplice1()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string a = "1";

            for (int i = 0; i < 100000; i++)
            {
                a = a + "1";
            }

            watch.Stop();

            Console.WriteLine("+ 用时：" + watch.ElapsedMilliseconds + " ms");

            string b = "1";
            watch.Restart();

            for (int i = 0; i < 100000; i++)
            {
                b = string.Format("{0}{1}", b, "1");
            }

            watch.Stop();

            Console.WriteLine("Format 用时：" + watch.ElapsedMilliseconds + " ms");

            StringBuilder str = new StringBuilder();
            string charT = "1";
            watch.Restart();

            for (int i = 0; i < 100000; i++)
            {
                str.Append(charT);
            }

            watch.Stop();

            Console.WriteLine("StringBuilder 用时：" + watch.ElapsedMilliseconds + " ms");

            string c = "1";
            List<string> aggregate = new List<string>();

            for (int i = 0; i < 100000; i++)
            {
                aggregate.Add("1");
            }
            watch.Restart();
            c = aggregate.Aggregate((x, y) =>  x + "," + y);

            watch.Stop();

            Console.WriteLine("Aggregate 用时：" + watch.ElapsedMilliseconds + " ms");
        }

        /// <summary>
        /// 测试2
        /// </summary>
        public static void StartSplice2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string a = "1";

            for (int i = 0; i < 100000; i++)
            {
                a = a + "1" + "1" + "1" + "1" + "1" + "1" + "1" + "1" + "1" + "1";
            }

            watch.Stop();

            Console.WriteLine("+ 用时：" + watch.ElapsedMilliseconds + " ms");

            string b = "1";
            watch.Restart();

            for (int i = 0; i < 100000; i++)
            {
                b = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}", b, "1", "1", "1", "1", "1", "1", "1", "1", "1", "1");
            }

            watch.Stop();

            Console.WriteLine("Format 用时：" + watch.ElapsedMilliseconds + " ms");

            StringBuilder str = new StringBuilder();
            char charT = '1';
            watch.Restart();

            for (int i = 0; i < 100000; i++)
            {
                str.Append(charT, 10);
            }

            watch.Stop();

            Console.WriteLine("StringBuilder 用时：" + watch.ElapsedMilliseconds + " ms");
        }
    }
}
