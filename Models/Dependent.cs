using System.ComponentModel.DataAnnotations;

namespace BenifitCostDemo.Models
{
    public class Dependent
    {
        public int Id { get; set; }
        [Required]
        public string Dep_FirstName { get; set; }
        [Required]
        public string Dep_LastName { get; set; }
        [Required]
        public string Relationship { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

    }
}