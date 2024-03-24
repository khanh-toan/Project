using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace clients.Pages.Admins.Attendances
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var client = new ClientService(HttpContext);
            var apiUrl = $"api/Attendance/{Id}";
            var res = await client.Delete(apiUrl);
            return RedirectToPage("/Admins/Attendances/Index");
        }
    }
}
