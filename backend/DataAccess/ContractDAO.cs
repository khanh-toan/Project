using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ContractDAO
    {
        public static bool CheckEmployeeHaveAnyContract(int id)
        {
            var context = new MyDbContext();
            var check = context.Contracts.Where(x => x.EmployeeId == id).FirstOrDefault();
            return check != null;
        }
    }
}
