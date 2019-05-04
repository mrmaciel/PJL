//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Http.Dependencies;
//using Ninject;

//namespace Projeto_Livraria.Ninject
//{
//    public class LocalNinjectDependencyResolver : IDependencyResolver
//    {
//        private readonly IKernel _kernel;

//        public LocalNinjectDependencyResolver(IKernel kernel)
//        {
//            _kernel = kernel;
//        }
                
                
//        public object GetService(Type serviceType)
//        {
//            return _kernel.TryGet(serviceType);
//        }

//        public IEnumerable<object> GetServices(Type serviceType)
//        {
//            try
//            {
//                return _kernel.GetAll(serviceType);
//            }
//            catch (Exception)
//            {
//                return new List<object>();
//            }
//        }

//        public IDependencyScope BeginScope()
//        {
//            return this;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            _kernel.Dispose();
//        }
//    }
//}