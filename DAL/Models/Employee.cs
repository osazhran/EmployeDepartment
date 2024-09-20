using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employee:ModuleBase
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "the max length of name is 50 char")]
        [MinLength(5, ErrorMessage = "the min length of name is 5 char")]
        public string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
        , ErrorMessage = "the addres must be like 123-street-city-country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        [Display(Name = "Email Address")]

        public string EmailAddress { get; set; }
        [Phone]
        [Display(Name = "Phone Numver")]

        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]

        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int? DepartmentId { get; set; } // foriegn key column

        // Navigation properity => one
        //[InverseProperty(nameof(Models.Department.Employees))]
        public  Department? Department { get; set; }

    }
}
