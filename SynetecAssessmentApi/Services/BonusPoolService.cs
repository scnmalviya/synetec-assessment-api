using SynetecAssessmentApi.Services.Interfaces;

namespace SynetecAssessmentApi.Services
{
    /// <summary>
    /// Calculate employee bonus based define BonusPoolAmount, employee Salary, and sum of all employees salary.
    /// </summary>
    public class BonusPoolService: ICalculateBonus
    {
        /// <summary>
        /// Calculate bonus for an employee
        /// </summary>
        /// <param name="bonusPoolAmount">Received this input parameter from api consumer.</param>
        /// <param name="salary">Employee object whom bonus need to be calculated.</param>
        /// <param name="totalSalary">Sum of all Employees salary</param>
        /// <returns></returns>
        public int CalculateBonus(int bonusPoolAmount, int salary, int totalSalary)
        {
            // Calculate the bonus allocation for the employee.
            decimal bonusPercentage = (decimal)salary / (decimal)totalSalary;
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);
            return bonusAllocation;
        }
    }
}
