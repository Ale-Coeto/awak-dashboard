using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dashboard.Pages
{
	public class AyudaModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Nombre")))
            {
                return RedirectToPage("./Login");
            }
            return Page();
        }
    }
}
