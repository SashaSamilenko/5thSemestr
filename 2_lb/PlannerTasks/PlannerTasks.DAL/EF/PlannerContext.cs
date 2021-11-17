using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
//using System.Threading.Tasks;
using PlannerTasks.DAL.Entities;
using PlannerTasks.DAL.EntityConfigurations;

namespace PlannerTasks.DAL.EF
{
    /// <summary>
    /// Class TaskContext
    /// Presents context of database
    /// Has DbSet of entities
    /// </summary>
    public class PlannerContext: DbContext
    {
        /// <summary>
        /// DbSet of entity Employee
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// DbSet of entity Task
        /// </summary>
        public DbSet<Task> Tasks { get; set; }

        /// <summary>
        /// DbSet of entity StatusHistory
        /// </summary>
        public DbSet<StatusHistory> StatusHistories { get; set; }

        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public PlannerContext()
        {}

        /// <summary>
        /// Static constructor without parameters, which initializes DB
        /// </summary>
        static PlannerContext()
        {
            Database.SetInitializer<PlannerContext>(new PlannerDbInitializer());
        }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="connectionString">Is an connection string to DB</param>
        public PlannerContext(string connectionString)
            : base(connectionString)
        {}

        /// <summary>
        /// Overriding of method OnModelCreating
        /// to configure entities before creating model
        /// </summary>
        /// <param name="modelBuilder">Database model builder. It builds of model.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new TaskConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
