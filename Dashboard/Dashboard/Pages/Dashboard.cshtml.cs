using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dashboard.Pages
{

	public class DashboardModel : PageModel
    {
        public bool IsAdmin { get; set; }
        public string URL { get; set; } = "";

        public IActionResult OnGet()
        {
            int id = -1;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Rol")))
            {
                IsAdmin = HttpContext.Session.GetString("Rol") == "admin";
            }

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("ID")))
            {
                id = int.Parse(HttpContext.Session.GetString("ID")!);
            }

            if (!IsAdmin) {
                URL = "https://lookerstudio.google.com/embed/reporting/53df42ab-f54b-4c1f-b258-dbb238dcfe2c/page/dcH1D?params={%22id%22:%22" + id + "%22}";
                Console.WriteLine(URL);
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Nombre")))
            {
                return RedirectToPage("./Login");
            }
            return Page();
        }
    }
}
