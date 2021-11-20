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
    /// <summary>
    /// Class TaskRepository.
    /// Implements interface IRepository with type value of Task.
    /// Provides method to interact with DB context
    /// </summary>
    public class TaskRepository: IRepository<Task>
    {
        /// <summary>
        /// DB context.
        /// </summary>
        private PlannerContext db;

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="context">Context of DB</param>
        public TaskRepository(PlannerContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Method return IEnumerable of Tasks entities
        /// </summary>
        /// <returns>IEnumerable of Task entities</returns>
        public IEnumerable<Task> GetAll()
        {
            return db.Tasks.Include(t => t.Employee);
        }

        /// <summary>
        /// Get tasks with given Id
        /// </summary>
        /// <param name="id">Identifier of task</param>
        /// <returns>Tasks with given Id</returns>
        public Task Get(int id)
        {
            return db.Tasks.Find(id);
        }

        /// <summary>
        /// Adds task to DB context.
        /// Changes EntityState from Detached=1 to Added=4 for Task entity
        /// </summary>
        /// <param name="task">Task entity for adding</param>
        public void Create(Task task)
        {
            db.Tasks.Add(task);
        }

        /// <summary>
        /// Changes EntityState to Modified=16 for Task entity
        /// </summary>
        /// <param name="task">Modified Task entity</param>
        public void Update(Task task)
        {
            db.Entry(task).State = EntityState.Modified;
        }

        /// <summary>
        /// Finds Task entities that satisfy the condition
        /// </summary>
        /// <param name="predicate">Condition of finding</param>
        /// <returns>Enumerable of Task entities
        /// which obey condition(predicate)</returns>
        public IEnumerable<Task> Find(Func<Task, Boolean> predicate)
        {
            return db.Tasks.Include(t => t.Employee).Where(predicate).ToList();
        }

        /// <summary>
        /// Remove Task entity from DBContext and DB with given Id if that entity exists
        /// </summary>
        /// <param name="id">Identifier of Task entity</param>
        public void Delete(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task != null)
                db.Tasks.Remove(task);
        }
    }
}
