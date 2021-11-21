using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerTasks.DAL.Interfaces;
using PlannerTasks.DAL.EF;
using PlannerTasks.DAL.Entities;
using System.Data.Entity;

namespace PlannerTasks.DAL.Repositories
{
    /// <summary>
    /// Class StatusHistoryRepository.
    /// Implements interface IRepository with type value of StatusHistory.
    /// Provides method to interact with DB context
    /// </summary>
    public class StatusHistoryRepository: IRepository<StatusHistory>
    {
        /// <summary>
        /// DB context.
        /// </summary>
        private PlannerContext db;

        /// <summary>
        /// Constructor with one parameters.
        /// </summary>
        /// <param name="context">Planner context of DB</param>
        public StatusHistoryRepository(PlannerContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Method return IEnumerable of StatusHistories entities
        /// </summary>
        /// <returns>IEnumerable of StatusHistory entities</returns>
        public IEnumerable<StatusHistory> GetAll()
        {
            return db.StatusHistories;
        }

        /// <summary>
        /// Get StatusHistories with given Id
        /// </summary>
        /// <param name="id">Identifier of statusHistory</param>
        /// <returns>StatusHistory entity with given Id</returns>
        public StatusHistory Get(int id)
        {
            return db.StatusHistories.Find(id);
        }

        /// <summary>
        /// Adds statusHistory to DB context.
        /// Changes EntityState from Detached=1 to Added=4 for StatusHistory entity
        /// </summary>
        /// <param name="statusHistory">StatusHistory for adding</param>
        public void Create(StatusHistory statusHistory)
        {
            db.StatusHistories.Add(statusHistory);
        }

        /// <summary>
        /// Changes EntityState to Modified=16 for StatusHistory entity
        /// </summary>
        /// <param name="statusHistory">Modified StatusHistory entity</param>
        public void Update(StatusHistory statusHistory)
        {
            db.Entry(statusHistory).State = EntityState.Modified;
        }

        /// <summary>
        /// Finds StatusHistory entities that satisfy the condition
        /// </summary>
        /// <param name="predicate">Enumerable of StatusHistory entities</param>
        /// <returns>IEnumerable of StatusHistory
        /// entities which obey condition(predicate)</returns>
        public IEnumerable<StatusHistory> Find(Func<StatusHistory, Boolean> predicate)
        {
            return db.StatusHistories.Include(sh => sh.Task).Where(predicate).ToList();
        }

        /// <summary>
        /// Remove StatusHistory entity from DBContext and DB with given Id if that entity exists
        /// </summary>
        /// <param name="id">Identifier of StatusHistory entity</param>
        public void Delete(int id)
        {
            StatusHistory statusHistory = db.StatusHistories.Find(id);
            if (statusHistory != null)
                db.StatusHistories.Remove(statusHistory);
        }
    }
}
