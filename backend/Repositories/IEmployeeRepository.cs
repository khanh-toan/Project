using BusinessObject;
using DataTransfer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        public Employee GetEmployeeById(int id);
        public List<Employee> GetAll();
        public string CreateUser(EmployeeReq employee);
        public bool UpdateUser(int id, EmployeeUpdateDTO employee);
        public bool DeleteUser(int id);
        public string DeactivateEmployee(int id);
        public void ActiveEmployee(int id);
        public bool CheckCCCDIsExist(string cccd);
    }
}
