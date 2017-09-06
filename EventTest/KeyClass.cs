using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTest
{
    internal class KeyEventArgs : EventArgs
    {
        private char keyChar;
        public KeyEventArgs(char keyChar) : base()
        {
            this.keyChar = keyChar;
        }

        public char KeyChar
        {
            get
            {
                return keyChar;
            }
        }
    }

    internal class KeyInputMonitor
    {
        // 创建一个委托，返回类型为void，两个参数
        public delegate void KeyDownHandler(object sender, KeyEventArgs e);
        // 将创建的委托和特定事件关联,在这里特定的事件为KeyDown
        public event KeyDownHandler KeyDown;

        public void OnKeyDown(KeyEventArgs keyEventArgs)
        {
            KeyDown?.Invoke(this, keyEventArgs);
        }

        public void Run()
        {
            bool finished = false;
            do
            {
                Console.WriteLine("Input a char");
                string respone = Console.ReadLine();

                char responChar = respone == "" ? ' ' : char.ToUpper(respone[0]);
                switch (responChar)
                {
                    case 'X':
                        finished = true;
                        break;
                    default:
                        // 得到按键信息的参数
                        KeyEventArgs keyEventArgs = new KeyEventArgs(responChar);
                        // 触发事件
                        OnKeyDown(keyEventArgs);
                        break;
                }
            } while (!finished);
        }
    }

    internal class EventReceiver
    {
        public EventReceiver(KeyInputMonitor monitor)
        {
            // 产生一个委托实例并添加到KeyInputMonitor产生的事件列表中
            monitor.KeyDown += new KeyInputMonitor.KeyDownHandler(OnKeyDown);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // 真正的事件处理函数
            Console.WriteLine($"Capture key:{e.KeyChar}");
        }
    }
}
