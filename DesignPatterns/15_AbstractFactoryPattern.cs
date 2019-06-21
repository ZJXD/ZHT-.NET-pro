using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    /// <summary>
    /// 抽象工厂模式
    /// 例如，汽车可以分为轿车、SUV、MPV等，也分为奔驰、宝马等。我们可以将奔驰的所有车看作是一个产品族，而将宝马的所有车看作是另一个产品族。
    /// 分别对应两个工厂，一个是奔驰的工厂，另一个是宝马的工厂。与工厂方法不同，奔驰的工厂不只是生产具体的某一个产品，
    /// 而是一族产品（奔驰轿车、奔驰SUV、奔驰MPV）。“抽象工厂”的“抽象”指的是就是这个意思。
    /// </summary>
    class AbstractFactoryPattern
    {
        public void Main()
        {
            IFactoryDatabase factory = new SqlServerFac();
            IDepartMent sqlServer = factory.GetInstance();
            sqlServer.Insert("helloworld!-insert");
            sqlServer.Delete("helloworld!-delete");
        }
    }
    interface IDepartMent
    {
        void Insert(string sql); //在数据库中插入一条记录

        void Delete(string sql); // 删除一条数据
    }
    class SqlServer : IDepartMent
    {
        public void Insert(string sql)
        {
            Console.WriteLine("sqlServer执行sql插入语句！");
        }

        public void Delete(string sql)
        {
            Console.WriteLine("sqlServer执行sql删除语句！");
        }
    }

    class Access : IDepartMent
    {
        public void Insert(string sql)
        {
            Console.WriteLine("Access执行sql插入语句！");
        }

        public void Delete(string sql)
        {
            Console.WriteLine("Access执行sql删除语句！");
        }
    }

    interface IFactoryDatabase
    {
        IDepartMent GetInstance();
    }

    class SqlServerFac : IFactoryDatabase
    {
        public IDepartMent GetInstance()
        {
            return new SqlServer();
        }
    }

    class AccessFac : IFactoryDatabase
    {
        public IDepartMent GetInstance()
        {
            return new Access();
        }
    }
}
