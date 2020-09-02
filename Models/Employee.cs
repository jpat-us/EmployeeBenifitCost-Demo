using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BenifitCostDemo.Models
{
    public class Employee
    {
        public Employee()
        {
            Dependent = new HashSet<Dependent>();
        }

        public int EmployeeId { get; set; }

        [Column(TypeName = "varchar(150)")]
        [DisplayName("Full Name")]
        public string FullName { get; set; }   
        [Column(TypeName= "varchar(20)")]
        public string Gender { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Department { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Company { get; set; }



        public virtual ICollection<Dependent> Dependent { get; set; }

    }
}
