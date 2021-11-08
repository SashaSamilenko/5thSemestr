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
    public class TasksContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public TasksContext()
        {

        }
        static TasksContext()
        {
            Database.SetInitializer<TasksContext>(new PlannerDbInitializer());
        }
        public TasksContext(string connectionString)
            : base(connectionString)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TasksContext>(new PlannerDbInitializer());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new TaskConfiguration());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Task>().
                Property(p => p.StartTime)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .IsRequired();
        }
    }
}
