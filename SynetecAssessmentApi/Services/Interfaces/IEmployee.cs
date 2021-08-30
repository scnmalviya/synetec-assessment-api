using SynetecAssessmentApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services.Interfaces
{
    /// <summary>
    /// This Interface contains method related to employee 
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// This method returns all employee details with department.  
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        /// <summary>
        /// This method returns one employee details with department based on EmployeeId param.
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        Task<Employee> GetEmployeeAsync(int EmployeeId);

        /// <summary>
        /// This method returns sum of all employee salary from employee table.
        /// </summary>
        /// <returns></returns>
        int GetAllEmployeesSalary();
    }
}
