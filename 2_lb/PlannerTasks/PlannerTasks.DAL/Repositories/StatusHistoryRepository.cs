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
    public class StatusHistoryRepository: IRepository<StatusHistory>
    {
        private PlannerContext db;

        public StatusHistoryRepository(PlannerContext context)
        {
            this.db = context;
        }

        public IEnumerable<StatusHistory> GetAll()
        {
            return db.StatusHistories.Include(sh=>sh.Task);
        }

        public StatusHistory Get(int id)
        {
            return db.StatusHistories.Find(id);
        }

        public void Create(StatusHistory statuHistory)
        {
            db.StatusHistories.Add(statuHistory);
        }

        public void Update(StatusHistory statuHistory)
        {
            db.Entry(statuHistory).State = EntityState.Modified;
        }
        public IEnumerable<StatusHistory> Find(Func<StatusHistory, Boolean> predicate)
        {
            return db.StatusHistories.Include(sh => sh.Task).Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            StatusHistory statuHistory = db.StatusHistories.Find(id);
            if (statuHistory != null)
                db.StatusHistories.Remove(statuHistory);
        }
    }
}
