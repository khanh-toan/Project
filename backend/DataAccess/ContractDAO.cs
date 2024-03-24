using BusinessObject;
using BusinessObject.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ContractDAO
    {
        public static object checkEmployeeHasAnyActiveContract(int id)
        {
            var context = new MyDbContext();
            return context.Contracts
                .Where(x => x.EmployeeId == id && x.Status == EnumList.ContractStatus.Active)
                .Include(x => x.User).FirstOrDefault();
        }

        public static bool CheckEmployeeHaveAnyContract(int id)
        {
            var context = new MyDbContext();
            var check = context.Contracts.Where(x => x.EmployeeId == id).FirstOrDefault();
            return check != null;
        }

        public static void CreateContract(Contract contract)
        {
            var context = new MyDbContext();
            context.Contracts.Add(contract);
            context.SaveChanges();
        }

        public static void DeleteContract(Contract contract)
        {
            var context = new MyDbContext();
            context.Contracts.Remove(contract);
            context.SaveChanges();
        }

        public static Contract FindContractById(int id)
        {
            var context = new MyDbContext();
            return context.Contracts
                .Include(s => s.User)
                .Include(s => s.Level)
                .Include(s => s.Position)
                .SingleOrDefault(x => x.Id == id);
        }

        public static List<Contract> GetAll()
        {
            var context = new MyDbContext();
            return context.Contracts
                .Include(s => s.User)
                .Include(s => s.Level)
                .Include(s => s.Position)
                .ToList();
        }

        public static List<Contract> GetContractsByEmpId(int empId)
        {
            var context = new MyDbContext();
            return context.Contracts.Where(x => x.EmployeeId == empId).ToList();
        }

        public static void UpdateContract(Contract contract)
        {
            var context = new MyDbContext();
            context.Entry(contract).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
