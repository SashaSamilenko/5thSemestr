using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerTasks.DAL.Interfaces;
using PlannerTasks.DAL.Entities;
using PlannerTasks.DAL.EF;

namespace PlannerTasks.DAL.Repositories
{
    /// <summary>
    /// Class EFUnitOfWork
    /// Implements interface IUnitOfWork
    /// </summary>
    public class EFUnitOfWork: IUnitOfWork
    {
        /// <summary>
        /// Context DB
        /// </summary>
        private PlannerContext db;

        /// <summary>
        /// Employee repository
        /// </summary>
        private EFRepository<Employee> employeeRepository;
        /*private EmployeeRepository employeeRepository;*/

        /// <summary>
        /// Task repository
        /// </summary>
        private EFRepository<Task> taskRepository;
        /*private TaskRepository taskRepository;*/

        /// <summary>
        /// StatusHistory repository
        /// </summary>
        private EFRepository<StatusHistory> statusHistoryRepository;
        /*private StatusHistoryRepository statusHistoryRepository;*/

        /// <summary>
        /// Constructor without parametrs
        /// </summary>
        public EFUnitOfWork()
        {
            db = new PlannerContext();
        }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="connectionString">Is a connection string to DB</param>
        public EFUnitOfWork(string connectionString)
        {
            db = new PlannerContext(connectionString);
        }

        /// <summary>
        /// Implementation of repository interface
        /// for employees
        /// </summary>
        public IRepository<Employee> Employees
        {
            get
            {
                if(employeeRepository == null)
                    employeeRepository = new EFRepository<Employee>(db);
                return employeeRepository;
            }
        }

        /// <summary>
        /// Implementation of repository interface
        /// for tasks
        /// </summary>
        public IRepository<Task> Tasks
        {
            get
            {
                if(taskRepository == null)
                    taskRepository = new EFRepository<Task>(db);
                return taskRepository;
            }
        }

        /// <summary>
        /// Implementation of repository interface
        /// for statusHistories
        /// </summary>
        public IRepository<StatusHistory> StatusHistories
        {
            get
            {
                if (statusHistoryRepository == null)
                    statusHistoryRepository = new EFRepository<StatusHistory>(db);
                return statusHistoryRepository;
            }
        }

        /// <summary>
        /// Implementation method Save.
        /// Call method SaveChanges in DB context to  save changes of context.
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }

        /// <summary>
        /// Bool value about disposing DBContext
        /// and connection with DB, if that exist
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Method for disposing DB context
        /// and connection with DB, if that exist
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        /// <summary>
        /// Method for call disposing context
        /// and connection with DB.
        /// And also deleting objects from finalize list.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
