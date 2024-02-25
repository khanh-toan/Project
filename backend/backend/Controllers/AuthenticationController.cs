using BusinessObject;
using DataAccess;
using DataTransfer.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Helper;
using Repositories.Impl;

namespace backend.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private IAuthenticationRepository authenticationRepository = new AuthenticationRepository();
        private IEmployeeRepository employeeRepository = new EmployeeRepository();


        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var check = authenticationRepository.Login(loginDTO);
            return check.Equals("false") ? BadRequest("Wrong email & password") : Ok(check);
        }

        [Authorize]
        [HttpPatch("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordReq req)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token)) return BadRequest("Invalid token");
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }
            var empId = UserHelper.GetEmployeeIdFromToken(token);
            var checkEmployeeIsFirstLogin = employeeRepository.GetEmployeeById(empId).IsFirstLogin;
            if (checkEmployeeIsFirstLogin)
                return BadRequest("You don't use this function");
            var check = authenticationRepository.ChangePassword(empId, req);
            return check ? Ok(check) : BadRequest("Confirm password not march password");
        }


    }
}
