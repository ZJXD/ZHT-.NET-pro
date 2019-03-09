using AsyncTest;
using DataType;
using SortAlgo;
using System;

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

            // 文本格式化测试
            //TextFormart.AlignText();
            //TextFormart.NumberFormart();

            // 测试字符串
            //StringTest.StartTest();

            // 日期测试
            //DataTimeHelper.GetLastMonth();

            // 测试异步
            MultipleVerAsync.ThreadNoLock();
            MultipleVerAsync.ThreadLock();

            Console.ReadKey();
        }
    }
}
