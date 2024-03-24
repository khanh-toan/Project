using clients.Models;
using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Admins.Contracts
{
    public class IndexModel : PageModel
    {
        public List<ContractResponse> Contracts { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var client = new ClientService(HttpContext);
                var relativeUrl = "/api/Contract";
                var response = await client.GetAll<List<ContractResponse>>(relativeUrl);
                Contracts = response ?? new List<ContractResponse>();
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
