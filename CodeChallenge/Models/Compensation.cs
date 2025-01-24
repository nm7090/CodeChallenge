

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public String ID {get; set;}
        public Employee employee {get; set;}
        public int salary {get; set;}
        public String effectiveDate {get; set;}
    }
}