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
            string path = ConfigurationSettings.AppSettings["DataDirectory"];
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            /*var modules = new INinjectModule[] { new TaskModule(), new ServiceModule("PlannerDB2") };
            var kernel = new StandardKernel(modules);
            kernel.Components.GetAll();*/

            /*NinjectModule taskModule = new TaskModule();
            NinjectModule serviceModule = new ServiceModule("PlannerDB2");
            var kernel = new StandardKernel(taskModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));*/
            /*
            TaskService taskService = new TaskService(new EFUnitOfWork("PlannerDB2"));
            taskService.MakeTask(new TaskDTO
                {
                    Description = "Check issues",
                    Priority = Priority.Medium,
                    Status = Status.NotStarted,
                    TimeExecution = new TimeSpan(0, 2, 0, 0),
                    StartTime = DateTime.Now,
                    EmployeeId = 3
                }, 1);
            
                var employees = taskService.GetEmployees();
                foreach (EmployeeDTO e in employees)
                {
                    System.Console.WriteLine("Employee:");
                    System.Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", e.EmployeeId, e.FirstName, e.SecondName, e.BirthDate, e.HomePhone, e.BusyRate, e.TitleOfCourtesy);

                    foreach (Task t in e.Tasks)
                    {
                        System.Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", t.TaskId, t.EmployeeId, t.Description, t.Priority, t.Status, t.TimeExecution, t.StartTime);
                    }
                }
                System.Console.ReadKey();
            }*/
            //efUnitOfWork.Employees.Update(new Employee { FirstName = "Oleg", SecondName = "Samilenko", TitleOfCourtesy = TitleOfCourtesy.Dr, BirthDate = new DateTime(1992, 7, 23), HomePhone = "(066) 577-7778", BusyRate = BusyRate.MediumBusy});
            //efUnitOfWork.Save();
            using (TasksContext db = new TasksContext("TestConnection"))//"PlannerDB"))
            {
                var taskRepository = new TaskRepository(db);
                taskRepository.Create(new Task
                {
                    Description = "asdasd",
                    CurrentPriority = Priority.High,
                    Status = Status.NotStarted,
                    TimeExecution = new TimeSpan(0, 3, 0, 0),
                    StartTime = DateTime.Now,
                    EmployeeId = 1
                });
                //EmployeeRepository employeeRepository = new EmployeeRepository(db);
                //employeeRepository.Get(1).Tasks.Add(taskRepository.Get(1));
                //employeeRepository.Update(new Employee { EmployeeId = 1, FirstName = "Oleg", SecondName = "Samilenko", TitleOfCourtesy = TitleOfCourtesy.Dr, BirthDate = new DateTime(1992, 7, 23), HomePhone = "(066) 577-7778", BusyRate = BusyRate.MediumBusy });
                db.SaveChanges();

                var employees = db.Employees;
                foreach (Employee e in employees)
                {
                    System.Console.WriteLine("Employee:");
                    System.Console.WriteLine("{0}, {1}, {2}, {3}, {4}", e.EmployeeId, e.FirstName, e.SecondName, e.BirthDate, e.HomePhone);

                    foreach (Task t in e.Tasks)
                    {
                        System.Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", t.TaskId, t.EmployeeId, t.Description, t.CurrentPriority, t.Status, t.TimeExecution, t.StartTime);
                    }
                }
                System.Console.ReadKey();
            }

        }
    }
}