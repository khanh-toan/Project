using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Admins.Contracts
{
    public class ActiveModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var client = new ClientService(HttpContext);
            var relativeUrl = $"/api/Contract/Active/{Id}";
            var response = await client.Deactivate(relativeUrl);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admins/Contracts/Index");
            }
            return Page();
        }
    }
}
