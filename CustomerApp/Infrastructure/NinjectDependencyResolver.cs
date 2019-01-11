using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace CustomerApp.Infrastructure
{
    public class NinjectDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;
        private List<Products> productsList;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }

        private void AddBindings()
        {
            kernel.Bind<Model.Abstract.IProductRepository>().To<Model.Concrete.EFProductRepository>();
        }
    }
}