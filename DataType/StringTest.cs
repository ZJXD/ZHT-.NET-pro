using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataType
{
    public class StringTest
    {
        public StringTest()
        {
        }

        public static void StartTest()
        {
            Console.WriteLine("Hello World!");

            string a = "abs";

            string b = a;
            string c = a;
            Console.WriteLine(a + "," + b + "," + c);

            string d = "567";
            c = d;
            Console.WriteLine(a + "," + b + "," + c + "," + d);

            c = "ese";

            Console.WriteLine(a + "," + b + "," + c + "," + d);

            GCHandle hander = GCHandle.Alloc(a);
            var pin = GCHandle.ToIntPtr(hander);
            Console.WriteLine("0 a memory:" + pin);

            ChangeString(a);

            hander = GCHandle.Alloc(a);
            pin = GCHandle.ToIntPtr(hander);
            Console.WriteLine("2 a memory:" + pin);
        }

        private static void ChangeString(string a)
        {
            GCHandle hander = GCHandle.Alloc(a);
            var pin = GCHandle.ToIntPtr(hander);
            Console.WriteLine("1 a memory:" + pin);
        }
    }
}
