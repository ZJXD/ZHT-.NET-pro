using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTest
{
    /// <summary> 
    /// 类EatEventArgs 必须继承自类EventArgs，用来引发事件时封装数据 
    /// </summary> 
    public class EatEventArgs : EventArgs
    {
        public String restrauntName; //饭店名称 
        public decimal moneyOut; //准备消费金额 
    }

    /// <summary> 
    /// 这个委托用来说明处理吃饭事件的方法的方法头(模式) 
    /// </summary> 
    public delegate void EatEventHandler(object sender, EatEventArgs e);

    /// <summary> 
    /// 引发吃饭事件(EateEvent)的类Master(主人)，这个类必须 
    /// 1.声明一个名为EatEvent的事件: public event EatEventHandler EatEvent; 
    /// 2.通过一个名为OnEatEvent的方法来引发吃饭事件，给那些处理此事件的方法传数据； 
    /// 3.说明在某种情形下引发事件呢？在饿的时候。用方法Hungrg来模拟。 
    /// </summary> 
    public class Master
    {
        //声明事件 
        public event EatEventHandler EatEvent;

        //引发事件的方法 
        public void OnEatEvent(EatEventArgs e)
        {
            EatEvent?.Invoke(this, e);
        }

        //当主人饿的时候，他会指定吃饭地点和消费金额。 
        public void Hungry(String restrauntName, decimal moneyOut)
        {
            EatEventArgs e = new EatEventArgs();
            e.restrauntName = restrauntName;
            e.moneyOut = moneyOut;

            Console.WriteLine("主人说：");
            Console.WriteLine("我饿了，要去{0}吃饭，消费{1}元", e.restrauntName, e.moneyOut);

            //引发事件 
            OnEatEvent(e);
        }
    }

    /// <summary> 
    /// 类Servant(仆人)有一个方法ArrangeFood(安排食物)来处理主人的吃饭事件
    /// </summary> 
    public class Servant
    {
        public void ArrangeFood(object sender, EatEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("仆人说:");
            Console.WriteLine("我的主人, 您的命令是 : ");
            Console.WriteLine("吃饭地点 -- {0}", e.restrauntName);
            Console.WriteLine("准备消费 -- {0}元 ", e.moneyOut);
            Console.WriteLine("好的，正给您安排。。。。。。。。\n");
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("主人，您的食物在这儿，请慢用");
            Console.Read();
        }
    }

    /// <summary> 
    /// 类God安排qinshihuang（秦始皇）的仆人是lisi（李斯），并让李斯的ArrangeFood 
    /// 方法来处理qinshihuang的吃饭事件：qinshihuang.EatEvent += new EatEventHandler(lishi.ArrangeFood); 
    /// </summary> 
    public class God
    {
        public static void Start()
        {
            Master qinshihuang = new Master();
            Servant lishi = new Servant();

            qinshihuang.EatEvent += new EatEventHandler(lishi.ArrangeFood);

            //秦始皇饿了，想去希尔顿大酒店，消费5000元 
            qinshihuang.Hungry("希尔顿大酒店", 5000.0m);
        }
    }
}
