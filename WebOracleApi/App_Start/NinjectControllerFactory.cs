using InterfaceOracle;
using Ninject;
using ServiceOracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebOracleApi.App_Start
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        /// <summary>
        /// 依赖注入核心
        /// </summary>
        private readonly IKernel _kernel;

        /// <summary>
        /// 构造依赖注入控制器工厂
        /// </summary>
        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(_kernel);
        }

        /// <summary>
        /// 产生控制器
        /// </summary>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }
        /// <summary>
        /// 添加绑定
        /// </summary>
        private void AddBindings()
        {
            //绑定接口和实现类
            _kernel.Bind<IMyTest>().To<SMyTest>();
            //InRequestScope 表示生命周期为：请求。  请求结束时，对象被释放
        }
    }
}