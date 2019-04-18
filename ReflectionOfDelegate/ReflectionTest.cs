using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ReflectionOfDelegate
{
    public class ReflectionTest
    {
        public static void Start()
        {
            // 目标示例
            var instance = new SubClass();

            // 反射得到的方法，相当于缓存了反射
            var method = typeof(SubClass).GetMethod(nameof(SubClass.Test), new[] { typeof(int) });

            // 将反射找到的方法创建一个委托
            var func = InstanceMethodBuilder<int, int>.CreateInstanceMethod(instance, method);

            // 和被测试方法一样的纯委托
            int pureFunc(int value) => value;

            // 测试次数
            int count = 10000000;

            Stopwatch watch = new Stopwatch();
            // 直接调用
            watch.Start();
            for (int i = 0; i < count; i++)
            {
                instance.Test(i);
            }
            watch.Stop();
            Console.WriteLine($"{watch.Elapsed} - {count}次 - 直接调用");

            // 使用同样功能的Func
            watch.Restart();
            for (int i = 0; i < count; i++)
            {
                pureFunc(i);
            }
            watch.Stop();
            Console.WriteLine($"{watch.Elapsed} - {count}次 - 同样功能的 Func 调用");

            // 使用反射得到的委托调用
            watch.Restart();
            for (int i = 0; i < count; i++)
            {
                func(i);
            }
            watch.Stop();
            Console.WriteLine($"{watch.Elapsed} - {count}次 - 反射得到的委托调用");

            // 使用反射得到的方法缓存调用
            watch.Restart();
            for (int i = 0; i < count; i++)
            {
                method?.Invoke(instance, new object[] { i });
            }
            watch.Stop();
            Console.WriteLine($"{watch.Elapsed} - {count}次 - 反射得到的方法缓存调用");

            // 直接使用反射调用
            watch.Restart();
            for (int i = 0; i < count; i++)
            {
                typeof(SubClass).GetMethod(nameof(SubClass.Test), new[] { typeof(int) })?.Invoke(instance, new object[] { i });
            }
            watch.Stop();
            Console.WriteLine($"{watch.Elapsed} - {count}次 - 直接反射调用");
        }

        private class SubClass
        {
            public int Test(int i)
            {
                return i;
            }
        }
    }
}
