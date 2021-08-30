using Moq;
using Newtonsoft.Json;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services;
using SynetecAssessmentApi.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace SynetecAssessmentApi.Test.IntegrationTest
{
    public class BonusPoolApiTest: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public BonusPoolApiTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CalculateBonus_When_ValidEmployeeId_ReturnsBPCRDto()
        {
            // Initialize input data with valid EmployeeId. 
            CalculateBonusDto calculateBonusDto = new CalculateBonusDto();
            calculateBonusDto.TotalBonusPoolAmount = 50000;
            calculateBonusDto.SelectedEmployeeId = 1;

            // The endpoint of Calculate BonusPool post method.
            var httpResponse = await _client.PostAsync("/api/BonusPool", JsonContent.Create(calculateBonusDto));

            // Verify api call success.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var bonusPoolCalculatorResultDto =  JsonConvert.DeserializeObject<BonusPoolCalculatorResultDto>(stringResponse);
            Assert.Equal("John Smith", bonusPoolCalculatorResultDto.Employee.Fullname);
            Assert.Equal("Accountant (Senior)", bonusPoolCalculatorResultDto.Employee.JobTitle);
            Assert.Equal(60000, bonusPoolCalculatorResultDto.Employee.Salary);
            Assert.Equal("Finance", bonusPoolCalculatorResultDto.Employee.Department.Title);
            Assert.Equal("The finance department for the company", bonusPoolCalculatorResultDto.Employee.Department.Description);
            Assert.Equal(4581, bonusPoolCalculatorResultDto.Amount);
        }
               
        [Fact]
        public async Task CalculateBonus_When_InvalidEmployeeId_ReturnsBadRequest()
        {
            // Initialize input data with Invalid EmployeeId.
            CalculateBonusDto calculateBonusDto = new CalculateBonusDto();
            calculateBonusDto.TotalBonusPoolAmount = 50000;
            calculateBonusDto.SelectedEmployeeId = 100;

            // The endpoint of Calculate BonusPool post method.
            var httpResponse = await _client.PostAsync("/api/BonusPool", JsonContent.Create(calculateBonusDto));

            // Verify api call to Bad Request.
            Assert.Equal(HttpStatusCode.BadRequest.ToString(), httpResponse.StatusCode.ToString());
            Assert.Equal("Provided employee Id does not belong to this company", httpResponse.Content.ReadAsStringAsync().Result);
        } 

    }
}
