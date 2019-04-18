using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    /// <summary>
    /// 模板模式（Template Pattern）中，一个抽象类公开定义了执行它的方法的方式/模板。它的子类可以按需要重写方法实现，但调用将以抽象类中定义的方式进行。 
    /// 这种类型的设计模式属于行为型模式。定义一个操作中的算法的骨架，而将一些步骤延迟到子类中。
    /// 模板模式：通过定义一个抽象类，抽象类里的一个方法A内部调用了另一个虚方法B，就可以通过继承这个抽象类，然后重写这个虚方法，达到控制方法A的目的。最大程度上减少了类B的代码重复量
    /// 定义一个类的基本骨架，然后通过虚函数将部分差异性方法或者属性在子类中重新定义，使得子类可以不改变父类的基本骨架即可重新定义从父类继承过来的一些方法。
    /// 也就是说模板方法通过将不变的行为搬移到父类中，去除了子类中的重复代码，代码复用程度较高。
    /// </summary>
    class TemplatePattern
    {
        public void Main()
        {
            //var t = new TestPaper1();
            //t.QuestionOneAnswer("26");
            //t.QuestionOne();

            Game game = new ContraGame();
            game.Play();
            Console.WriteLine("-----------------------------------------------------");
            Game tGame = new TMNTGame();
            tGame.Play();
        }
    }
    abstract class TestPaper
    {
        protected string Age;
        public void QuestionOne()
        {
            Console.WriteLine("how old are you! " + QuestionOneAnswer(Age));
        }
        public virtual string QuestionOneAnswer(string age)
        {
            return "";
        }
    }

    class TestPaper1 : TestPaper
    {
        public override string QuestionOneAnswer(string age)
        {
            this.Age = age;
            return age;
        }
    }

    #region 玩游戏的例子
    // 这里为了方便理解，我们依旧使用一个简单的示例来加以说明。
    // 我们以前在玩魂斗罗、双截龙、热血物语、忍者神龟等等游戏的时候，都需要在小霸王游戏机上插卡，然后启动游戏才能玩，
    // 其中魂斗罗这种游戏，启动游戏之后就可以直接玩了，但是忍者神龟这种游戏则在启动游戏之后，需要选择其中一个角色才能开始玩。
    // 那么我们可以根据这个场景写出一个通用的模板，主要包含启动游戏，玩游戏，结束游戏这几个必须实现的方法，选择人物这个方法改成可选。

    abstract class Game
    {
        // 启动游戏
        protected abstract void RunGame();

        // 选择人物，可选（用虚方法，可以覆盖也可以不覆盖）
        public virtual void ChoosePerson()
        {
            //Console.WriteLine("这里是Game...");
        }

        // 开始游戏
        protected abstract void PlayGame();

        // 结束游戏
        protected abstract void EndGame();

        public void Play()
        {
            RunGame();
            ChoosePerson();
            PlayGame();
            EndGame();
        }
    }

    class ContraGame : Game
    {
        protected override void RunGame()
        {
            Console.WriteLine("开始魂斗罗...");
        }

        protected override void PlayGame()
        {
            Console.WriteLine("魂斗罗，发射子弹，打打怪兽...");
        }

        protected override void EndGame()
        {
            Console.WriteLine("啊哦，中弹了，游戏结束...");
        }
    }

    class TMNTGame : Game
    {
        protected override void RunGame()
        {
            Console.WriteLine("开始忍者神龟...");
        }

        public override void ChoosePerson()
        {
            Console.WriteLine("选择了Raph 这个忍者神龟...");
        }

        protected override void PlayGame()
        {
            Console.WriteLine("忍者神龟，来一组大招...");
        }

        protected override void EndGame()
        {
            Console.WriteLine("啊哦，被对方大招打趴下了，游戏结束...");
        }
    }
    #endregion

}
