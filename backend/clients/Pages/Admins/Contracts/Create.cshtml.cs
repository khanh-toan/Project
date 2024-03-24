using clients.Models;
using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace clients.Pages.Admins.Contracts
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public AddContractResponse Contracts { get; set; }

        public List<EmployeeResponse> employees { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var client = new ClientService(HttpContext);
                var relativeUrl = "/api/Employee";
                var response = await client.GetAll<List<EmployeeResponse>>(relativeUrl);
                employees = response ?? new List<EmployeeResponse>();
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
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
                var relativeUrl = "/api/Contract";
                var response = await client.PostReturnResponse(relativeUrl, Contracts);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Admins/Contracts/Index");
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
