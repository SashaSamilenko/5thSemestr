using PlannerTasks.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.BLL.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDTO GetEmployee(int id);
        IEnumerable<EmployeeDTO> GetEmployees();
        IEnumerable<TaskDTO> GetAllTaskForGivenEmployee(int id);
        void Dispose();
    }
}
