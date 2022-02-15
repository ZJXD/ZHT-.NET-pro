using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateNewQueue("MsgQueue");              //创建一个消息队列  
            sendSimpleMsg();                         //每一个队列最好只发送和接收同一种格式的信息，不然不好转换格式。  
            receiveSimpleMsg();
            receiveSimpleMsg();
            sendComplexMsg();
            receiveComplexMsg();
            //MsgModel m = receiveComplexMsg<MsgModel>();
            //Console.WriteLine(m.ToString());
            Console.ReadKey();
        }

        private static void sendSimpleMsg()
        {
            //实例化MessageQueue,并指向现有的一个名称为VideoQueue队列  
            MessageQueue MQ = new MessageQueue(@".\private$\MsgQueue");
            //MQ.Send("消息测试", "测试消息");  
            Message message = new Message();
            message.Label = "消息lable";
            message.Body = "消息body";
            MQ.Send(message);

            Console.WriteLine("成功发送消息，" + DateTime.Now);
        }
        private static void receiveSimpleMsg()
        {
            MessageQueue MQ = new MessageQueue(@".\private$\MsgQueue");
            //调用MessageQueue的Receive方法接收消息  
            if (MQ.GetAllMessages().Length > 0)
            {
                Message message = MQ.Receive(TimeSpan.FromSeconds(5));
                if (message != null)
                {
                    //message.Formatter = new System.Messaging.XmlMessageFormatter(new string[] { "Message.Bussiness.VideoPath,Message" });//消息类型转换  
                    message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                    Console.WriteLine(string.Format("接收消息成功,lable:{0},body:{1},{2}", message.Label, message.Body.ToString(), DateTime.Now));
                }
            }
            else
            {
                Console.WriteLine("没有消息了！");
            }
        }
        private static void sendComplexMsg()
        {
            //实例化MessageQueue,并指向现有的一个名称为VideoQueue队列  
            MessageQueue MQ = new MessageQueue(@".\private$\MsgQueue");
            //MQ.Send("消息测试", "测试消息");  
            Message message = new Message();
            message.Label = "复杂消息lable";
            message.Body = new MsgModel("1", "消息1");
            MQ.Send(message);

            Console.WriteLine("成功发送消息，" + DateTime.Now );
        }
        private static void receiveComplexMsg()
        {
            MessageQueue MQ = new MessageQueue(@".\private$\MsgQueue");
            //调用MessageQueue的Receive方法接收消息  
            if (MQ.GetAllMessages().Length > 0)
            {
                Message message = MQ.Receive(TimeSpan.FromSeconds(5));
                if (message != null)
                {
                    message.Formatter = new XmlMessageFormatter(new Type[] { typeof(MsgModel) });//消息类型转换  
                    MsgModel msg = (MsgModel)message.Body;
                    Console.WriteLine(string.Format("接收消息成功,lable:{0},body:{1},{2}", message.Label, msg, DateTime.Now));
                }
            }
            else
            {
                Console.WriteLine("没有消息了！");
            }
        }
        private T receiveComplexMsg<T>()
        {
            MessageQueue MQ = new MessageQueue(@".\private$\MsgQueue");
            //调用MessageQueue的Receive方法接收消息  
            if (MQ.GetAllMessages().Length > 0)
            {
                Message message = MQ.Receive(TimeSpan.FromSeconds(5));
                if (message != null)
                {
                    message.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });//消息类型转换  
                    T msg = (T)message.Body;
                    return msg;
                }
            }

            return default(T);
        }

        /// <summary>  
        /// 创建消息队列  
        /// </summary>  
        /// <param name="name">消息队列名称</param>  
        /// <returns></returns>  
        public static void CreateNewQueue(string name)
        {
            if (!MessageQueue.Exists(".\\private$\\" + name))//检查是否已经存在同名的消息队列  
            {

                MessageQueue mq = MessageQueue.Create(".\\private$\\" + name);
                mq.Label = "private$\\" + name;
                Console.WriteLine("创建成功！");
            }
            else
            {
                //System.Messaging.MessageQueue.Delete(".\\private$\\" + name);//删除一个消息队列  
                Console.WriteLine("已经存在");
            }
        }

    }
    [Serializable]
    public class MsgModel
    {
        public string id { get; set; }
        public string Name { get; set; }
        public MsgModel() { }
        public MsgModel(string _id, string _Name)
        {
            id = _id;
            Name = _Name;
        }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(Name)) return "";
            return string.Format("id--{0},Name--{1}", id, Name);
        }
    }
}
