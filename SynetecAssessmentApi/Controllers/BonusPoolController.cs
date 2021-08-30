using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        private readonly IEmployee _employee;
        private readonly ICalculateBonus _calculateBonus;
        public BonusPoolController(IEmployee employee,ICalculateBonus calculateBonus)
        {
            _employee = employee;
            _calculateBonus = calculateBonus;
        }

        /// <summary>
        /// Returns all employees with Department details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Employee> employeesList = await _employee.GetEmployeesAsync();
            List<EmployeeDto> result = new List<EmployeeDto>();
            foreach (var employee in employeesList)
            {
                result.Add(
                    new EmployeeDto
                    {
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    });
            }

            return Ok(result);
        }

        /// <summary>
        /// Calculate provided employee bonus based on TotalBonusPoolAmount param.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            var employee = await _employee.GetEmployeeAsync(request.SelectedEmployeeId);
            if (employee == null)
            {
                return BadRequest("Provided employee Id does not belong to this company");
            }

            var calculatedBonus = _calculateBonus.CalculateBonus(request.TotalBonusPoolAmount, employee.Salary,_employee.GetAllEmployeesSalary());
            return Ok(new BonusPoolCalculatorResultDto
            {
                Employee = new EmployeeDto
                {
                    Fullname = employee.Fullname,
                    JobTitle = employee.JobTitle,
                    Salary = employee.Salary,
                    Department = new DepartmentDto
                    {
                        Title = employee.Department.Title,
                        Description = employee.Department.Description
                    }
                },

                Amount = calculatedBonus
            });
        }
    }
}
