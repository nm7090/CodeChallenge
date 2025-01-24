

using System;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository: ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }

        public Compensation AddCompensation(Compensation compensation)
        {
            compensation.ID = Guid.NewGuid().ToString();
            _compensationContext.Add(compensation);
            return compensation;
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }

        public Compensation GetByEmployeeId(String id)
        {
            return _compensationContext.Compensations.Include(c => c.employee.DirectReports).SingleOrDefault(e => e.employee.EmployeeId == id);
        }
    }
}