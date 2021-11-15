using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using PlannerTasks.BLL.Services;
using PlannerTasks.BLL.Interfaces;

namespace PlannerTasks.Console.Util
{
    public class TaskModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITaskService>().To<TaskService>();
        }
    }
}
