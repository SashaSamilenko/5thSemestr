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
    /// Class EmployeeConfiguration
    /// Is configure properties of employee
    /// and relations between Employee and Task
    /// </summary>
    internal class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        /// <summary>
        /// Constructor without parameters
        /// That configures properties of employee
        /// </summary>
        public EmployeeConfiguration()
        {
            this.Property(e => e.FirstName).HasMaxLength(50);
            this.Property(e => e.SecondName).HasMaxLength(70);
            this.Property(e => e.PhoneNumber).HasMaxLength(24);
            this.HasMany(e => e.Tasks)
                .WithRequired(t => t.Employee);
        }
    }
}