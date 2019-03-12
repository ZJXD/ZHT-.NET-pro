using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadState = System.Threading.ThreadState;

namespace AsyncTest
{
    /// <summary>
    /// 多版本异步使用
    /// </summary>
    public class MultipleVerAsync
    {
        /// <summary>
        /// 异步1：这个比较老，.NET Core 不支持
        /// </summary>
        public static void ActionAsync()
        {
            //委托异步多线程
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine($"开始执行了,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")},,,,{Thread.CurrentThread.ManagedThreadId}");
            Action<string> act = DoSomethingLong;
            for (int i = 0; i < 5; i++)
            {
                //int ThreadId = Thread.CurrentThread.ManagedThreadId;//获取当前线程ID
                string name = $"Async {i}";
                act.BeginInvoke(name, null, null);      // 该平台在这不支持
            }
            watch.Stop();
            Console.WriteLine($"结束执行了,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")},,,,{watch.ElapsedMilliseconds}");
        }

        /// <summary>
        /// 一个比较耗时的方法,循环1000W次
        /// </summary>
        /// <param name="name"></param>
        public static void DoSomethingLong(string name)
        {
            int iResult = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                iResult += i;
            }
            Console.WriteLine($"********************{name}*******{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}****{Thread.CurrentThread.ManagedThreadId}****");
        }

        /// <summary>
        /// Thread 异步
        /// </summary>
        public static void ThreadAsync()
        {
            // Thread
            // Thread 默认属于前台线程，启动后必须完成
            // Thread 有很多Api，但大多数都已经不用了
            Console.WriteLine("Thread多线程开始了");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            //线程容器
            List<Thread> list = new List<Thread>();
            for (int i = 0; i < 5; i++)
            {
                //int ThreadId = Thread.CurrentThread.ManagedThreadId;//获取当前线程ID
                string name = $"Async {i}";
                void start1()
                {
                    DoSomethingLong(name);
                }
                Thread thread = new Thread(start1)
                {
                    IsBackground = true //设置为后台线程，关闭后立即退出
                };
                thread.Start();
                list.Add(thread);
                // thread.Suspend();    //暂停，已经不用了
                // thread.Resume();     //恢复，已经不用了
                // thread.Abort();      //销毁线程
                // 停止线程靠的不是外部力量，而是线程自身，外部修改信号量，线程检测信号量
            }
            // 判断当前线程状态，来做线程等待
            while (list.Count(t => t.ThreadState != ThreadState.Stopped) > 0)
            {
                Console.WriteLine("等待中....."); Thread.Sleep(500);
            }
            // 统计正确全部耗时，通过join来做线程等待
            foreach (var item in list)
            {
                item.Join();    // 线程等待，表示把thread线程任务join到当前线程，也就是当前线程等着thread任务完成
                                // 等待完成后统计时间
            }
            watch.Stop();

            Console.WriteLine($"结束执行了,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")},,,,{watch.ElapsedMilliseconds}");
        }

        /// <summary>
        /// 回调封装，无返回值
        /// </summary>
        /// <param name="start"></param>
        /// <param name="callback"></param>
        private static void ThreadWithCallback(ThreadStart start, Action callback)
        {
            void nweStart()
            {
                start.Invoke();
                callback.Invoke();
            }
            Thread thread = new Thread(nweStart);
            thread.Start();
        }

        /// <summary>
        /// Thread 异步，通过回调
        /// </summary>
        public static void ThreadAsyncByCallback()
        {
            // 回调的委托
            void act()
            {
                Console.WriteLine("回调函数");
            }
            //要执行的异步操作
            void start()
            {
                Console.WriteLine("正常执行。。");
            }
            ThreadWithCallback(start, act);
        }

        /// <summary>
        /// 回调封装，有返回值
        /// 想要获取返回值，必须要有一个等待的过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private static Func<T> ThreadWithReturn<T>(Func<T> func)
        {
            T t = default(T);
            // ThreadStart本身也是一个委托
            void start()
            {
                t = func.Invoke();
            }
            // 开启一个子线程
            Thread thread = new Thread(start);
            thread.Start();
            // 返回一个委托，因为委托本身是不执行的，所以这里返回出去的是还没有执行的委托
            // 等在获取结果的地方，调用该委托
            return () =>
            {
                // 只是判断状态的方法
                while (thread.ThreadState != ThreadState.Stopped)
                {
                    Thread.Sleep(500);
                }
                // 使用线程等待
                // thread.Join();
                // 以上两种都可以
                return t;
            };
        }

        /// <summary>
        /// Thread 异步，通过有返回值的回调
        /// </summary>
        public static void ThreadAsyncByReturn()
        {
            Func<int> func = () =>
            {
                Console.WriteLine("正在执行。。。");
                Thread.Sleep(10000);
                Console.WriteLine("执行结束。。。");
                return DateTime.Now.Year;
            };
            Func<int> newfunc = ThreadWithReturn(func);
            // 这里为了方便测试，只管感受回调的执行原理
            Console.WriteLine("Else.....");
            Thread.Sleep(100);
            Console.WriteLine("Else.....");
            Thread.Sleep(100);
            Console.WriteLine("Else.....");
            // 执行回调函数里return的委托获取结果
            // newfunc.Invoke();
            Console.WriteLine($"有参数回调函数的回调结果：{newfunc.Invoke()}");
        }

        /// <summary>
        /// ThreadPool 异步
        /// </summary>
        public static void ThreadPoolAyscn()
        {
            ThreadPool.QueueUserWorkItem(p =>
            {
                // 这里的p是没有值的
                Console.WriteLine("没有参数：" + p + ",1等待中……");
                Thread.Sleep(2000);
                Console.WriteLine($"线程池线程1,{Thread.CurrentThread.ManagedThreadId}");
            });
            ThreadPool.QueueUserWorkItem(p =>
            {
                // 这里的p就是传进来的值
                Console.WriteLine("有参数：" + p + ",2等待中……");
                Thread.Sleep(2000);
                Console.WriteLine($"线程池线程2，带参数,{Thread.CurrentThread.ManagedThreadId}");
            }, "这里是参数");

            int workerThreads = 0;
            int completionPortThreads = 0;
            // 设置线程池的最大线程数量（普通线程，IO线程）
            ThreadPool.SetMaxThreads(80, 80);
            // 设置线程池的最小线程数量(普通线程，IO线程)
            ThreadPool.SetMinThreads(8, 8);
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"当前可用最大普通线程:{workerThreads}，IO:{completionPortThreads}");
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"当前可用最小普通线程：{workerThreads},IO:{completionPortThreads}");

            // 用来控制线程等待,false默认为关闭状态
            ManualResetEvent mre = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(p =>
            {
                DoSomethingLong("控制线程等待");
                Console.WriteLine($"线程池线程3，带参数,{Thread.CurrentThread.ManagedThreadId}");
                // 通知线程，修改信号为true
                mre.Set();
            });
            // 阻止当前线程，直到等到信号为true在继续执行
            mre.WaitOne();
            // 关闭线程，相当于设置成false
            mre.Reset();
            Console.WriteLine("信号被关闭了");
            ThreadPool.QueueUserWorkItem(p =>
            {
                Console.WriteLine("再次等待");
                mre.Set();
            });
            mre.WaitOne();
        }

        /// <summary>
        /// Task 异步使用
        /// </summary>
        public static void TaskAyscn()
        {
            //Task.Factory.StartNew:创建一个新的线程，Task的线程也是从线程池中拿的（ThreadPool）

            //Task.WaitAny:等待一群线程中的其中一个完成，这里是卡主线程，一直等到一群线程中的最快的一个完成，才能继续往下执行（20年前我也差点被后面的给追上），打个简单的比方：从三个地方获取配置信息（数据库，config，IO），同时开启三个线程来访问，谁快我用谁。

            //Task.WaitAll:等待所有线程完成，这里也是卡主线程，一直等待所有子线程完成任务，才能继续往下执行。

            //Task.ContinueWhenAny:回调形式的，任意一个线程完成后执行的后续动作，这个就跟WaitAny差不多，只不是是回调形式的。

            //Task.ContinueWhenAll:回调形式的，所有线程完成后执行的后续动作，理解同上


            // 线程容器 
            List<Task> taskList = new List<Task>();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("************Task Begin**************");
            // 启动5个线程
            for (int i = 0; i < 5; i++)
            {
                string name = $"Task:{i}";
                Task task = Task.Factory.StartNew(() =>
                {
                    DoSomethingLong(name);
                });
                taskList.Add(task);
            }
            // 回调形式的，任意一个完成后执行的后续动作
            Task any = Task.Factory.ContinueWhenAny(taskList.ToArray(), task =>
            {
                Console.WriteLine("ContinueWhenAny");
            });
            // 回调形式的，全部任务完成后执行的后续动作
            Task all = Task.Factory.ContinueWhenAll(taskList.ToArray(), tasks =>
            {
                Console.WriteLine($"ContinueWhenAll{tasks.Length}");
            });
            // 把两个回调也放到容器里面，包含回调一起等待
            taskList.Add(any);
            taskList.Add(all);
            // WaitAny:线程等待，当前线程等待其中一个线程完成
            Task.WaitAny(taskList.ToArray());
            Console.WriteLine("WaitAny");

            // WaitAll:线程等待，当前线程等待所有线程的完成
            Console.WriteLine("WaitAll");
            Task.WaitAll(taskList.ToArray());
            watch.Stop();
            Console.WriteLine($"************Task End**************{watch.ElapsedMilliseconds},{Thread.CurrentThread.ManagedThreadId}");
        }

        /// <summary>
        /// Parallel（并行编程） 异步
        /// </summary>
        public static void ParallelAsync()
        {
            // 问：首先是为什么叫做并行编程，跟Task有什么区别？

            // 答：使用Task开启子线程的时候，主线程是属于空闲状态，
            // 并不参与执行（我是领导，有5件事情需要处理，我安排了5个手下去做，而我本身就是观望状态 PS：到底是领导。），
            // Parallel开启子线程的时候，主线程也会参与计算（我是领导，我有5件事情需要处理，我身为领导，但是我很勤劳，
            // 所以我做了一件事情，另外四件事情安排4个手下去完成），很明显，减少了开销。

            // 你以为Parallel就只有这个炫酷的功能？大错特错，更炫酷的还有；
            // Parallel可以控制线程的最大并发数量，啥意思？
            // 就是不管你是脑溢血，还是心脏病，还是动脉大出血，我的手术室只有2个，同时也只能给两个人做手术，
            // 做完一个在进来一个，同时做完两个，就同时在进来两个，多了不行。

            // 当前，你想使用Task，或者ThreadPool来实现这样的效果也是可以的，不过这就需要你动动脑筋了，
            // 当然，有微软给封装好的接口直接使用，岂不美哉？


            // 并行编程 
            Console.WriteLine($"*************Parallel start********{Thread.CurrentThread.ManagedThreadId}****");
            // 一次性执行1个或多个线程，效果类似：Task WaitAll，只不过Parallel的主线程也参与了计算
            Parallel.Invoke(
            () => { DoSomethingLong("Parallel-1:`1"); },
            () => { DoSomethingLong("Parallel-1:`2"); },
            () => { DoSomethingLong("Parallel-1:`3"); },
            () => { DoSomethingLong("Parallel-1:`4"); },
            () => { DoSomethingLong("Parallel-1:`5"); });
            // 定义要执行的线程数量
            Parallel.For(0, 5, t =>
            {
                int a = t;
                DoSomethingLong($"Parallel-2:`{a}");
            });

            {
                ParallelOptions options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 3 //执行线程的最大并发数量,执行完成一个，就接着开启一个
                };
                // 遍历集合，根据集合数量执行线程数量
                Parallel.ForEach(new List<string> { "a", "b", "c", "d", "e", "f", "g" }, options, t =>
                {
                    DoSomethingLong($"Parallel-3:`{t}");
                });
            }

            {
                ParallelOptions options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 3 // 执行线程的最大并发数量,执行完成一个，就接着开启一个
                };
                // 遍历集合，根据集合数量执行线程数量
                Parallel.ForEach(new List<string> { "a", "b", "c", "d", "e", "f", "g" }, options, (t, status) =>
                {
                    // status.Break();  // 这一次结束。
                    // status.Stop();   // 整个Parallel结束掉，Break和Stop不可以共存
                    DoSomethingLong($"Parallel-4:`{t}");
                });
            }
            Console.WriteLine("*************Parallel end************");
        }

        /// <summary>
        /// 线程异常
        /// </summary>
        public static void ThreadException()
        {
            // 关于线程取消这块呢，要记住一点，线程只能自身终结自身，也就是除非我自杀，否则你干不掉我，
            // 不要妄想通过主线程来控制计算中的子线程。

            // 关于线程异常处理这块呢，想要捕获子线程的异常，最好在子线程本身抓捕，也可以使用Task的WaitAll，
            // 不过这种方法不推荐，如果用了，别忘了一点，catch里面放的是AggregateException，不是Exception，
            // 不然捕捉不到别怪我


            TaskFactory taskFactory = new TaskFactory();
            // 通知线程是否取消
            CancellationTokenSource cts = new CancellationTokenSource();
            List<Task> taskList = new List<Task>();
            // 想要主线程抓捕到子线程异常，必须使用Task.WaitAll，这种方法不推荐
            // 想要捕获子线程的异常，最好在子线程本身抓捕
            // 完全搞不懂啊，看懵逼了都
            try
            {
                for (int i = 0; i < 40; i++)
                {
                    int name = i;
                    // 在子线程中抓捕异常
                    Action<object> a = t =>
                    {
                        try
                        {
                            Thread.Sleep(2000);
                            Console.WriteLine(cts.IsCancellationRequested);
                            if (cts.IsCancellationRequested)
                            {
                                Console.WriteLine($"放弃执行{name}");
                            }
                            else
                            {
                                if (name == 1)
                                {
                                    throw new Exception($"取消了线程 name:{name},t:{t}");
                                }
                                if (name == 5)
                                {
                                    throw new Exception($"取消了线程 name:{name},t:{t}");
                                }
                                Console.WriteLine($"执行成功:{name}");
                            }
                        }
                        catch (Exception ex)
                        {
                            // 通知线程取消，后面的都不执行
                            cts.Cancel();
                            Console.WriteLine("子线程内部捕获：" + ex.Message);
                        }
                    };
                    taskList.Add(taskFactory.StartNew(a, name, cts.Token));
                }
                Task.WaitAll(taskList.ToArray());
            }
            catch (AggregateException ex)
            {
                foreach (var item in ex.InnerExceptions)
                {
                    Console.WriteLine("TaskWaitAll 捕获：" + item.Message);
                }
            }
        }

        #region ThreadLock
        //先准备三个公共变量
        private static int iIndex;
        private static object objLock = new object();
        private static List<int> indexList = new List<int>();

        static MultipleVerAsync()
        {
        }

        /// <summary>
        /// 没有加锁的
        /// </summary>
        public static void ThreadNoLock()
        {
            // 这个运行的结果，每次总是会少于 10000，这是因为有同时操作的情况，导致结果没有达到预期


            List<Task> taskList = new List<Task>();
            // 开启 10000个线程
            for (int i = 0; i < 10000; i++)
            {
                int newI = i;
                Task task = Task.Factory.StartNew(() =>
                {
                    iIndex += 1;
                    indexList.Add(newI);
                });
                taskList.Add(task);
            }
            // 等待所有线程完成
            Task.WhenAll(taskList.ToArray());
            // 打印结果
            Console.WriteLine(iIndex);
            Console.WriteLine(indexList.Count);
        }

        /// <summary>
        /// 带锁的多线程
        /// </summary>
        public static void ThreadLock()
        {
            List<Task> taskList = new List<Task>();

            // 创建10000个Task
            for (int i = 0; i < 10000; i++)
            {
                int NewI = i;
                Task task = Task.Factory.StartNew(() =>
                {
                    lock (objLock)
                    {
                        iIndex += 1;
                        indexList.Add(NewI);
                    }
                });
                taskList.Add(task);
            }
            Task.WaitAll(taskList.ToArray());
            Console.WriteLine(iIndex);
            Console.WriteLine(indexList.Count);
        }

        #endregion
    }
}
