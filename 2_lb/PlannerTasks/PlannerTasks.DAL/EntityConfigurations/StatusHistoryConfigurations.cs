using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlannerTasks.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlannerTasks.DAL.EntityConfigurations
{
    /// <summary>
    /// Class StatusHistoryConfigurations
    /// Is configure properties of statusHistory
    /// </summary>
    public class StatusHistoryConfigurations: EntityTypeConfiguration<StatusHistory>
    {
        /// <summary>
        /// Constructor without parameters
        /// That configures properties of statusHistory
        /// and relations between Task and HistoryStatuses,
        /// </summary>
        public StatusHistoryConfigurations()
        {
            this.HasRequired(e => e.Task)
                .WithMany(s => s.HistoryStatus);
            this.Property(p => p.DateAppearOfStatus)
                .HasColumnType("datetime2")
                .HasPrecision(0);
        }
    }
}
