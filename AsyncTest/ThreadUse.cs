using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace AsyncTest
{
    /// <summary>
    /// Thread 的使用（独享数据、上下文）
    /// 代码参考：https://www.cnblogs.com/edisonchou/p/4848131.html#undefined
    /// </summary>
    public class ThreadUse
    {
        /// <summary>
        /// 线程本地存储（Thread Local Storage,TLS），示例
        /// </summary>
        public static void ThreadUseEntrance()
        {
            //Console.WriteLine("开始测试数据插槽：");
            //Console.WriteLine("开始测试线程静态数据：" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("开始测试静态方法的同步：" + Thread.CurrentThread.ManagedThreadId);
            // 创建五个线程来同时运行，但是这里不适合用线程池，
            // 因为线程池内的线程会被反复使用导致线程ID一致
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(Lock.StaticIncrement);
                thread.Start();
            }

            // 等待上面静态方法同步 完成
            Thread.Sleep(5 * 1000);
            Console.WriteLine("-----------------------------------");

            Console.WriteLine("开始测试实例方法的同步：" + Thread.CurrentThread.ManagedThreadId);
            Lock l = new Lock();
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(l.InstanceIncrement);
                thread.Start();
            }
        }

        public static void MutexUseEntre()
        {
            //MutexUse.StartDo();
            SemaphoreUse.StartDo();
        }

    }

    /// <summary>
    /// 数据插槽使用
    /// </summary>
    public class ThreadDataSlot
    {
        // 分配一个数据插槽，注意插槽本身是全局可见的，因为这里分配是在所有线程的TLS内创建数据块
        private static LocalDataStoreSlot localSlot = Thread.AllocateDataSlot();

        // 线程要执行的方法，操作数据插槽所存储的数据
        public static void Work()
        {
            // 将线程的 ID 注册到数据插槽中，因为在一个应用程序中线程 ID 不会重复(线程池可能会重复)
            Thread.SetData(localSlot, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("线程{0}内的数据是：{1}", Thread.CurrentThread.ManagedThreadId.ToString(), Thread.GetData(localSlot).ToString());
            Thread.Sleep(1000);
            Console.WriteLine("线程{0}内的数据是：{1}", Thread.CurrentThread.ManagedThreadId.ToString(), Thread.GetData(localSlot).ToString());
        }
    }

    /// <summary>
    /// 线程静态数据
    /// </summary>
    public class ThreadStatic
    {
        // 值类型的线程静态数据
        [ThreadStatic]
        private static int threadId;

        // 引用类型的线程静态数据
        private static Ref refId = new Ref();
        //private static string refId;

        public static void Work()
        {
            threadId = Thread.CurrentThread.ManagedThreadId;
            refId.Id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("[线程{0}]：线程静态值变量：{1}，线程静态引用变量：{2}", Thread.CurrentThread.ManagedThreadId.ToString(), threadId, refId.Id);
            Thread.Sleep(1000);
            Console.WriteLine("[线程{0}]：线程静态值变量：{1}，线程静态引用变量：{2}", Thread.CurrentThread.ManagedThreadId.ToString(), threadId, refId.Id);
        }
    }

    public class Ref
    {
        private int id;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
    }

    /// <summary>
    /// 锁的使用：静态锁和实例锁
    /// </summary>
    public class Lock
    {
        // 应该完全避免使用this对象和当前类型对象作为同步对象，而应该在类型中定义私有的同步对象，
        // 同时应该使用lock而不是Monitor类型，这样可以有效地减少同步块不被释放的情况。


        // 静态方法同步锁
        private static object staticLocker = new object();

        // 实例方法同步锁
        private object instanceLocker = new object();

        // 成员变量
        private static int staticNumber = 0;
        private int instanceNumber = 0;

        // 静态方法同步
        public static void StaticIncrement(object state)
        {
            lock (staticLocker)
            {
                Console.WriteLine("当前线程ID：" + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("staticNumber 的值为：" + staticNumber);

                // 这里制造线程并行的机会，来检查同步的功能
                staticNumber++;
                Console.WriteLine("staticNumber 自增后的值为：" + staticNumber);
            }
        }

        // 实例方法同步
        public void InstanceIncrement(object state)
        {
            lock (instanceLocker)
            {
                Console.WriteLine("当前线程ID：" + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("instanceNumber 的值为：" + instanceNumber);

                // 这里制造线程并行的机会，来检查同步的功能
                instanceNumber++;
                Console.WriteLine("instanceNumber 自增后的值为：" + instanceNumber);
            }
        }
    }

    /// <summary>
    /// 互斥体
    /// </summary>
    public class MutexUse
    {
        const string testFile = "D:\\TestMutex.txt";
        /// <summary>
        /// 这个互斥体保证所有的进程都能得到同步
        /// </summary>
        static Mutex mutex = new Mutex(false, "TestMutex");

        public static void StartDo()
        {
            // 留出时间来启动其他进程
            Thread.Sleep(3000);
            DoWork();
            mutex.Close();
            Console.ReadKey();
        }

        /// <summary>
        /// 往文件里写连续的内容
        /// </summary>
        static void DoWork()
        {
            long d1 = DateTime.Now.Ticks;
            mutex.WaitOne();
            long d2 = DateTime.Now.Ticks;
            Console.WriteLine("经过了{0}个Tick后进程{1}得到互斥体，进入临界区代码。", (d2 - d1).ToString(), Process.GetCurrentProcess().Id.ToString());

            try
            {
                if (!File.Exists(testFile))
                {
                    FileStream fs = File.Create(testFile);
                    fs.Dispose();
                }
                for (int i = 0; i < 5; i++)
                {
                    // 每次都保证文件被关闭再重新打开
                    // 确定有mutex来同步，而不是IO机制
                    using (FileStream fs = File.Open(testFile, FileMode.Append))
                    {
                        string content = "【进程" + Process.GetCurrentProcess().Id.ToString() +
                            "】:" + i.ToString() + "\r\n";
                        Byte[] data = Encoding.Default.GetBytes(content);
                        fs.Write(data, 0, data.Length);
                    }
                    // 模拟做了其他工作
                    Thread.Sleep(300);
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }

    /// <summary>
    /// 信号量
    /// 1、一个防止他人进入的简单方法，就是门口加一把锁。先到的人锁上门，后到的人看到上锁，就在门口排队，等锁打开再进去。
    /// 这就叫"互斥锁"（Mutual exclusion，缩写 Mutex），防止多个线程同时读写某一块内存区域。
    /// 2、还有些房间，可以同时容纳n个人，比如厨房。也就是说，如果人数大于n，多出来的人只能在外面等着。
    /// 这好比某些内存区域，只能供给固定数目的线程使用。
    /// 3、这时的解决方法，就是在门口挂n把钥匙。进去的人就取一把钥匙，出来时再把钥匙挂回原处。
    /// 后到的人发现钥匙架空了，就知道必须在门口排队等着了。这种做法叫做"信号量"（Semaphore），用来保证多个线程不会互相冲突。
    /// </summary>
    public class SemaphoreUse
    {
        // 第一个参数指定当前有多少个“空位”（允许多少条线程进入）
        // 第二个参数指定一共有多少个“座位”（最多允许多少个线程同时进入）
        static Semaphore sem = new Semaphore(2, 2);

        const int threadSize = 4;

        public static void StartDo()
        {
            for (int i = 0; i < threadSize; i++)
            {
                Thread thread = new Thread(ThreadEntry);
                thread.Start(i + 1);
            }

            Console.ReadKey();
        }

        static void ThreadEntry(object id)
        {
            Console.WriteLine("线程{0}申请进入本方法", id);
            // WaitOne:如果还有“空位”，则占位，如果没有空位，则等待；
            sem.WaitOne();
            Console.WriteLine("线程{0}成功进入本方法", id);
            // 模拟线程执行了一些操作
            Thread.Sleep(100);
            Console.WriteLine("线程{0}执行完毕离开了", id);
            // Release:释放一个“空位”
            sem.Release();
        }
    }
}
