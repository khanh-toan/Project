using BusinessObject.Enum;
using DataTransfer.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Impl;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContractController : ControllerBase
    {
        private IContractRepository _contractRepository = new ContractRepository();
        private IEmployeeRepository employeeRepository = new EmployeeRepository();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contractRepository.GetContracts());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var check = _contractRepository.GetContract(id);
            return check == null ? NotFound() : Ok(check);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult Create([FromBody] ContractReq contractReq)
        {
            var checkEmployeeTypeIsDefined = !Enum.IsDefined(typeof(EnumList.EmployeeType), contractReq.EmployeeType);
            var checkSalaryTypeIsDefined = !Enum.IsDefined(typeof(EnumList.SalaryType), contractReq.SalaryType);
            if (checkEmployeeTypeIsDefined || checkSalaryTypeIsDefined) return BadRequest("Employee Type or Salary Type is not defined");
            var contract = _contractRepository.CreateContract(contractReq);
            return contract == null ? BadRequest("Loi khong the add") : Ok(contract);
        }


    }
}
