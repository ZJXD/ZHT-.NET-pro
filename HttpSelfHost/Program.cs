using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace HttpSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServer();
            //string inputStr = null;
            //while ((inputStr = Console.ReadLine()).ToLower() != "exit")
            //{
            //}
        }

        static public void StartServer()
        {
            WebAPI.WebServer.Start();
        }
    }
}
