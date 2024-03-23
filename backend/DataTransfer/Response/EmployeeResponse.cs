using BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataTransfer.Response
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeCode { get; set; }

        public string EmployeeType { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public EnumList.Role Role { get; set; } 
        [Required]
        public DateTime CreatedDate { get; set; }           
        [Required]
        public bool IsFirstLogin { get; set; }

        public string jobTitle { get; set; }
    }

    public class UpdateEmployeeResponse
    {
        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public EnumList.Gender Gender { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required, MinLength(10), MaxLength(12)]
        public string CCCD { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public EnumList.EmployeeType EmployeeType { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
