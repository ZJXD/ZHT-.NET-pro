using System;
using System.Collections.Generic;
using System.Text;

namespace DataType
{
    /// <summary>
    /// 文本格式化
    /// </summary>
    public class TextFormart
    {
        public TextFormart()
        {
        }

        /// <summary>
        /// 文本对齐
        /// </summary>
        public static void AlignText()
        {
            // 正数靠右，负数靠左对齐
            Console.WriteLine("The value |{0,10}|", 500);
            Console.WriteLine("The value |{0,-10}|", 500);
        }

        /// <summary>
        /// 对数值格式化
        /// </summary>
        public static void NumberFormart()
        {
            double num = 17502.5;
            int numInt = 124008;
            Console.WriteLine("Currency :{0:C}", num);
            Console.WriteLine("十进制:{0:D4}", numInt);   // 只能和整数配合使用
            Console.WriteLine("定点:{0:F3}", num);
            Console.WriteLine("常规:{0:G2}", num);
            Console.WriteLine("十六进制:{0:X}", numInt);   // 只能和整数配合使用
            Console.WriteLine("数字:{0:N2}", num);
            Console.WriteLine("百分比:{0:P2}", num);
            Console.WriteLine("往返过程:{0:R}", num);
            Console.WriteLine("科学计数法:{0:E5}", num);

            short sh1 = 1, sh2 = 2;
            int sh3 = sh1 + sh2;
        }
    }
}
