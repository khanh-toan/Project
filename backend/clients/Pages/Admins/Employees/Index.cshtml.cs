using BusinessObject;
using clients.Models;
using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Admins.Employees
{
    public class IndexModel : PageModel
    {
        public List<EmployeeResponse> Employees { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var client = new ClientService(HttpContext);
                var relativeUrl = "/api/Employee";
                var response = await client.GetAll<List<EmployeeResponse>>(relativeUrl);
                Employees = response ?? new List<EmployeeResponse>();
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

    }
}
