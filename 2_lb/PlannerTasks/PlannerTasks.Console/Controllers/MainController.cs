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

namespace PlannerTasks.Console.Controllers
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
        public IEnumerable<EmployeeViewModel> GetEmployees()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(taskService.GetEmployees());
        }

        public EmployeeViewModel GetEmployee(int id)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
                return mapper.Map<EmployeeDTO, EmployeeViewModel>(taskService.GetEmployee(id));
            }
            catch (NotExistEmployeeWithIdException e)
            {
                throw e;
            }
        }
        public IEnumerable<TaskViewModel> GetAllTaskForGivenEmployee(int id)
        {
            try
            {
                IEnumerable<TaskDTO> taskDtos = taskService.GetAllTaskForGivenEmployee(id);
                MapperConfiguration config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<TaskDTO, TaskViewModel>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<IEnumerable<TaskDTO>, IEnumerable<TaskViewModel>>(taskDtos);
            }
            catch (NotExistEmployeeWithIdException e)
            {
                throw e;
            }
            
        }

        public IEnumerable<StatusHistoryViewModel> GetAllStatusHistoryForGivenTask(int id)
        {
            IEnumerable<StatusHistoryDTO> statusHistoryDtos = taskService.GetAllStatusHistoryForGivenTask(id);
            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<StatusHistoryDTO, StatusHistoryViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<StatusHistoryDTO>, IEnumerable<StatusHistoryViewModel>>(statusHistoryDtos);
        }

        public void SetOnExecutionStatus(int id)
        {
            taskService.SetOnExecutionStatus(id);
        }

        public void SetOnTestingStatus(int id)
        {
            taskService.SetOnTestingStatus(id);
        }

        public void SetExpiredStatus(int id)
        {
            taskService.SetExpiredStatus(id);
        }

        public void SetDoneStatus(int id)
        {
            taskService.SetDoneStatus(id);
        }

        protected void Dispose(bool disposing)
        {
            taskService.Dispose();
        }
    }
}
