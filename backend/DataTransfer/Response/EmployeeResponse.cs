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
}
