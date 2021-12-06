using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using PlannerTasks.DAL.Entities;

namespace PlannerTasks.DAL.Interfaces
{
    /// <summary>
    /// Interface IUnitOfWork
    /// Presents IUnitOfWork for DAL level
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// Repository of Employee
        /// </summary>
        IRepository<Employee> Employees { get; }

        /// <summary>
        /// Repository of Task
        /// </summary>
        IRepository<Task> Tasks { get; }

        /// <summary>
        /// Repository of StatusHistory
        /// </summary>
        IRepository<StatusHistory> StatusHistories { get; }
        
        /// <summary>
        /// Method save changes of DB context
        /// </summary>
        void Save();
    }
}
