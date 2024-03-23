using clients.Models;
using clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace clients.Pages.Admins.Employees
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public UpdateEmployeeResponse Employee { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }
            var client = new ClientService(HttpContext);
            var apiUrl = $"api/Employee/{Id}";
            Employee = await client.GetDetail<UpdateEmployeeResponse>(apiUrl, null);    

            if (Employee == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()  
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = new ClientService(HttpContext);
            var apiUrl = $"api/Employee/{Id}";
            var response = await client.Put(apiUrl, Employee);

            if (response != null)   
            {
                return RedirectToPage("/Admins/Employees/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi cập nhật thông tin nhân viên.");
                return Page();
            }
        }
    }
}
