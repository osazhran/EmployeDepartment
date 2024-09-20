using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public  class Department:ModuleBase
    {
        [Required(ErrorMessage = "Name is Requried")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code is required!!")]
        public string Code { get; set; }
        [Display(Name = "Date of creation")]
        public DateTime DateOfCreation { get; set; }

        //[InverseProperty(nameof(Employee.Department))]
        public  ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
