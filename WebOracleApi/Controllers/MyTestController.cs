using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebOracleApi.Controllers
{
    using InterfaceOracle;
    using Ninject;
    using ModelOracle;

    public class MyTestController : ApiController
    {
        [Inject]
        public IMyTest GetIMyTest { get; set; }


        [HttpGet]
        public List<MyTest> GetTests()
        {
            return GetIMyTest.SelectMyTest();
        }

        [HttpGet]
        public List<MyTest> GetTests(string name, DateTime? StartTime, DateTime? EndTime)
        {
            return GetIMyTest.SelectWhereMyTest(name, StartTime, EndTime);
        }

        [HttpGet]
        public MyTest GetTests(int id)
        {
            return GetIMyTest.SelectByID(id);
        }

        [HttpPut]
        public bool Put(MyTest myTest) {
            return GetIMyTest.Updata(myTest);
        }

        [HttpPost]
        public bool Post(MyTest myTest)
        {
            return GetIMyTest.Insert(myTest);
        }

        [HttpDelete]
        public bool Delete(int id) {
            return GetIMyTest.Delete(id);
        }


        /// <summary>
        /// 预请求
        /// </summary>
        [HttpOptions]
        public void Options() {

        }
    }
}
