using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Request
{
    public class AttendanceReq
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Range(0, 8)]
        public double Hour { get; set; }
        [Required]
        [Range(1, 8)]
        public double OTHour { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public int EmployeeId { get; set; }
    }

    public class AttendanceEmployeeReq
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Range(0, 8)]
        public double Hour { get; set; }
        [Required]
        [Range(1, 8)]
        public double OTHour { get; set; }
        [Required]
        public int Type { get; set; }
    }
}
