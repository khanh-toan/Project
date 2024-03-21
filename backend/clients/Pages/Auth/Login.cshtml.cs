using clients.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace clients.Pages.Auth
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string email, string password)
        {
            var clientService = new ClientService(HttpContext);
            var loginData = new
            {
                Email = email,
                Password = password
            };

            var relativeUrl = "/api/Authentication/Login";
            var response = await clientService.PostReturnResponse(relativeUrl, loginData);

            var content = await response.Content.ReadAsStringAsync();
            if (content.Equals("Not activated"))
            {
                ViewData["Title"] = "Account is locked";
                return Page();
            }   
            if (!response.IsSuccessStatusCode)
            {
                ViewData["Title"] = "Wrong Email or Password!";
                return Page();
            }

            if (response.IsSuccessStatusCode)
            {
                // Đọc token từ phản hồi API
                var token = await response.Content.ReadAsStringAsync();
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                // Trích xuất vai trò từ token
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

                // Lưu token vào cookie
                Response.Cookies.Append("AccessToken", token, new CookieOptions { HttpOnly = true, Secure = true });

                if (roleClaim == "Admin")
                {
                    return RedirectToPage("/Admins/Employees/Index");
                }
                else
                {
                    return RedirectToPage("/User/Index");
                }
            }
            else
            {
                // Xử lý lỗi đăng nhập
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }
    }
}
