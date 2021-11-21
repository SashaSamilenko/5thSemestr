using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlannerTasks.DAL.Entities;
using PlannerTasks.DAL.Interfaces;
using PlannerTasks.DAL.EF;
using System.Data.Entity;

namespace PlannerTasks.DAL.Repositories
{
    /// <summary>
    /// Class EmployeeRepository.
    /// Implements interface IRepository with type value of Employee.
    /// Provides method to interact with DB context
    /// </summary>
    public class EmployeeRepository: IRepository<Employee>
    {
        /// <summary>
        /// DB context.
        /// </summary>
        private PlannerContext db;

        /// <summary>
        /// Constructor with one parameters.
        /// </summary>
        /// <param name="context">Planner context of DB</param>
        public EmployeeRepository(PlannerContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Method return IEnumerable of Employees entities
        /// </summary>
        /// <returns>IEnumerable of Employee entities</returns>
        public IEnumerable<Employee> GetAll()
        {
            return db.Employees.Include(e=>e.Tasks);
        }

        /// <summary>
        /// Get Employees with given Id
        /// </summary>
        /// <param name="id">Identifier of employee</param>
        /// <returns>Employee entity with given Id</returns>
        public Employee Get(int id)
        {
            return db.Employees.Find(id);
        }

        /// <summary>
        /// Adds employee to DB context.
        /// Changes EntityState from Detached=1 to Added=4 for Employee entity
        /// </summary>
        /// <param name="employee">Employee for adding</param>
        public void Create(Employee employee)
        {
            db.Employees.Add(employee);
        }

        /// <summary>
        /// Changes EntityState to Modified=16 for Employee entity
        /// </summary>
        /// <param name="employee">Modified Employee entity</param>
        public void Update(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
        }

        /// <summary>
        /// Finds Employee entities that satisfy the condition
        /// </summary>
        /// <param name="predicate">Enumerable of Employee entities</param>
        /// <returns>IEnumerable of Employee
        /// entities which obey condition(predicate)</returns>
        public IEnumerable<Employee> Find(Func<Employee, Boolean> predicate)
        {
            return db.Employees.Where(predicate).ToList();
        }

        /// <summary>
        /// Remove Employee entity from DBContext and DB with given Id if that entity exists
        /// </summary>
        /// <param name="id">Identifier of Employee entity</param>
        public void Delete(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
                db.Employees.Remove(employee);
        }
    }
}
