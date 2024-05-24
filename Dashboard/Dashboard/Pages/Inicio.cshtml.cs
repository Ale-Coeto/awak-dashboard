using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dashboard.Pages
{
	public class InicioModel : PageModel
    {
        public string Nombre { set; get; } = "";

        public IActionResult OnGet()
        {
            
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return Page();
                
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Nombre")) == false)
            {
                Nombre = HttpContext.Session.GetString("Nombre");
            }
            return Redirect("/login");
        }
    }
}
