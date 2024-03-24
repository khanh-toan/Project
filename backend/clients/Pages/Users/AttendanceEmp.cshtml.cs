using BusinessObject;
using clients.Models;
using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Users
{
    public class AttendanceEmpModel : PageModel
    {
        public int IdEmployee { get; set; }
        public List<AttendanceEmployee> attendances { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                IdEmployee = int.Parse(id);
                var client = new ClientService(HttpContext);
                var relativeUrl = $"/api/Attendance/AttendanceEmployee/{IdEmployee}";
                var response = await client.GetAll<List<AttendanceEmployee>>(relativeUrl);

                if (response != null)
                {
                    attendances = response;
                    return Page();
                }
                else
                {
                    attendances = new List<AttendanceEmployee>();
                    TempData["ErrorMessage"] = "Employee needs attendance today.";
                    return Page();
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
