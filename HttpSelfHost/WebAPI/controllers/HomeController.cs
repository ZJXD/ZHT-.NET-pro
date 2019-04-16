using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HttpSelfHost.WebAPI.controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public object Get()
        {
            return new { code = 1, msg = "OK HomeController" };
        }
    }

    public class VideoController : ApiController
    {
        [HttpGet]
        public object Get()
        {
            return new { code = 2, msg = "OK VideoController" };
        }
    }

    public class ValuesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "111", "222" };
        }
    }
}
