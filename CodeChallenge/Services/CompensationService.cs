

using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        /// <summary>
        /// Takes a compensation parameter and adds it to the repository
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns>Compnesation Input</returns>
        public Compensation CreateCompensation(Compensation compensation)
        {
            if(compensation != null) {
                _compensationRepository.AddCompensation(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        /// <summary>
        /// Gets a Compensation based on the employee ID it is associated with
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null if not found or the gotten Compensation</returns>
        public Compensation GetByEmployeeId(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetByEmployeeId(id);
            }

            return null;
        }
    }
}