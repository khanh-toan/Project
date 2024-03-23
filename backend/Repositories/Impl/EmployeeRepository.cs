using BusinessObject;
using BusinessObject.Enum;
using DataAccess;
using DataTransfer.Request;
using Repositories.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl
{
    public class EmployeeRepository : IEmployeeRepository
    {

        public bool CheckCCCDIsExist(string cccd) => EmployeeDAO.FindEmployeeByCCCD(cccd);

        public string DeactivateEmployee(int id)
        {
            var checkHasCurrentContract = ContractDAO.checkEmployeeHasAnyActiveContract(id);
            if (checkHasCurrentContract != null)
            {
                return "Should end contract first, then can deactivate this user";
            }
            var employee = EmployeeDAO.FindEmployeeById(id);

            employee.IsFirstLogin = true;
            employee.Password = UserHelper.GeneratedEmployeeCode(employee.EmployeeName.ToLower(), employee.Dob);
            employee.Status = EnumList.EmployeeStatus.Deactive;
            EmployeeDAO.UpdateEmployee(employee);
            return "1";
        }

        public void ActiveEmployee(int id)
        {
            var employee = EmployeeDAO.FindEmployeeById(id);
            employee.Status = EnumList.EmployeeStatus.Active; EmployeeDAO.UpdateEmployee(employee);
        }

        public bool DeleteUser(int id)
        {
            var employee = EmployeeDAO.FindEmployeeById(id);
            if (employee == null)
            {
                return false;
            }
            var checkHasAnyContract = ContractDAO.CheckEmployeeHaveAnyContract(id);
            if (checkHasAnyContract)
            {
                return false;
            }
            EmployeeDAO.DeleteEmployee(employee);
            return true;
        }

        public List<Employee> GetAll() => EmployeeDAO.GetAllEmployeeInCompany();

        public Employee GetEmployeeById(int id) => EmployeeDAO.FindEmployeeById(id);


        public string CreateUser(EmployeeReq employee)
        {
            var count = EmployeeDAO.CountEmployeeInCompany();
            var employeeCode = UserHelper.GeneratedEmployeeCode(count);
            var employeeFullName = employee.LastName + " " + employee.FirstName;
            var password = UserHelper.GeneratedEmployeeCode(employeeFullName, employee.Dob);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var employeeEmail = UserHelper.GeneratedEmployeeEmail(employeeFullName.ToLower());
            Employee e = new Employee()
            {
                EmployeeCode = employeeCode,
                IsFirstLogin = true,
                CreatedDate = DateTime.Now,
                EmployeeName = employeeFullName,
                Password = passwordHash,
                Email = employeeEmail,
                Role = EnumList.Role.Employee,
                Status = EnumList.EmployeeStatus.Active,
                Address = employee.Address,
                CCCD = employee.CCCD,
                Dob = employee.Dob,
                Phone = employee.Phone,
                Gender = employee.Gender
            };
            EmployeeDAO.CreateEmployee(e);
            return "success";
        }

        public bool UpdateUser(int id, EmployeeUpdateDTO employee)
        {
            var e = EmployeeDAO.FindEmployeeById(id);
            if (e == null)
            {
                return false;
            }
            e.EmployeeName = employee.EmployeeName;
            e.Gender = employee.Gender;
            e.Phone = employee.Phone;
            e.Dob = employee.Dob;
            e.CCCD = employee.CCCD;
            e.Address = employee.Address;
            EmployeeDAO.UpdateEmployee(e);
            return true;
        }
    }
}
