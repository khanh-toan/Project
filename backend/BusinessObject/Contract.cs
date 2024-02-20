using BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Contract
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public EnumList.EmployeeType EmployeeType { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal BaseSalary { get; set; }
        [Required]
        public int DateOffPerYear { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public double OTSalaryRate { get; set; }
        [Required]
        public double InsuranceRate { get; set; }
        [Required]
        public double TaxRate { get; set; }
        [Required]
        public EnumList.SalaryType SalaryType { get; set; }
        [Required]
        public int PositionId { get; set; }
        [Required]
        public EnumList.ContractStatus Status { get; set; }

        public virtual Employee User { get; set; }
        public virtual Level Level { get; set; }
        public virtual Position Position { get; set; }
        [JsonIgnore]
        public virtual ICollection<PayRoll> PayRolls { get; set; }
    }
}
