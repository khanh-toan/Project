using BCrypt.Net;
using BusinessObject;
using BusinessObject.Enum;
using DataAccess;
using DataTransfer.Request;
using DataTransfer.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        public string Login(LoginDTO login)
        {
            var employee = EmployeeDAO.Login(login.Email, login.Password);
            if(employee == null) return "false";
            if(employee.Status != EnumList.EmployeeStatus.Active) return "false";
            string token = CreateToken(employee);
            return token;
        }

        public string CreateToken(Employee e)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            EnumList.Role role = e.Role;
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role,role.ToString()),
                new Claim("EmployeeId",e.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Token").Value!));
            var cread = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cread
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public bool ChangePassword(int empId, ChangePasswordReq req)
        {
            var user = EmployeeDAO.FindEmployeeById(empId);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(req.Password);
            if (!req.Password.Equals(req.ConfirmPassword))
                return false;
            user.Password = passwordHash;
            EmployeeDAO.UpdateEmployee(user);
            return true;
        }

        public ProfileEmployeeResponse GetProfile(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            var idEmployee = claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;
            var employeeLogged = EmployeeDAO.FindEmployeeById(int.Parse(idEmployee));
            ProfileEmployeeResponse p = new ProfileEmployeeResponse
            {
                Id = employeeLogged.Id,
                EmployeeName = employeeLogged.EmployeeName,
                EmployeeCode = employeeLogged.EmployeeCode,
                Email = employeeLogged.Email,
                Role = employeeLogged.Role,
                IsFirstLogin = employeeLogged.IsFirstLogin
            };
            return p;
        }
    }
}
