using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static List<Employee> GetAllEmployeeInCompany()
        {
            var context = new MyDbContext();
            return context.Users
                .Include(u => u.Contracts)
                .ThenInclude(c => c.Position)
                .Include(u => u.Contracts)
                .ThenInclude(c => c.Level)
                .Include(u => u.Contracts)
                .ThenInclude(c => c.PayRolls)
                .ToList();
        }

        public static int CountEmployeeInCompany()
        {
            var context = new MyDbContext();
            var countEmployee = context.Users.Count();
            string patternNumber = @"\d+";
            if (countEmployee == 0)
            {
                return countEmployee;
            }
            var codeOfEmployee = context.Users.OrderBy(x => x.Id).Last().EmployeeCode;
            Match match = Regex.Match(codeOfEmployee, patternNumber);
            var lastCodeOfEmployee = int.Parse(match.Value);
            return lastCodeOfEmployee;
        }

        public static bool FindEmployeeByCCCD(string cccd)
        {
            var context = new MyDbContext();
            var empl = context.Users.FirstOrDefault(x => x.CCCD.Equals(cccd));
            return empl != null;
        }

        public static int CountEmailSameName(string email)
        {
            var context = new MyDbContext();
            var count = context.Users.Where(x => x.Email.Contains(email)).ToList();
            string pattern = @"[A-Za-z]";
            string patternNumber = @"\d+";
            if (count.Count == 0)
            {
                return 0;
            }
            List<Employee> listCheckEmail = new List<Employee>();
            var countEmail = 0;
            foreach (var item in count)
            {
                var emailCheck = item.Email.Split("@")[0];
                var emailWithoutNumber = string.Concat(Regex.Matches(emailCheck, pattern));
                if (email.Equals(emailWithoutNumber))
                {
                    listCheckEmail.Add(item);
                    countEmail++;
                }
            }
            if (countEmail == 0 || countEmail == 1)
            {
                return countEmail;
            }
            var lastEmail = listCheckEmail.OrderBy(x => x.Id).Last().Email;
            Match match = Regex.Match(lastEmail, patternNumber);
            var numberOfThisEmail = int.Parse(match.Value);
            return ++numberOfThisEmail;
        }

        public static void CreateEmployee(Employee e)
        {
            var context = new MyDbContext();
            context.Users.Add(e);
            context.SaveChanges();
        }

        public static void DeleteEmployee(Employee employee)
        {
            var context = new MyDbContext();
            context.Users.Remove(employee);
            context.SaveChanges();
        }
    }
}
