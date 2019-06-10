using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTest
{
    /// <summary>
    /// 事件调用的一个示例
    /// </summary>
    public class HostAndWife
    {
        public void Start()
        {
            // 6、添加注册
            Host.Registe();
            HostWife.Registe();

            // 当前时间，从2018年12月31日23:59:50开始计时
            DateTime now = new DateTime(2018, 12, 31, 23, 59, 50);
            DateTime midnight = new DateTime(2019, 1, 1, 0, 0, 0);

            // 等待午夜的到来
            Console.WriteLine("时间一秒一秒地流逝... ");
            while (now < midnight)
            {
                Console.WriteLine("当前时间: " + now);
                System.Threading.Thread.Sleep(1000);    //程序暂停一秒
                now = now.AddSeconds(1);                //时间增加一秒
            }

            // 午夜零点新年到,看门狗引发Alarm事件
            Console.WriteLine("\n2018最后一天的午夜: " + now);
            Console.WriteLine("9102即将到来... ");
            Dog.OnAlarm();
            Console.ReadLine();
        }
    }

    // 事件发送者
    public class Dog
    {
        // 1、声明关于事件的委托
        public delegate void AlarmEventHandler(object sender, EventArgs e);

        // 2、声明事件
        public static event AlarmEventHandler Alarm;

        // 3、触发事件的函数
        public static void OnAlarm()
        {
            if (Alarm != null)
            {
                Console.WriteLine("\n狗：新年到啦！ 汪汪汪~~~~");
                Alarm(null, new EventArgs());      // 发出警报
            }
        }
    }

    // 事件订阅者
    public class Host
    {
        // 4、事件处理程序
        private static void HostHandle(object sender, EventArgs e)
        {
            Console.WriteLine("\n主人：跨年了，新年好！");
        }

        // 5、注册事件处理程序
        public static void Registe()
        {
            Dog.Alarm += HostHandle;
        }
    }

    // 事件订阅者
    public class HostWife
    {
        public static void Registe()
        {
            Dog.Alarm += HostWifeHandle;
        }

        private static void HostWifeHandle(object sender, EventArgs e)
        {
            Console.WriteLine("\n主人妻子：你也是，新年好！");
        }
    }
}
