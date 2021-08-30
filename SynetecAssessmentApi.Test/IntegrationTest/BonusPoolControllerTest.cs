using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecAssessmentApi.Controllers;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace SynetecAssessmentApi.Test.IntegrationTest
{
    public class BonusPoolControllerTest
    {

        private readonly Mock<ICalculateBonus> calcBonusMockObject;
        private readonly Mock<IEmployee> empMockObject;
        
        public BonusPoolControllerTest()
        {
            calcBonusMockObject = new Mock<ICalculateBonus>();
            empMockObject = new Mock<IEmployee>();
        }

        [Fact]
        public void BonusPoolController_WithoutEmployeeObject_ReturnsBadRequestObjectResult()
        {
            // Arrange
            // Do not setup Employee mock and Calculate mock object then Employee does not exist and return BadRequestObjectReulst from Controller.
            var controller = new BonusPoolController(empMockObject.Object, calcBonusMockObject.Object);

            // Act
            var result = controller.CalculateBonus(new CalculateBonusDto() { SelectedEmployeeId = 1, TotalBonusPoolAmount = 100 });

            // Assert
            BadRequestObjectResult badRequestObjectResult = result.Result as BadRequestObjectResult;
            Assert.Equal(400, badRequestObjectResult.StatusCode);
            Assert.Equal("Provided employee Id does not belong to this company", badRequestObjectResult.Value);
        }

        [Fact]
        public async Task BonusPoolController_WithEmployeeObject_ReturnsOKResult()
        {
            // Arrange
            var employee = new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1);
            var department = new Department(1, "Finance", "The finance department for the company");
            employee.Department = department;
            empMockObject.Setup(t => t.GetEmployeeAsync(1)).Returns(Task.FromResult(employee));
            calcBonusMockObject.Setup(t => t.CalculateBonus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(4581);
            
            // Act
            var controller = new BonusPoolController(empMockObject.Object, calcBonusMockObject.Object);
            var result = await controller.CalculateBonus(new CalculateBonusDto() { SelectedEmployeeId = 1, TotalBonusPoolAmount = 50000 });
            var okresult = result as ObjectResult;
            BonusPoolCalculatorResultDto bonusPoolCalculatorResultDto = (BonusPoolCalculatorResultDto)okresult.Value;

            // Assert
            Assert.Equal("John Smith", bonusPoolCalculatorResultDto.Employee.Fullname);
            Assert.Equal("Accountant (Senior)", bonusPoolCalculatorResultDto.Employee.JobTitle);
            Assert.Equal(60000, bonusPoolCalculatorResultDto.Employee.Salary);
            Assert.Equal("Finance", bonusPoolCalculatorResultDto.Employee.Department.Title);
            Assert.Equal("The finance department for the company", bonusPoolCalculatorResultDto.Employee.Department.Description);
            Assert.Equal(4581, bonusPoolCalculatorResultDto.Amount);
        }
    }
}
