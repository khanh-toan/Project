using BusinessObject.Enum;

namespace clients.Models
{
    public class EmployeeResponse
    {
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeType { get; set; }
        public string status { get; set; }
        public EnumList.Role Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsFirstLogin { get; set; }
        public string jobTitle { get; set; }
    }
}
