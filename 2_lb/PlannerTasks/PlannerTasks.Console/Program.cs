using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using PlannerTasks.BLL.Infrastructure;
using PlannerTasks.BLL.Services;
using PlannerTasks.BLL.DTO;
using PlannerTasks.Console.Util;

using PlannerTasks.DAL.Repositories;
using PlannerTasks.DAL.EF;
using PlannerTasks.DAL.Entities;

using Ninject;
using Ninject.Modules;
using System.Data.Entity;




namespace PlannerTasks.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Specify path to dataDirectory
            string path = ConfigurationSettings.AppSettings["DataDirectory"];
            //Configurating data_directory path
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            /*var modules = new INinjectModule[] { new TaskModule(), new ServiceModule("PlannerDB2") };
            var kernel = new StandardKernel(modules);
            kernel.Components.GetAll();*/

            /*NinjectModule taskModule = new TaskModule();
            NinjectModule serviceModule = new ServiceModule("PlannerDB2");
            var kernel = new StandardKernel(taskModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));*/

            using (EFUnitOfWork iEfUnitOfWork = new EFUnitOfWork("PlannerDB"))//"PlannerDB"))
            {
                iEfUnitOfWork.Tasks.
                Create(new Task
                {
                    Description = "Hello, World!",
                    CurrentPriority = Priority.High,
                    Status = Status.NotStarted,
                    TimeExecution = new TimeSpan(0, 3, 0, 0),
                    StartTime = DateTime.Now,
                    EmployeeId = 2,
                });
                iEfUnitOfWork.Save();

                iEfUnitOfWork.StatusHistories.Create(new StatusHistory()
                {
                    DateAppearOfStatus = DateTime.Now,
                    Status = Status.NotStarted,
                    TaskId = iEfUnitOfWork.Tasks.GetAll().OrderBy(t=>t.TaskId).Last().TaskId
                });

                iEfUnitOfWork.Save();

                foreach (Employee e in iEfUnitOfWork.Employees.GetAll())
                {
                    System.Console.WriteLine("Employee:");
                    System.Console.WriteLine("{0}, {1}, {2}, {3}, {4}", e.EmployeeId, e.FirstName, e.SecondName, e.BirthDate, e.PhoneNumber);

                    foreach (Task t in e.Tasks)
                    {
                        System.Console.WriteLine("Task:");
                        System.Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", t.TaskId, t.EmployeeId,
                            t.Description, t.CurrentPriority, t.Status, t.TimeExecution, t.StartTime);
                        System.Console.WriteLine("StatusHistories:");
                        foreach (StatusHistory hs in t.HistoryStatus)
                        {
                            System.Console.WriteLine("{0}, {1}, {2}, {3}", hs.StatusHistoryId, hs.Status,
                                hs.DateAppearOfStatus, hs.TaskId);
                        }
                    }
                }
                System.Console.ReadKey();
            }

        /*            using (PlannerContext db = new PlannerContext("PlannerDB"))
                    {
                        db.SaveChanges();
                        System.Console.ReadKey();
                    }*/
/*        SqlException: The INSERT statement conflicted with the FOREIGN KEY constraint 
        "FK_dbo.StatusHistories_dbo.Tasks_TaskId".The conflict occurred 
            in database "TestConnection", table "dbo.Tasks", column 'TaskId'.*/
        }
    }
}