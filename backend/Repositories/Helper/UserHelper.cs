using DataAccess;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessObject.Enum.EnumList;

namespace Repositories.Helper
{
    public class UserHelper
    {
        public static string GetStatusString(EmployeeStatus status)
        {
            return status == EmployeeStatus.Active ? "Active" : "Deactive";
        }

        public static string GetEmployeeType(EmployeeType? type)
        {
            if (type == null)
            {
                return "Unknown";
            }
            return type == EmployeeType.FullTime ? "FullTime" : "PartTime";
        }

        public static bool CheckAgeLessThan18(DateTime dob)
        {
            DateTime currentDate = DateTime.Now;

            int age = currentDate.Year - dob.Year;

            if (currentDate.Month < dob.Month || (currentDate.Month == dob.Month && currentDate.Day < dob.Day))
            {
                age--;
            }
            return age < 18;
        }

        public static int GetEmployeeIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            var idEmployee = claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;
            return int.Parse(idEmployee);
        }

        public static string GeneratedEmployeeCode(int count)
        {
            count++;
            var check = count.ToString().Length;
            if (check == 1)
            {
                return "SD000" + count;
            }
            else if (check == 2)
            {
                return "SD00" + count;
            }
            else if (check == 3)
            {
                return "SD0" + count;
            }
            return "SD" + count;
        }

        public static string GeneratedEmployeeCode(string name, DateTime dob)
        {
            List<string> words = name.Split(' ').ToList();
            var lastName = words[words.Count() - 1];
            words.RemoveAt(words.Count() - 1);
            words.Insert(0, lastName);
            string password = "";
            for (int i = 0; i < words.Count(); i++)
            {
                password = password + words[i].ToString();
            }
            password = password + "@";
            password += dob.Day.ToString() + dob.Month.ToString() + dob.Year.ToString();
            password = password[0].ToString().ToUpper() + password.Substring(1);
            return password;
        }

        public static string GeneratedEmployeeEmail(string fullName)
        {
            List<string> words = fullName.Split(' ').ToList();
            var lastName = words[words.Count() - 1];
            words.RemoveAt(words.Count() - 1);
            words.Insert(0, lastName);
            string email = "";
            for (int i = 0; i < words.Count(); i++)
            {
                if (i == 0)
                {
                    email = email + words[i].ToString();

                }
                else
                {
                    email = email + words[i].Select(x => x.ToString()).ToArray()[0];
                }
            }   
            var count = EmployeeDAO.CountEmailSameName(email);
            if (count == 0)
            {
                return email + "@projectx.com";
            }

            return email + count + "@projectx.com";
        }
    }
}
