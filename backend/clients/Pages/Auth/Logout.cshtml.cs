using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Xóa token từ cookie
            Response.Cookies.Delete("AuthToken");

            // Chuyển hướng người dùng đến trang nào đó sau khi logout thành công
            return RedirectToPage("/Auth/Login");
        }
    }
}
