using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    /// <summary>
    /// 迪米特法则：强调类间的低耦合
    /// 类内信息的隐藏，可以提高类的可复用程度
    /// 一个对象应该对其他对象保持最少了解
    /// </summary>
    class DemeterRule
    {
        private Container container = new Container();

        public void ClickButtonClose()
        {
            container.SendCloseCommand();
        }
    }

    // 第一要义:从被依赖者的角度来说：只暴露应该暴露的方法或者属性，即在编写相关的类的时候确定方法/属性的权限
    // 第二要义:从依赖者的角度来说，只依赖应该依赖的对象
    // 下面用一个关闭计算机的例子来说明
    // 计算机分为软件系统(System)和硬件(Container)

    /// <summary>
    /// 第一要义
    /// 这里因为关闭是一系列的操作，是按照顺序的并且必须这样
    /// 所以具体的一些操作都是私有的，不对外暴露
    /// </summary>
    public class System
    {
        private void SaveCurrentTask()
        {
            Console.WriteLine("保存当前任务！");
        }

        private void CloseService()
        {
            Console.WriteLine("关闭服务！");
        }

        private void CloseScreen()
        {
            Console.WriteLine("关闭屏幕！");
        }

        private void ClosePower()
        {
            Console.WriteLine("关闭电源！");
        }

        public void Close()
        {
            SaveCurrentTask();
            CloseService();
            CloseScreen();
            ClosePower();
        }
    }

    /// <summary>
    /// 第二要义
    /// 这个是硬件（这里是拿手动关闭电源关机），按下关闭电源，接下来的具体操作，
    /// 和实际的操作人就没有关系了
    /// </summary>
    public class Container
    {
        private System system = new System();

        public void SendCloseCommand()
        {
            system.Close();
        }
    }
}
