using DataTransfer.Request;
using DataTransfer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAuthenticationRepository
    {
        public string Login(LoginDTO login);
        public bool ChangePassword(int empId, ChangePasswordReq req);
        public ProfileEmployeeResponse GetProfile(string token);
    }
}
