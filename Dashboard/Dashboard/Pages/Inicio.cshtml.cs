using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc; // Add this using directive

namespace Dashboard.Pages
{
	public class InicioModel : PageModel
    {
        public string Nombre { set; get; } = "";

        

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Nombre")))
            {
                return RedirectToPage("./Login");
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Nombre")) == false)
            {
                Nombre = HttpContext.Session.GetString("Nombre");
            }
            return Page();
        }
    }
}
