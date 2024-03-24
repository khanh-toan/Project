using BusinessObject;
using BusinessObject.Enum;
using System.ComponentModel.DataAnnotations;

namespace clients.Models
{
    public class ContractResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Employee { get; set; }
        public string EmployeeType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }

    public class AddContractResponse
    {
        public int EmployeeId { get; set; }
        [Required]
        public int EmployeeType { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal BaseSalary { get; set; }
        public double? OTSalaryRate { get; set; }
        public double? InsuranceRate { get; set; }
        public double? TaxRate { get; set; }
        [Required]
        public int DateOffPerYear { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public int PositionId { get; set; }
        [Required]
        public int SalaryType { get; set; }
    }

}
