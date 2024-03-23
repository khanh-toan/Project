using clients.Models;
using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace clients.Pages.Admins.Employees
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public AddEmployeeResponse Employee { get; set; }
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
                var relativeUrl = "/api/Employee";
                var response = await client.PostReturnResponse(relativeUrl, Employee);
                if (response.IsSuccessStatusCode)
                {
                    // Nếu thành công, hiển thị thông báo thành công
                    TempData["SuccessMessage"] = "Nhân viên đã được thêm mới thành công!";
                    return RedirectToPage("/Admins/Employees/Index");
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
