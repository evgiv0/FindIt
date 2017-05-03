using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using FindIt.Concrete;
using System.Web.Mvc;
using FindIt.Abstract;

namespace FindIt.Infrastracture
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }
        
        private void AddBindings()
        {
            ninjectKernel.Bind<INoticeRepository>().To<EFNoticeRepository>();
        }

    }
}