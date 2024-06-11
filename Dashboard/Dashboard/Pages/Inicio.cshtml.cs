using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Utils.Model; // Add this using directive

namespace Dashboard.Pages
{
	public class InicioModel : PageModel
    {
        public string Nombre { set; get; } = "";
        public Usuario usuario = new Usuario();

        public string Rol { set; get; } = "";

        public int Progreso { set; get; }

        public int Puntaje { set; get; } 

        

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

            string id = "";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("ID")))
            {
                id = HttpContext.Session.GetString("ID");
            }

            if (id != "")
            {
                usuario = DatabaseManager.GetUsuario(id);
            }

            Rol = usuario.Admin ? "Administrador" : "Usuario";
            int id_usuario = int.Parse(id);
            List<Progreso_Zona> progreso = DatabaseManager.GetProgress(id_usuario);
            //Progreso_dir progreso = DatabaseManager.GetProgreso2(id_usuario);
            foreach (Progreso_Zona p in progreso)
            {
                Puntaje += p.Puntaje;
                Progreso += p.JefeVencido ? 1 : 0;
            }
            Progreso_dir pnuevo = DatabaseManager.GetProgreso2(id_usuario);

            Console.WriteLine(pnuevo.puntos);
            Puntaje = pnuevo.puntos;
            Progreso = pnuevo.zonas;
            Rol = pnuevo.rol;

            //p.
            //Console.WriteLine("PROGRESO: " + progreso.Count);

            return Page();
        }
    }
}
