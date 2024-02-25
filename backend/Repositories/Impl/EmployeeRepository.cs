using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Employee GetEmployeeById(int id) => EmployeeDAO.FindEmployeeById(id);
    }
}
