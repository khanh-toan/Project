using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace clients.Pages.Users
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var client = new ClientService(HttpContext);

            var tokenCookie = HttpContext.Request.Cookies["AccessToken"];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokenCookie);

            // Lấy EmployeeId từ claims trong token
            var idEmployee = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;

            var apiUrl = $"api/Attendance/{Id}";
            var res = await client.Delete(apiUrl);
            return RedirectToPage("/Users/AttendanceEmp", new { id = idEmployee });
        }
    }
}
