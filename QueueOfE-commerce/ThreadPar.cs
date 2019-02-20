using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueueOfE_commerce
{
    internal class ThreadPar
    {
        private static readonly int cnt = 10;
        private static readonly Queue<string> queueAll = new Queue<string>();
        //private static readonly Queue<string> queueCur = new Queue<string>();
        private static int SuccessCount;
        private Random r = new Random(1);

        public ThreadPar()
        {
            SuccessCount = 0;
            HandleQueue();
        }

        /// <summary>
        /// 购买
        /// </summary>
        /// <param name="uid">用户标识</param>
        /// <returns></returns>
        internal RetData Buy(string uid)
        {
            queueAll.Enqueue(uid);
            if (SuccessCount >= cnt)
            {
                return new RetData { Code = -1, Msg = "商品抢光了！", Cnt = 0 };
            }
            return new RetData { Code = 1, Msg = "正在处理订单中……" };
        }

        internal void HandleQueue()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (queueAll.Count > 0)
                    {
                        HandleOrder();
                    }
                }
            });
        }

        internal void HandleOrder()
        {
            while (SuccessCount < cnt && queueAll.Count > 0)
            {
                string uid;
                lock (this)
                {
                    uid = queueAll.Dequeue();
                }
                int n = r.Next(2);
                if (n == 1)
                {
                    SuccessCount++;
                    Console.WriteLine($"恭喜：{uid}，秒杀成功！，剩余商品数：{cnt - SuccessCount}");
                }
            }
        }
    }

    internal class RetData
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public int Cnt { get; set; }
    }
}
