using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BenifitCostDemo.Models
{
    public class EmployeeBenifitCost
    { 
        [Key]
        public int EmployeeId { get; set; }

        [DisplayName("Employee Name")]
        public string  FullName { get; set; }

        public string Department { get; set; }
        [DisplayName("Depenents Count")]
        public Nullable<int> DependentsCount { get; set; }
        public decimal BenifitCost { get; set; }
    }
}
