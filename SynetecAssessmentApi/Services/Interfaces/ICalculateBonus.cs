namespace SynetecAssessmentApi.Services.Interfaces
{
    /// <summary>
    /// Interface for CalculateBonus
    /// </summary>
    public interface ICalculateBonus
    {
        /// <summary>
        /// This method calculate bonus of employee. 
        /// </summary>
        /// <param name="bonusPoolAmount"></param>
        /// <param name="salary"></param>
        /// <param name="totalSalary"></param>
        /// <returns></returns>
        int CalculateBonus(int bonusPoolAmount, int salary, int totalSalary);
    }
}
