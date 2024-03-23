using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Admins.Employees
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var client = new ClientService(HttpContext);
            var apiUrl = $"api/Employee/{Id}";
            var res = await client.Delete(apiUrl);
            return RedirectToPage("/Admins/Employees/Index");
        }
    }
}
