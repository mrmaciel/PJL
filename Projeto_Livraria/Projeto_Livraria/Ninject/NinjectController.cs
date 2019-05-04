//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using ClassLibrary_Projeto_Livraria.Interface;
//using ClassLibrary_Projeto_Livraria.Repositorio;
//using Ninject;
//using Ninject.Web.WebApi;
//using Ninject.Web.Common;
//using System.Web.Http;

//namespace Projeto_Livraria.Ninject
//{
//    public class NinjectController: DefaultControllerFactory
//    {

//        private IKernel ninjectKernel;

//        public NinjectController()
//        {
//            ninjectKernel = new StandardKernel();
           
//            AddBinding();

//            GlobalConfiguration.Configuration.DependencyResolver = new LocalNinjectDependencyResolver(ninjectKernel);
//        }

//        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
//        {
//            IController controller = null;

//            if (controllerType != null)
//                controller = (IController)ninjectKernel.Get(controllerType);

//            return controller;

//            //return controllerType == null
//            //    ? null
//            //    : (IController)ninjectKernel.Get(controllerType);
//        }

//        private void AddBinding()
//        {
//            ninjectKernel.Bind<ILivro>().To<LivroRepositorio>();
//        }
//    }
//}