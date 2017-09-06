using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //实例化一个事件发送器
            KeyInputMonitor keyInputMonitor = new KeyInputMonitor();
            //实例化一个事件接收器
            EventReceiver eventReceiver = new EventReceiver(keyInputMonitor);
            //运行
            keyInputMonitor.Run();
        }
    }
}
