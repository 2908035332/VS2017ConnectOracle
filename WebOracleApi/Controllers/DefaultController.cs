using System;
using System.Web.Http;

namespace WebOracleApi.Controllers
{
    public class DefaultController : ApiController
    {

        [HttpGet]
        public bool Get(string id)
        {
            DateTime time = DateTime.Parse(id);
            TimeSpan span = DateTime.Now.Date - time;
            //var a = Math.Abs(span.TotalMinutes);
            return Math.Abs(span.TotalMinutes) <= 5.0;
        }
        [HttpGet]
        [Route("api/{contrller}/DateNow")]
        public string Get()
        {
            return DateTime.Now.ToString();
        }

        /// <summary>
        /// 预请求
        /// </summary>
        [HttpOptions]
        public void Options()
        {

        }
    }
}
