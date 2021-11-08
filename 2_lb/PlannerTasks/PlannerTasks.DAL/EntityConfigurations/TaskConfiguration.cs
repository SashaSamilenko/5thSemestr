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
            this.Property(t => t.Description).HasMaxLength(200);
        }
    }
}