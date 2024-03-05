using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Owin;

namespace clients.Pages.Admin
{
    public class ManageLevelModel : PageModel
    {
        public RequestCookieCollection Cookies { get; set; }

        public void OnGet()
        {
           
        }
    }
}
