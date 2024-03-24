using clients.Models;
using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace clients.Pages.Users
{
    public class AddAttendanceModel : PageModel
    {
        [BindProperty]
        public AddAttendanceEmployee attendance { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var client = new ClientService(HttpContext);

                var tokenCookie = HttpContext.Request.Cookies["AccessToken"];
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(tokenCookie);

                // Lấy EmployeeId từ claims trong token
                var idEmployee = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;

                var relativeUrl = "/api/Attendance/AttendanceEmployee";
                var response = await client.PostReturnResponse(relativeUrl, attendance);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Users/AttendanceEmp", new { id = idEmployee });
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    // Đọc thông điệp lỗi từ phản hồi
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = errorMessage;
                }
                else
                {
                    // Nếu không thành công, hiển thị thông báo lỗi
                    TempData["ErrorMessage"] = "Đã xảy ra lỗi khi thêm mới nhân viên.";
                }
                return Page();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
