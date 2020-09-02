using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenifitCostDemo.Models
{
    public class HRMSContext : DbContext
    {
        public HRMSContext(DbContextOptions<HRMSContext> options): base(options)
        {

        }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Dependent> dependents { get; set; }
        public virtual DbSet<EmployeeBenifitCost> employeeBenifitCosts { get; set; }
    }
}
