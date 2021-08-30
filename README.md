Assessment has been completed with following code changes: 

1. SynetecAssessmentApi project
	a. Created interface ICalculateBonus to calculate employee bonus and implemented in BonusPoolService class. 
	b. Created interface IEmployee which contains method related to Employee operation and implemented in EmployeeService class. 
	c. Refactor code in BonusPoolController, prepare and return of Dto class from  controller intead of service class. 
	d. Added APIHealthCheck class in HealthCheck folder to check Health of Api after deploying on production. 
	e. Changes in StartUp.cs class inject service classes and define end point for HealthCheck (/health). 
2. Added new SynetecAssessmentApi.Test xUnit project
	a. Implemented Integration test in IntegrationTest folder
	b. Implemented Unit test in UnitTest folder 
