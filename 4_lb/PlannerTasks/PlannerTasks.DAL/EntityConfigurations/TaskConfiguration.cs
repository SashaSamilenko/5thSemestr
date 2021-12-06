using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerTasks.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlannerTasks.DAL.EntityConfigurations
{
    /// <summary>
    /// Class TaskConfiguration
    /// Is configure properties of task
    /// </summary>
    internal class TaskConfiguration : EntityTypeConfiguration<Task>
    {
        /// <summary>
        /// Constructor without parameters
        /// That configures properties of task
        /// and relations between Task and HistoryStatuses,
        /// and between Task and Employee
        /// </summary>
        public TaskConfiguration()
        {
            this.HasRequired(t => t.Employee)
                .WithMany(e => e.Tasks);
            this.HasMany(t => t.HistoryStatuses)
                .WithRequired(sh => sh.Task);
            this.Property(t => t.Description).HasMaxLength(200);
            this.Property(p => p.StartTime)
                .HasColumnType("datetime2")
                .HasPrecision(0);
            this.Property(t => t.TimeExecution)
                .IsOptional()
                .HasPrecision(0);
            this.Property(t => t.CurrentPriority)
                .IsOptional();
        }
    }
}