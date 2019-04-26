using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    /// <summary>
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 模板模式测试
            //TemplatePattern templatePattern = new TemplatePattern();
            //templatePattern.Main();

            // 简单的深拷贝和浅拷贝
            //Student student1 = new Student { Id = 10, Name = "aedjoi", Addres = "adjgj", Number = 24 };
            //Student student2 = student1;
            //Student student3 = new Student { Id = student1.Id, Name = student1.Name, Addres = student1.Addres, Number = student1.Number };
            //student1.Number = 56;
            //student1.Name = "安东尼";
            //Console.WriteLine($"student1.Number：{student1.Number},Name：{student1.Name}");
            //Console.WriteLine($"student2.Number：{student2.Number},Name：{student2.Name}");
            //Console.WriteLine($"student3.Number：{student3.Number},Name：{student3.Name}");

            // 迪米特法则
            //DemeterRule demeterRule = new DemeterRule();
            //demeterRule.ClickButtonClose();

            // 建造者模式
            //BuilderPattern builderPattern = new BuilderPattern();

            // 观察者模式
            ObserverPattern observer = new ObserverPattern();
            observer.StartObserver();

            Console.ReadLine();
        }
    }


    class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Addres { get; set; }

        public int Number { get; set; }
    }

    class A
    {
        public virtual void Amethod()
        {
            Console.Write("A");
        }
    }

    class B : A
    {
        public override void Amethod()
        {
            Console.Write("A->B");
        }
        public void Bmethod()
        {
            Console.Write("B");
        }
    }

}
