using System;
using System.Threading.Tasks;

namespace QueueOfE_commerce
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var thread = new ThreadPar();

            //for (int i = 0; i < 100; i++)
            //{
            //    string uid = "用户" + i;
            //    RetData retData = thread.Buy(uid);
            //    if (retData.Code == -1)
            //    {
            //        Console.WriteLine(uid + "：" + retData.Msg);
            //    }
            //    else
            //    {
            //        Console.WriteLine(uid + "：" + retData.Msg + "，还剩下" + retData.Cnt + "件。");
            //    }
            //}

            Parallel.For(0, 100, (t, state) =>
              {
                  string uid = "用户" + t;
                  RetData retData = thread.Buy(uid);
                  if (retData.Code == -1)
                  {
                      Console.WriteLine(uid + "：" + retData.Msg);
                  }
                  else if (retData.Code == 1)
                  {
                      Console.WriteLine(uid + "：" + retData.Msg);
                  }
                  else
                  {
                      Console.WriteLine(uid + "：" + retData.Msg + "，还剩下" + retData.Cnt + "件。");
                  }
              });

            Console.WriteLine("----------操作完成----------");
            Console.ReadKey();
        }
    }
}
