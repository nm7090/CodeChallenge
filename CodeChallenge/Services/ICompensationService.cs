

using System;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation GetByEmployeeId(String id);
        Compensation CreateCompensation(Compensation compensation);

    }
}