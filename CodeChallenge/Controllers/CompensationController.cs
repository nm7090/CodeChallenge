

using System;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        /// <summary>
        /// Receives a compensation in a POST request and adds it to the Context
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns>Sends back the created compensation</returns>
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation) 
        {

            _logger.LogDebug($"Received compensation create request for '{compensation.employee.EmployeeId}'");

            _compensationService.CreateCompensation(compensation);
            return CreatedAtRoute("GetCompensationByEmployeeId", new {id = compensation.employee.EmployeeId}, compensation );
        }

        /// <summary>
        /// Receives an employee id as a URL param and grabs compensation with matching employee id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Sends back the matching compensation</returns>
        [HttpGet("{id}", Name = "GetCompensationByEmployeeId")]
        public IActionResult GetCompensationById(String id) {

            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetByEmployeeId(id);

            if(compensation == null)
            {
                return NotFound();
            }

            return Ok(compensation);
        }       
    }
}