using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    /// <summary>
    /// 工厂方法模式（工厂模式）
    /// 工厂模式存在类与switch语句的高耦合，增加新的类 需要去增加case分支，违背了开放-封闭原则
    /// 工厂方法模式可以解决这个问题。
    /// 工厂方法是针对每一种产品提供一个工厂类。通过不同的工厂实例来创建不同的产品实例。
    /// 工厂方法模式是一种极端情况的抽象工厂模式（即只生产一种产品的抽象工厂模式），而抽象工厂模式可以看成是工厂方法模式的一种推广。
    /// </summary>
    class FactoryMethod
    {
        public void Main()
        {
            SubFactory sf = new SubFactory();
            Operator op = sf.CreateOperator();
            op.NumberA = 10;
            op.NumberB = 5;
            op.GetResult();
        }
    }

    public abstract class Operator
    {
        public double NumberA;
        public double NumberB;
        public virtual double GetResult()
        {
            return 0;
        }
    }

    public class Add1 : Operator
    {
        public override double GetResult()
        {
            return NumberA + NumberB;
        }
    }

    public class Sub1 : Operator
    {
        public override double GetResult()
        {
            return NumberA - NumberB;
        }
    }

    abstract class IFactory
    {
        public abstract Operator CreateOperator();
    }

    class AddFactory : IFactory
    {
        public override Operator CreateOperator()
        {
            return new Add1();
        }
    }

    class SubFactory : IFactory
    {
        public override Operator CreateOperator()
        {
            return new Sub1();
        }
    }
}
