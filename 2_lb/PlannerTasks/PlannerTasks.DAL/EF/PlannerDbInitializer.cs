using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PlannerTasks.DAL.Entities;

namespace PlannerTasks.DAL.EF
{
    public class PlannerDbInitializer : DropCreateDatabaseIfModelChanges<TasksContext>//DropCreateDatabaseAlways<TasksContext>//
    {
        protected override void Seed(TasksContext db)
        {
            db.Employees.Add(new Employee { FirstName = "Alex", SecondName = "Samilenko",  BirthDate = new DateTime(2002,5,27), HomePhone = "(095) 449-1779" });//TitleOfCourtesy = TitleOfCourtesy.Dr, BusyRate = BusyRate.LowBusy
            db.Employees.Add(new Employee { FirstName = "Tadjat", SecondName = "Margarian", BirthDate = new DateTime(2000, 7, 13), HomePhone = "(095) 449-1778"});//TitleOfCourtesy = TitleOfCourtesy.Dr, BusyRate = BusyRate.MediumBusy
            db.Employees.Add(new Employee { FirstName = "Ivan", SecondName = "Panasenko", BirthDate = new DateTime(2001, 8, 14), HomePhone = "(096) 449-1779"});//TitleOfCourtesy = TitleOfCourtesy.Dr, BusyRate = BusyRate.HighBusy
            db.Employees.Add(new Employee { FirstName = "Artem", SecondName = "Morozov", BirthDate = new DateTime(1996, 1, 21), HomePhone = "(093) 449-1779"});//TitleOfCourtesy = TitleOfCourtesy.Dr, BusyRate = BusyRate.LowBusy
            db.Employees.Add(new Employee { FirstName = "Anna", SecondName = "Surante", BirthDate = new DateTime(2003, 5, 21), HomePhone = "(099) 798-0690"});// TitleOfCourtesy = TitleOfCourtesy.Ms, BusyRate = BusyRate.MediumBusy
        }
    }
}
