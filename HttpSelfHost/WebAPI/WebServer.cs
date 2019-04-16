using System;
using System.Web.Http.SelfHost;

namespace HttpSelfHost.WebAPI
{
    public class WebServer
    {
        static HttpSelfHostServer server = null;

        public static void Start()
        {
            string baseAddress = string.Format("http://{0}:{1}/",
                   System.Configuration.ConfigurationManager.AppSettings.Get("Domain"),
                   System.Configuration.ConfigurationManager.AppSettings.Get("Port"));
            using (var server = new HttpSelfHostServer(HostStartup.InitWebApiConfig(baseAddress)))
            {
                server.OpenAsync().Wait();

                Console.WriteLine(string.Format("host 已启动：{0}", baseAddress));

                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
