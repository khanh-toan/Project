using BusinessObject.Enum;
using System.ComponentModel.DataAnnotations;

namespace clients.Models
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeType { get; set; }
        public string status { get; set; }
        public EnumList.Role Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsFirstLogin { get; set; }
        public string jobTitle { get; set; }
    }

    public class AddEmployeeResponse
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public int Gender { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "CCCD is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "CCCD must contain only digits")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "CCCD must be 10 digits long")]
        public string CCCD { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } 
    }

    public class UpdateEmployeeResponse
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        [Required, MinLength(10), MaxLength(10)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "CCCD must contain only digits")]
        public string CCCD { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int EmployeeType { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
