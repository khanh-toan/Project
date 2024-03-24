using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Admins.Attendances
{
    public class RejectModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public string status { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var client = new ClientService(HttpContext);
            var relativeUrl = $"/api/Attendance/{Id}?attendanceStatus={status}";
            var response = await client.Deactivate(relativeUrl);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admins/Attendances/Index");
            }
            return Page();
        }
    }
}
