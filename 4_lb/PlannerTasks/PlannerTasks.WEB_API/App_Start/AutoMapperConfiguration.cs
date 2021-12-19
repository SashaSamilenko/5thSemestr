using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web;

using PlannerTasks.WEB_API.Models;
using PlannerTasks.BLL.DTO;

namespace PlannerTasks.WEB_API.App_Start
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeViewModel, EmployeeDTO>();
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
                cfg.CreateMap<TaskViewModel, TaskDTO>().ForMember("TimeExecution", te => te.MapFrom(c => new TimeSpan(c.TimeExecution, 0, 0)));
                cfg.CreateMap<TaskDTO, TaskViewModel>().ForMember("TimeExecution", te => te.MapFrom(c => c.TimeExecution.Hours));
                cfg.CreateMap<StatusHistoryViewModel, StatusHistoryDTO>();
                cfg.CreateMap<StatusHistoryDTO, StatusHistoryViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}