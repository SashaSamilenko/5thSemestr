using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
//using System.Threading.Tasks;
using PlannerTasks.DAL.Entities;

namespace PlannerTasks.DAL.EF
{
    public class MobileContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public MobileContext()
        {
            Database.SetInitializer<MobileContext>(new StoreDbInitializer());
        }
        public MobileContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
