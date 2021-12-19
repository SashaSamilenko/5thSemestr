using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlannerTasks.BLL.Interfaces;
using PlannerTasks.BLL.Services;
using Ninject;
using System.Web.Mvc;

namespace PlannerTasks.WEB_MVC.Util
{
    public class NinjectDependencyResolverWEB: IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolverWEB(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IEmployeeService>().To<EmployeeService>();
            kernel.Bind<ITaskService>().To<TaskService>();
            kernel.Bind<IStatusHistoryService>().To<StatusHistoryService>();
        }
    }
}