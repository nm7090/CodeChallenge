

using System;
using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories 
{
    public interface ICompensationRepository 
    {
        Compensation GetByEmployeeId(String id);
        Compensation AddCompensation(Compensation compensation);
        Task SaveAsync();
    }
}