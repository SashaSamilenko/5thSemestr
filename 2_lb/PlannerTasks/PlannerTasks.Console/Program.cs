using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlannerTasks.DAL.Repositories;
using PlannerTasks.DAL.Entities;

namespace PlannerTasks.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            EFUnitOfWork efUnitOfWork = new EFUnitOfWork();//("PlannerTaskDataBase");
            //efUnitOfWork.Employees.Update(new Employee { FirstName = "Oleg", SecondName = "Samilenko", TitleOfCourtesy = TitleOfCourtesy.Dr, BirthDate = new DateTime(1992, 7, 23), HomePhone = "(066) 577-7778", BusyRate = BusyRate.MediumBusy});
            //efUnitOfWork.Save();
        }
    }
}
