using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using PlannerTasks.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlannerTasks.DAL.EntityConfigurations
{
    internal class TaskConfiguration : EntityTypeConfiguration<Task>
    {
        public TaskConfiguration()
        {
            this.HasRequired(t => t.Employee)
                .WithMany(e => e.Tasks);
            this.HasMany(t => t.HistoryStatus)
                .WithRequired(sh => sh.Task);
            this.Property(t => t.Description).HasMaxLength(200);
            this.Property(p => p.StartTime)
                .HasColumnType("datetime2")
                .HasPrecision(0);
        }
    }
}