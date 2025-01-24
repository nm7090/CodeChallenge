using System;
using System.Net;
using System.Net.Http;
using System.Text;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration 
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        [Priority(1)]
        public void CreateCompensation_Returns_Created()
        {
            //Arrange
            var compedEmployee = new Employee()
            {
                EmployeeId = "001",
                Department = "Complaints",
                FirstName = "Debbie",
                LastName = "Downer",
                Position = "Receiver",
            };

            var compensation = new Compensation()
            {
                employee = compedEmployee,
                salary = 20000,
                effectiveDate = "09/10/2024"
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            //Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(newCompensation.employee.EmployeeId, compedEmployee.EmployeeId);
            Assert.AreEqual(newCompensation.salary, compensation.salary);
            Assert.AreEqual(newCompensation.effectiveDate, compensation.effectiveDate);
        }

        [TestMethod]
        [Priority(2)]
        public void GetCompensationByEmployeeId_Returns_Ok()
        {
            //Arrange
            var employeeId = "001";
            var expectedSalary = 20000;
            var expectedDate = "09/10/2024";
            var expectedEmployeeFirstName = "Debbie";

            //Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(compensation.salary, expectedSalary);
            Assert.AreEqual(compensation.effectiveDate, expectedDate);
            Assert.AreEqual(compensation.employee.FirstName, expectedEmployeeFirstName);
            
        }

        [TestMethod]
        [Priority(3)]
        public void GetCompensationByEmployeeId_Returns_Not_Found()
        {
            //Arrange
            var employeeId = "002";

            //Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            
        }
    }
}