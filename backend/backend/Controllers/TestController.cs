using Microsoft.AspNetCore.Mvc;
using Repositories.Impl;
using Repositories;
using BusinessObject;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly MyDbContext _dbContext;
        public TestController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lastUser = _dbContext.Users.OrderBy(x => x.Id).LastOrDefault();
            if (lastUser != null)
            {
                var codeOfEmployee = lastUser.EmployeeCode;
                return Ok(codeOfEmployee);
            }
            else
            {
                // Xử lý trường hợp không tìm thấy bản ghi
                return NotFound("No users found.");
            }
        }
    }
}
