using SynetecAssessmentApi.Services;
using SynetecAssessmentApi.Services.Interfaces;
using Xunit;

namespace SynetecAssessmentApi.Test.UnitTest
{
    public class BonusPoolServiceTest 
    {
        [Fact]
        public void CaculateBonus_WithValidData_ReturnsCalculatedBonusData()
        {
            ICalculateBonus calculateBonus = new BonusPoolService();
            Assert.Equal(4581, calculateBonus.CalculateBonus(50000, 60000, 654750));
        }

        [Fact]
        public void CaculateBonus_WithValidData_ReturnsInvalidData()
        {
            ICalculateBonus calculateBonus = new BonusPoolService();
            Assert.NotEqual(4581, calculateBonus.CalculateBonus(60000, 60000, 654750));
        }
    }
}
