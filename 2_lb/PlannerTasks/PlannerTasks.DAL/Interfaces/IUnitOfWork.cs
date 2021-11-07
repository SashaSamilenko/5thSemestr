using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using PlannerTasks.DAL.Entities;

namespace PlannerTasks.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Employee> Employees { get; }
        IRepository<Task> Tasks { get; }
        void Save();
    }
}
