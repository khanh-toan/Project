using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Admins.Employees
{
    public class DeactiveModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var client = new ClientService(HttpContext);
            var relativeUrl = $"/api/Employee/Deactivate/{Id}";
            var response = await client.Deactivate(relativeUrl);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admins/Employees/Index");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = errorMessage;
                return Page();
            }
        }
    }
}
