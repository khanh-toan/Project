using Microsoft.AspNetCore.Mvc;
using Repositories.Impl;
using Repositories;
using Microsoft.AspNetCore.Authorization;
using BusinessObject;
using DataAccess;
using BusinessObject.Enum;
using Repositories.Helper;
using DataTransfer.Request;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository = new EmployeeRepository();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(employeeRepository.GetAll());
        }

        [HttpGet("{key}")]
        public IActionResult Get(int key)
        {
            var check = employeeRepository.GetEmployeeById(key);
            return check == null ? NotFound() : Ok(check);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeReq employee)
        {
            var checkGenderIsDefined = !Enum.IsDefined(typeof(EnumList.Gender), employee.Gender);
            if (checkGenderIsDefined) return BadRequest("Role or Gender is not defined");

            var checkAgeOfEmployeeLessThan18 = UserHelper.CheckAgeLessThan18(employee.Dob);
            if (checkAgeOfEmployeeLessThan18) return BadRequest("Employee can't less than 18 years old");

            var checkCCCDAlreadyExist = employeeRepository.CheckCCCDIsExist(employee.CCCD);
            if (checkCCCDAlreadyExist) return BadRequest("CCCD already exist");

            var check = employeeRepository.CreateUser(employee);
            return check == "success" ? Ok() : BadRequest("Email already exist");
        }

        [Authorize(Roles = "Employee")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EmployeeUpdateDTO employee)
        {
            var check = employeeRepository.UpdateUser(id, employee);
            return check ? Ok() : BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var checkEmp = employeeRepository.GetEmployeeById(id);
            if (checkEmp == null)
            {
                return NotFound();
            }
            var check = employeeRepository.DeleteUser(id);

            return check ? Ok() : BadRequest("This employee has some contracts");
        }
    }
}
