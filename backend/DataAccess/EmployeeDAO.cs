using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeDAO
    {
        public static Employee Login(string email, string password)
        {
            var context = new MyDbContext();
            var emp = context.Users.FirstOrDefault(e => e.Email.Equals(email));
            if (emp == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, emp.Password)) return null;
            return emp;
        }

        public static Employee FindEmployeeById(int id)
        {
            var context = new MyDbContext();
            var emp = context.Users
                .Include(u => u.Contracts)
                .ThenInclude(c => c.Position)
                .Include(u => u.Contracts)
                .ThenInclude(c => c.Level)
                .Include(u => u.Attendances)
                .Include(u => u.Contracts)
                .ThenInclude(c => c.PayRolls)
                .FirstOrDefault(e => e.Id == id);
            return emp;
        }

        public static void UpdateEmployee(Employee emp)
        {
            var context = new MyDbContext();
            context.Entry(emp).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
