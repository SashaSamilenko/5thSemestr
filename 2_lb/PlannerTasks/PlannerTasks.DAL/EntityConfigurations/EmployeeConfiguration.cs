using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlannerTasks.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlannerTasks.DAL.EntityConfigurations
{
    internal class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            this.Property(e => e.FirstName).HasMaxLength(50);
            this.Property(e => e.SecondName).HasMaxLength(70);
            this.Property(e => e.HomePhone).IsFixedLength().HasMaxLength(14);
        }
    }
}