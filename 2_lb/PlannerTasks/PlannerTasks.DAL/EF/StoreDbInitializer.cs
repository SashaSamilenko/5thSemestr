using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PlannerTasks.DAL.Entities;

namespace PlannerTasks.DAL.EF
{
    public class StoreDbInitializer: DropCreateDatabaseIfModelChanges<MobileContext>
    {
        protected override void Seed(MobileContext db)
        {
            db.Employees.Add(new Employee { FirstName = "Alex", SecondName = "Samilenko", TitleOfCourtesy = TitleOfCourtesy.Dr, BirthDate = new DateTime(2002,5,27), HomePhone = "(095) 449-1779", BusyRate = BusyRate.LowBusy });
            db.Employees.Add(new Employee { FirstName = "Tadjat", SecondName = "Margarian", TitleOfCourtesy = TitleOfCourtesy.Dr, BirthDate = new DateTime(2000, 7, 13), HomePhone = "(095) 449-1778", BusyRate = BusyRate.MediumBusy });
            db.Employees.Add(new Employee { FirstName = "Ivan", SecondName = "Panasenko", TitleOfCourtesy = TitleOfCourtesy.Dr, BirthDate = new DateTime(2001, 8, 14), HomePhone = "(096) 449-1779", BusyRate = BusyRate.HighBusy });
            db.Employees.Add(new Employee { FirstName = "Artem", SecondName = "Morozov", TitleOfCourtesy = TitleOfCourtesy.Dr, BirthDate = new DateTime(1996, 1, 21), HomePhone = "(093) 449-1779", BusyRate = BusyRate.LowBusy });
            db.Employees.Add(new Employee { FirstName = "Anna", SecondName = "Surante", TitleOfCourtesy = TitleOfCourtesy.Ms, BirthDate = new DateTime(2003, 5, 21), HomePhone = "(099) 798-0690", BusyRate = BusyRate.MediumBusy });
            db.SaveChanges();
        }
    }
}
