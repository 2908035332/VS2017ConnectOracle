using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebOracleApi.App_Start;

namespace WebOracleApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //注册Ninject容器
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
