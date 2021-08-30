using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    /// <summary>
    /// This class contains method related to employee operation.
    /// </summary>
    public class EmployeeService : IEmployee
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Returns returns one employee details with department based on EmployeeId param.
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            var employee = await _appDbContext
             .Employees
             .Include(e => e.Department)
             .FirstOrDefaultAsync(m => m.Id == employeeId);
            return employee;
        }

        /// <summary>
        /// Retuens all employee details with department.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _appDbContext
             .Employees
             .Include(e => e.Department)
             .ToListAsync();
            return employees;
        }

        /// <summary>
        /// Returns sum of all employee salary from employee table.
        /// </summary>
        /// <returns></returns>
        public int GetAllEmployeesSalary()
        {
            return (int)_appDbContext.Employees.Sum(item => item.Salary);
        }
    }
}
