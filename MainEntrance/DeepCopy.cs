using System;
using System.Collections.Generic;
using System.Text;

namespace MainEntrance
{
    public class DeepCopy : ICloneable
    {

        public int[] s = { 1, 2, 3, 4 };
        public DeepCopy()
        {
        }
        private DeepCopy(int[] s)
        {
            this.s = (int[])s.Clone();
        }
        public Object Clone()
        {
            // 构造一个新的DeepCopy对象
            return new DeepCopy(this.s);
        }
        public void Display()
        {

            foreach (int i in s)
            {
                Console.Write(i + ", ");
            }
            Console.WriteLine();
        }
    }
}
