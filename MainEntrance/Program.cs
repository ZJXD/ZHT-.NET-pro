using AsyncTest;
using DataType;
using SortAlgo;
using System;
using System.Collections.Generic;
using Utils;

namespace MainEntrance
{
    class Program
    {
        // 作为测试的主入口
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // 排序测试
            //int[] nums = { 2, 6, 4, 8, 3, 1, 9, 5 };
            //int[] nums = { 3, 44, 38, 5, 47, 15, 36, 26, 27, 2, 46, 4, 19, 50, 48 };
            //int n = 0;
            //nums.QuickSort(0, 14, n);
            //nums.ShellSort();
            //List<int> nums = new List<int>();
            //Random random = new Random();
            //for (int i = 0; i < 200000; i++)
            //{
            //    nums.Add(random.Next(1,1000000));
            //}
            //nums.QuickSort();

            //int count = nums.Count;


            // 文本格式化测试
            //TextFormart.AlignText();
            //TextFormart.NumberFormart();

            // 测试字符串
            //StringTest.StartTest();

            // 日期测试
            //DataTimeHelper.GetLastMonth();

            // 测试异步
            //MultipleVerAsync.ThreadNoLock();
            //MultipleVerAsync.ThreadLock();

            // 测试线程
            //ThreadUse.ThreadUseEntrance();
            //ThreadUse.MutexUseEntre();

            //int[] temp = { 1, 2, 3, 4 };
            //DeepCopy deepCopy = new DeepCopy();
            //DeepCopy clone = deepCopy.Clone() as DeepCopy;
            //clone.s[2] = 10;
            //Console.WriteLine($"{deepCopy.s[2]},{clone.s[2]}");

            // 字符串拼接测试
            StringSplice.StartSplice1();

            Console.ReadKey();
        }
    }
}
