using System;
using SConsole = System.Console;// псевдонім
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

            var modules = new INinjectModule[] { new ServiceModule("PlannerDB"), new TaskModule(), new EmployeeModule(), new StatusHistoryModule()};
            var kernel = new StandardKernel(modules);

            ITaskService taskService = kernel.Get<ITaskService>();
            IEmployeeService employeeService = kernel.Get<IEmployeeService>();
            IStatusHistoryService statusHistoryService = kernel.Get<IStatusHistoryService>();

            MainController controller = new MainController(taskService, employeeService, statusHistoryService);

            ChoseMainAction(controller);

            System.Console.ReadKey();
        }

        /// <summary>
        /// Output manu to console
        /// </summary>
        static void MainMenu()
        {
            System.Console.WriteLine("MainMenu");
            System.Console.WriteLine("Choose needed action and input number of action.");
            System.Console.WriteLine("1. Show all employees with their tasks.");
            System.Console.WriteLine("2. Add new task.");
            System.Console.WriteLine("3. Delete task.");
            System.Console.WriteLine("4. Change status of task.");
            System.Console.WriteLine("5. Show history of statuses for task.");
            System.Console.WriteLine("6. Shut down application.");
            System.Console.Write("Number of action: ");
        }
        /// <summary>
        /// It calls performing the selected action
        /// </summary>
        /// <param name="controller"></param>
        static void ChoseMainAction(MainController controller)
        {
            SConsole.Clear();
            MainMenu();
            String mainCommand = SConsole.ReadLine();
            switch (mainCommand)
            {
                case "1":
                    SConsole.Clear();
                    ShowAllEmployeesWithTasksAction(controller);
                    break;
                case "2":
                    SConsole.Clear();
                    MakeTaskAction(controller);
                    break;
                case "3":
                    SConsole.Clear();
                    DeleteTaskAction(controller);
                    break;
                case "4":
                    SConsole.Clear();
                    ChangeStatusOfTaskAction(controller);
                    break;
                case "5":
                    SConsole.Clear();
                    ShowHistoryOfStatusesAction(controller);
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    SConsole.Clear();
                    SConsole.WriteLine("You entered invalid value of action. Please, enter number of action between 1 and 5.\n");
                    ChoseMainAction(controller);
                    break;
            }
        }

        /// <summary>
        /// It gives employees and their tasks from DB and print to console 
        /// </summary>
        /// <param name="controller">Allows modify entities</param>
        static void ShowAllEmployeesWithTasksAction(MainController controller)
        {
            IEnumerable<EmployeeDTO> employeeDTOs = controller.GetEmployees();
            foreach (EmployeeDTO e in employeeDTOs)
            {
                System.Console.WriteLine("Employee:");
                System.Console.WriteLine("Id: {0}, Fist name: {1}, Second name: {2}, Birthdate: {3}, PhoneNumber: {4}", e.EmployeeId, e.FirstName, e.SecondName, e.BirthDate, e.PhoneNumber);

                foreach (TaskDTO t in controller.GetAllTaskForGivenEmployee(e.EmployeeId))
                {
                    System.Console.WriteLine("Task:");
                    System.Console.WriteLine("TaskId = {0}, Description: {1}, Priority of task: {2}, Status of task: {3}, Time for execution: {4}",
                        t.TaskId, t.Description, t.CurrentPriority, t.Status, t.TimeExecution);
                    System.Console.WriteLine("  History of status for task with id = {0}:", t.TaskId);
                    Int32 count = 1;
                    foreach (StatusHistoryDTO hs in controller.GetAllStatusHistoryForGivenTask(t.TaskId))
                    {
                        System.Console.Write("   Status №{0}: Date of appear: {1}, value of status: ", count, hs.DateAppearOfStatus);
                        switch (hs.Status)
                        {
                            case 0:
                                SConsole.WriteLine("NotStarted");
                                break;
                            case 1:
                                SConsole.WriteLine("OnExecution");
                                break;
                            case 2:
                                SConsole.WriteLine("OnTesting");
                                break;
                            case 3:
                                SConsole.WriteLine("Expired");
                                break;
                            case 4:
                                SConsole.WriteLine("Done");
                                break;
                            default:
                                SConsole.WriteLine("HsStatus = {0} or in string format: {1}", hs.Status, hs.Status.ToString());
                                break;
                        }

                        count += 1;
                    }
                }
                SConsole.WriteLine();
            }

            SConsole.Write("\nPlease, enter any key for returning to main the menu...");
            var key = SConsole.ReadKey();
            ChoseMainAction(controller);
        }

        /// <summary>
        /// It takes fields for new employee and uses controller to add new employee
        /// </summary>
        /// <param name="controller">Allows modify entities and DB</param>
        static void MakeTaskAction(MainController controller)
        {
            SConsole.WriteLine("Make task action.");
            bool flagInputCorrectValue = false;
            System.Console.Write("Enter identifier of employee: ");
            Int32 employeeId;
            flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out employeeId);
            while (employeeId <= 0 || !flagInputCorrectValue)
            {
                SConsole.Write("WARNING! You entered invalid value of identifier of employee.\nPlease, enter integer value, which more than 0: ");
                flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out employeeId);
            }

            System.Console.Write("Description of task: ");
            String description = System.Console.ReadLine();

            System.Console.Write("Enter expected execution hours(e.g. 1, 2 etc.): ");
            Int32 expectedExecutionHours;
            flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out expectedExecutionHours);
            while (!flagInputCorrectValue || expectedExecutionHours <= 0)
            {
                SConsole.Write("WARNING! You entered invalid value of hours.\nPlease, enter integer value, which more than 0: ");
                flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out expectedExecutionHours);
            }

            System.Console.Write("Enter value of priority of task(from 1(small priority) to 3(big priority): ");
            Int32 priorityValue;
            flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out priorityValue);
            while (!flagInputCorrectValue || priorityValue <= 0 || priorityValue > 3)
            {
                SConsole.Write("WARNING! You entered invalid value of priority.\nPlease, enter integer value between 1 and 3: ");
                flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out priorityValue);
            }

            try
            {
                controller.MakeTask(new TaskDTO()
                {
                    EmployeeId = employeeId,
                    Description = description,
                    CurrentPriority = priorityValue,
                    TimeExecution = new TimeSpan(0, expectedExecutionHours, 0),
                    Status = 0
                });
                SConsole.WriteLine("New task was successfully added.");
            }
            catch (NotExistEmployeeWithIdException ex)
            {
                SConsole.WriteLine("New task wasn`t added.");
                SConsole.WriteLine("Exception type: {0}, message: {1}", ex.GetType().ToString(), ex.Message);
            }

            SConsole.Write("\nPlease, enter any key for returning to the main menu...");
            var key = SConsole.ReadKey();
            ChoseMainAction(controller);
        }

        /// <summary>
        /// It call method of main controller to delete task with given identifier
        /// </summary>
        /// <param name="controller">Allows modify entities and DB</param>
        static void DeleteTaskAction(MainController controller)
        {
            SConsole.WriteLine("Delete task action:");
            bool flagInputCorrectValue = false;
            System.Console.Write("Enter identifier of task: ");
            Int32 taskId;
            flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out taskId);
            while (taskId <= 0 || !flagInputCorrectValue)
            {
                SConsole.Write("WARNING! You entered invalid value of identifier of task.\nPlease, enter integer value, which more than 0: ");
                flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out taskId);
            }

            try
            {
                controller.DeleteTask(taskId);
                SConsole.WriteLine("Task with id {0} was deleted.", taskId);
            }
            catch(NotExistTaskWithIdException ex)
            {
                SConsole.WriteLine("Task wasn`t deleted.");
                SConsole.WriteLine("Exception type: {0}, message: {1}", ex.GetType().ToString(), ex.Message);
            }

            SConsole.Write("\nPlease, enter any key for returning to the main menu...");
            var key = SConsole.ReadKey();
            ChoseMainAction(controller);
        }

        /// <summary>
        /// Allows change status of task with given identifier to entered status type
        /// </summary>
        /// <param name="controller">Allows modify entities and DB</param>
        static void ChangeStatusOfTaskAction(MainController controller)
        {
            SConsole.WriteLine("Change status of task action.");
            bool flagInputCorrectValue;
            SConsole.Write("Enter id of task, which status will be modified: ");
            Int32 taskId;
            flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out taskId);
            while (taskId <= 0 || !flagInputCorrectValue)
            {
                SConsole.Write("WARNING! You entered invalid value of identifier of task.\nPlease, enter integer value, which more than 0: ");
                flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out taskId);
            }

            List<String> listOfStatus = new List<String> { "OnExecution", "OnTesting", "Done" };
            SConsole.Write("Enter new status of task(list of status: OnExecution, OnTesting, Expired, Done): ");
            String statusValue = System.Console.ReadLine();
            while (!listOfStatus.Contains(statusValue))
            {
                SConsole.Write("WARNING! You entered invalid value of new status.\nPlease, enter value from the list: 'OnExecution, OnTesting, Expired, Done': ");
                statusValue = System.Console.ReadLine();
            }

            try
            {
               switch(statusValue)
                {
                    case "OnExecution":
                        controller.SetOnExecutionStatus(taskId);
                        break;
                    case "OnTesting":
                        controller.SetOnTestingStatus(taskId);
                        break;
                    case "Expired":
                        controller.SetExpiredStatus(taskId);
                        break;
                    case "Done":
                        controller.SetDoneStatus(taskId);
                        break;
                }

                SConsole.WriteLine("Status of task with identifier = {0} is successfully modified.", taskId);
            }
            catch (NotExistTaskWithIdException ex)
            {
                System.Console.WriteLine("Exception type is {0} with message: {1}", ex.GetType().ToString(), ex.Message);
                ChoseMainAction(controller);
            }

            SConsole.Write("\nPlease, enter any key for returning to the main menu...");
            var key = SConsole.ReadKey();
            ChoseMainAction(controller);
        }

        /// <summary>
        /// It takes statuses about task with given identifier.
        /// </summary>
        /// <param name="controller">>Allows modify entities</param>
        static void ShowHistoryOfStatusesAction(MainController controller)
        {
            SConsole.WriteLine("History of status action.");
            bool flagInputCorrectValue;
            SConsole.Write("Enter id of needed task: ");
            Int32 taskId;
            flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out taskId);
            while (taskId <= 0 || !flagInputCorrectValue)
            {
                SConsole.Write("WARNING! You entered invalid value of identifier of task.\nPlease, enter integer value, which more 0: ");
                flagInputCorrectValue = Int32.TryParse(System.Console.ReadLine(), out taskId);
            }

            try
            {
                IEnumerable<StatusHistoryDTO> statusHistoryDTOs = controller.GetAllStatusHistoryForGivenTask(taskId);
                System.Console.WriteLine("StatusHistories for task with id = {0}", taskId);
                Int32 count = 1;
                foreach (StatusHistoryDTO hs in statusHistoryDTOs)
                {
                    System.Console.Write("   Status №{0}: Date of appear: {1}, value of status: ", count, hs.DateAppearOfStatus);
                    switch ((int)hs.Status)
                    {
                        case 0:
                            SConsole.WriteLine("NotStarted");
                            break;
                        case 1:
                            SConsole.WriteLine("OnExecution");
                            break;
                        case 2:
                            SConsole.WriteLine("OnTesting");
                            break;
                        case 3:
                            SConsole.WriteLine("Expired");
                            break;
                        case 4:
                            SConsole.WriteLine("Done");
                            break;
                    }

                    count += 1;
                }
            }
            catch (NotExistTaskWithIdException ex)
            {
                System.Console.WriteLine("Exception type is {0} with message: {1}", ex.GetType().ToString(), ex.Message);
            }

            SConsole.Write("\nPlease, enter any key for returning to the main menu...");
            var key = SConsole.ReadKey();
            ChoseMainAction(controller);
        }
    }
}