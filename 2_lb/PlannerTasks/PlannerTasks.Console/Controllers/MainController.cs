using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlannerTasks.BLL.Services;
using PlannerTasks.BLL.Interfaces;
using PlannerTasks.BLL.DTO;
using PlannerTasks.BLL.Infrastructure;
using PlannerTasks.Console.Models;
using Ninject;
using Ninject.Modules;
using AutoMapper;
using PlannerTasks.DAL.Entities;

namespace PlannerTasks.Console.Controllers
{
    public class MainController
    {
        /*ITaskService orderService;
        public MainController(ITaskService serv)
        {
            orderService = serv;
        }
        public TaskViewModel MakeTask(int? id)
        {
            try
            {
                Mapper.CreateMap<EmployeeDTO, TaskViewModel>()
                    .ForMember("PhoneId", opt => opt.MapFrom(src => src.Id));
                var order = Mapper.Map<EmployeeDTO, TaskViewModel>(orderService.GetEmployee(id));
                return order;
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
        }
        public void MakeOrder(TaskViewModel order, priorityValue)
        {
            try
            {
                Mapper.CreateMap<TaskViewModel, TaskDTO>();
                var taskDto = Mapper.Map<TaskViewModel, TaskDTO>(order);
                orderService.MakeTask(taskDto);
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
        }
        protected void Dispose(bool disposing)
        {
            orderService.Dispose();
        }*/
    }
}
