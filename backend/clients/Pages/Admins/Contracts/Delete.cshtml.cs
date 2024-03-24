using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Admins.Contracts
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var client = new ClientService(HttpContext);
            var apiUrl = $"api/Contract/{Id}";
            var res = await client.Delete(apiUrl);
            return RedirectToPage("/Admins/Contracts/Index");
        }
    }
}
