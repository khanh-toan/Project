using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace clients.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public LoginModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string email, string password)
        {
            var client = _clientFactory.CreateClient();
            var loginData = new
            {
                Email = email,
                Password = password
            };
            var response = await client.PostAsJsonAsync("https://localhost:7130/api/Authentication/Login", loginData);

            if (response.IsSuccessStatusCode)
            {
                // Đọc token từ phản hồi API
                var token = await response.Content.ReadAsStringAsync();

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                // Trích xuất vai trò từ token
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

                // Kiểm tra vai trò
                if (roleClaim == "Admin")
                {
                    // Lưu token vào cookie
                    Response.Cookies.Append("AuthToken", token, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddHours(1),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    });
                    return RedirectToPage("/Admin/ManageLevel");
                }
                else
                {
                    return RedirectToPage("/NormalUser/Index");
                }
            }
            else
            {
                // Xử lý lỗi đăng nhập
                return Page();
            }
        }
    }
}
