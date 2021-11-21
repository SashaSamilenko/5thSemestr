using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using PlannerTasks.BLL.Infrastructure;
using PlannerTasks.BLL.Services;
using PlannerTasks.BLL.DTO;
using PlannerTasks.Console.Util;
using Ninject;
using Ninject.Modules;
//using System.Data.Entity;
using PlannerTasks.BLL.Interfaces;
using PlannerTasks.Console.Controllers;
using PlannerTasks.Console.Models;
using AutoMapper;

namespace PlannerTasks.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Specify path to dataDirectory
            string path = ConfigurationSettings.AppSettings["DataDirectory"];
            //Configuration of data_directory path
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            var modules = new INinjectModule[] { new ServiceModule("TestInjection") };
            var kernel = new StandardKernel(modules);
            kernel.Bind<ITaskService>().To<TaskService>();
            ITaskService taskService = kernel.Get<ITaskService>();

            MainController controller = new MainController(taskService);

            ChoseMainAction(controller);

            /*MainController controller = new MainController(taskService);
            controller.MakeTask(new TaskViewModel()
            {
                EmployeeId = 2,
                Priority = 1,
                Status = 0,
                Description = "My First Injection test",
                TimeExecution = new TimeSpan(0,2,0,0)
            });*/

            /*foreach (EmployeeDTO e in taskService.GetEmployees().ToList())
            {
                System.Console.WriteLine("Employee:");
                System.Console.WriteLine("{0}, {1}, {2}, {3}, {4}", e.EmployeeId, e.FirstName, e.SecondName, e.BirthDate, e.PhoneNumber);

                foreach (TaskDTO t in taskService.GetAllTaskForGivenEmployee(e.EmployeeId))
                {
                    System.Console.WriteLine("Task:");
                    System.Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}", t.TaskId, t.EmployeeId,
                        t.Description, t.CurrentPriority, t.Status, t.TimeExecution);
                    System.Console.WriteLine("StatusHistories:");
                    foreach (StatusHistoryDTO hs in taskService.GetAllStatusHistoryForGivenTask(t.EmployeeId))
                    {
                        System.Console.WriteLine("{0}, {1}, {2}, {3}", hs.StatusHistoryId, hs.Status,
                            hs.DateAppearOfStatus, hs.TaskId);
                    }
                }
            }*/
            System.Console.ReadKey();
        }

        

        static void MainMenu()
        {
            System.Console.WriteLine("MainMenu");
            System.Console.WriteLine("Chose needed action and input number of action.");
            System.Console.WriteLine("1. Show all employees with their tasks.");
            System.Console.WriteLine("2. Add new task.");
            System.Console.WriteLine("3. Change status of task.");
            System.Console.WriteLine("4. Show history of statuses for task.");
            System.Console.WriteLine("5. Shot down application.");
            System.Console.Write("Number of action: ");
        }

        static void MakeTaskAction(MainController controller)
        {
            System.Console.Write("Enter identifier of employee: ");
            Int32 employeeId = Int32.TryParse(System.Console.ReadLine(), out employeeId) ? employeeId : Int32.MinValue; 

            System.Console.Write("Description of task: ");
            String description = System.Console.ReadLine();

            System.Console.Write("Enter expected execution hours(e.g. 3): ");
            Int32 expectedExecutionHours;
            try
            {
                expectedExecutionHours = Int32.Parse(System.Console.ReadLine());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Entered value is not integer or has value more than max or less than min. Value of execution hours is null instead of given value.");
            }

            System.Console.Write("Enter value of priority of task(from 1(small priority) to 3(big priority)");
            Int32 priorityValue = Int32.TryParse(System.Console.ReadLine(), out priorityValue) ? priorityValue : Int32.MinValue;
            if (priorityValue != Int32.MinValue && priorityValue<=1 && priorityValue>=3)
            {
                priorityValue = Int32.MinValue;
            }

            TaskViewModel taskViewModel = new TaskViewModel()
            {
                EmployeeId = employeeId,
                Description = description,
                //TimeExecution = (expectedExecutionHours!= null)?new TimeSpan(0,expectedExecutionHours,0)),
                //Priority = priorityValue
            };
            controller.MakeTask(taskViewModel);
        }

        static void ChoseMainAction(MainController controller)
        {
            MainMenu();
            String mainCommand = System.Console.ReadLine();
            switch (mainCommand)
            {
                case "1": System.Console.Clear(); 
                    break;
                case "2": System.Console.Clear(); 
                    break;
                case "3": System.Console.Clear(); 
                    break;
                case "4": System.Console.Clear(); 
                    ShowHistoryOfStatuses(controller);
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default: System.Console.Clear();
                    System.Console.WriteLine("You entered invalid value of action.\n Please, enter number of action between 1 and 5.");
                    ChoseMainAction(controller);
                    break;
            }
        }
       
        static void ShowAllEmployees(MainController controller)
        {
            IEnumerable<EmployeeViewModel> employeeViewModels = controller.GetEmployees();
            foreach (EmployeeViewModel e in employeeViewModels)
            {
                System.Console.WriteLine("Employee:");
                System.Console.WriteLine("{0}, {1}, {2}, {3}, {4}", e.EmployeeId, e.FirstName, e.SecondName, e.BirthDate, e.PhoneNumber);

                foreach (TaskViewModel t in controller.GetAllTaskForGivenEmployee(e.EmployeeId))
                {
                    System.Console.WriteLine("Task:");
                    System.Console.WriteLine("TaskId = {0},\n Description: {1},\n Priority of task: {2},\n Status of task: {3}, \nTime for execution: {4}", t.TaskId,
                        t.Description, t.Priority, t.Status, t.TimeExecution);
                    System.Console.WriteLine("  History of status for task with id = {0}:", t.TaskId);
                    Int32 count = 0;
                    foreach (StatusHistoryViewModel hs in controller.GetAllStatusHistoryForGivenTask(t.TaskId))
                    {
                        System.Console.WriteLine("   Status #{0}: {1}", count, hs.Status);
                        count += 1;
                    }
                }
            }
        }
        static void ShowHistoryOfStatuses(MainController controller)
        {
            try
            {
                System.Console.Write("Enter id of needed task: ");
                Int32 id = Int32.Parse(System.Console.ReadLine());

                IEnumerable<StatusHistoryViewModel> statusHistoryViewModels = controller.GetAllStatusHistoryForGivenTask(id);
                System.Console.WriteLine("StatusHistories for task with id = {0}", id);
                Int32 count = 0;
                foreach (StatusHistoryViewModel hs in statusHistoryViewModels)
                {
                    System.Console.WriteLine("Status #{0}: {1}", count, hs.Status);
                    count += 1;
                }
            }
            catch (ArgumentNullException e)
            {
                System.Console.Clear();
                System.Console.WriteLine("Entered value is nullable.\n Please repeat entering.");
                ShowHistoryOfStatuses(controller);
            }
            catch (ArgumentException e)
            {
                System.Console.Clear();
                System.Console.WriteLine("Entered value is not number.\n Please repeat entering.");
                ShowHistoryOfStatuses(controller);
            }
            catch (OverflowException e)
            {
                System.Console.Clear();
                System.Console.WriteLine("Entered value is more than max value or min value for Int32 type.\n Please repeat entering.");
                ShowHistoryOfStatuses(controller);
            }
            catch (NotExistTaskWithIdException e)
            {
                System.Console.Clear();
                System.Console.WriteLine(e);
                System.Console.WriteLine("Please repeat entering.");
                ShowHistoryOfStatuses(controller);
            }
        }
    }
}