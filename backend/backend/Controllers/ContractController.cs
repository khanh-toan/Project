using BusinessObject.Enum;
using DataTransfer.Request;
using DataTransfer.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;
using Repositories.Helper;
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
            var check = _contractRepository.GetContracts();
            List<ContractResponse> contracts = new List<ContractResponse>();
            foreach (var item in check)
            {
                ContractResponse response = new ContractResponse
                {
                    Id = item.Id,
                    EmployeeId = item.EmployeeId,
                    EndDate = item.EndDate,
                    StartDate = item.StartDate,
                    Employee = item.User.EmployeeCode + " - " + item.User.EmployeeName,
                    EmployeeType = UserHelper.GetEmployeeType(item.EmployeeType),
                    Status = UserHelper.GetContractStatusString(item.Status)
                };
                contracts.Add(response);
            }

            return Ok(contracts);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var checkContract = _contractRepository.GetContract(id);
            if (checkContract == null)
                return BadRequest("Not Found");
            var check = _contractRepository.DeleteContract(checkContract);
            return check ? Ok() : BadRequest("Contract with different status watting cannot be deleted");
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Deactivate/{id}")]
        public IActionResult Deactivate(int id)
        {
            var checkContract = _contractRepository.GetContract(id);
            if (checkContract == null)
                return NotFound("This contract not exist");
            if (checkContract.Status != EnumList.ContractStatus.Active)
                return BadRequest("Contract only has status Active can be Deactivate");
            _contractRepository.DeactivateContract(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Active/{id}")]
        public IActionResult Activate(int id)
        {
            var checkContract = _contractRepository.GetContract(id);
            if (checkContract == null)
                return NotFound("This contract not exist");
            if (checkContract.Status != EnumList.ContractStatus.Waiting)
                return BadRequest("Contract only has status Waiting can be Active");
            var checkEmployee = employeeRepository.GetEmployeeById(checkContract.EmployeeId);
            if (checkEmployee.Status == EnumList.EmployeeStatus.Deactive)
                return BadRequest("This user is deactive, need to active this user first");
            var checkSuccess = _contractRepository.ActiveContract(id);
            return checkSuccess ? Ok() : BadRequest("This user already has active contract, stop the previous first then perform this action");
        }
    }
}
