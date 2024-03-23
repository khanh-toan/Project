using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Admins.Employees
{
    public class ActiveModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var client = new ClientService(HttpContext);
            var relativeUrl = $"/api/Employee/Active/{Id}";
            var response = await client.Deactivate(relativeUrl);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admins/Employees/Index");
            }
            return Page();
        }
    }
}
