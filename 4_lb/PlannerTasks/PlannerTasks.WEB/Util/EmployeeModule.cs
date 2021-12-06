using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using PlannerTasks.BLL.Services;
using PlannerTasks.BLL.Interfaces;

namespace PlannerTasks.WEB.Util
{
    public class EmployeeModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IEmployeeService>().To<EmployeeService>();
        }
    }
}
