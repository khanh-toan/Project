using clients.Models;
using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Admins.Attendance
{
    public class IndexModel : PageModel
    {
        public List<AdminAttendance> admins { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var client = new ClientService(HttpContext);
                var relativeUrl = "/api/Attendance";
                var response = await client.GetAll<List<AdminAttendance>>(relativeUrl);
                admins = response ?? new List<AdminAttendance>();
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
