using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerTasks.DAL.Interfaces;
using PlannerTasks.DAL.Entities;
using PlannerTasks.DAL.EF;

namespace PlannerTasks.DAL.Repositories
{
    public class EFUnitOfWork: IUnitOfWork
    {
        private PlannerContext db;
        private EmployeeRepository employeeRepository;
        private TaskRepository taskRepository;
        private StatusHistoryRepository statusHistoryRepository;

        public EFUnitOfWork()
        {
            db = new PlannerContext();
        }

        public EFUnitOfWork(string connectionString)
        {
            db = new PlannerContext(connectionString);
        }
        public IRepository<Employee> Employees
        {
            get
            {
                if(employeeRepository == null)
                    employeeRepository = new EmployeeRepository(db);
                return employeeRepository;
            }
        }

        public IRepository<Task> Tasks
        {
            get
            {
                if(taskRepository == null)
                    taskRepository = new TaskRepository(db);
                return taskRepository;
            }
        }

        public IRepository<StatusHistory> StatusRepositories
        {
            get
            {
                if (statusHistoryRepository == null)
                    statusHistoryRepository = new StatusHistoryRepository(db);
                return statusHistoryRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
