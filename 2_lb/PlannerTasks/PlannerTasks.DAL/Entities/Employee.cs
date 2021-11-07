﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.DAL.Entities
{
    public enum TitleOfCourtesy
    {
        Dr,
        Ms
    }

    public enum BusyRate
    {
        LowBusy,
        MediumBusy,
        HighBusy
    }
    public class Employee
    {
        public Int32 EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String SecondName { get; set; }
        public TitleOfCourtesy TitleOfCourtesy { get; set; }
        public DateTime BirthDate { get; set; }
        public String HomePhone { get; set; }
        public BusyRate BusyRate { get; set; }
        public virtual ICollection<Task> Tasks { get; set; } // using 'virtual' for realization lazy loading
    }
}
