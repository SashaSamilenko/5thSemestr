using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlannerTasks.BLL.Services;
using PlannerTasks.BLL.Interfaces;
using PlannerTasks.BLL.DTO;
using PlannerTasks.BLL.Infrastructure;
using PlannerTasks.WPF.Models;
using Ninject;
using Ninject.Modules;
using AutoMapper;

namespace PlannerTasks.WPF.Controllers
{
    public class MainController
    {
        ITaskService taskService;
        public MainController(ITaskService serv)
        {
            taskService = serv;
        }
        public void MakeTask(TaskViewModel task)
        {
            try
            {
                MapperConfiguration config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<TaskViewModel, TaskDTO>();
                });

                IMapper mapper = config.CreateMapper();
                TaskViewModel source = new TaskViewModel();
                TaskDTO taskDto = mapper.Map<TaskViewModel, TaskDTO>(source);

                taskService.MakeTask(taskDto);
            }
            catch (NotExistEmployeeWithIdException ex)
            {
                throw ex;
            }
        }
        protected void Dispose(bool disposing)
        {
            taskService.Dispose();
        }
    }
}
