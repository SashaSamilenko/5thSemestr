using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerTasks.DAL.Entities;
using PlannerTasks.DAL.Interfaces;
using PlannerTasks.DAL.EF;
using System.Data.Entity;

namespace PlannerTasks.DAL.Repositories
{
    public class TaskRepository: IRepository<Task>
    {
        private MobileContext db;

        public TaskRepository(MobileContext context)
        {
            this.db = context;
        }

        public IEnumerable<Task> GetAll()
        {
            return db.Tasks.Include(t => t.Employee);
        }

        public Task Get(int id)
        {
            return db.Tasks.Find(id);
        }

        public void Create(Task task)
        {
            db.Tasks.Add(task);
        }

        public void Update(Task task)
        {
            db.Entry(task).State = EntityState.Modified;
        }
        public IEnumerable<Task> Find(Func<Task, Boolean> predicate)
        {
            return db.Tasks.Include(t => t.Employee).Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task != null)
                db.Tasks.Remove(task);
        }
    }
}
