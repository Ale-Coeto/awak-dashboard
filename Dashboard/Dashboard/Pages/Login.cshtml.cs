using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

//using namespace Dashboar;

namespace Dashboard.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "El campo \'Correo\' es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Correo { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "El campo 'Contraseña' es obligatorio.")]
        public string Contrasenia { get; set; } = "";
        
        public int ID_Usuario { get; set; }


        public IActionResult OnPost()
        {
            

            if (!ModelState.IsValid)
            {
                return Page();
            }

            ID_Usuario = DatabaseManager.GetUserID(Correo, Contrasenia);
            Console.WriteLine(ID_Usuario);
            Usuario user = DatabaseManager.GetUsuario(ID_Usuario.ToString());

            //ValidUser = DatabaseManager.IsValidUser(Correo, Contrasenia);

            if (ID_Usuario != -1)
            {
                HttpContext.Session.SetString("ID", ID_Usuario.ToString());
                HttpContext.Session.SetString("Correo", Correo);
                HttpContext.Session.SetString("Nombre", user.Nombre);

                //ViewData["Nombre"] = 
                return RedirectToPage("./Inicio");
            }
            else
                return Page();
        }
    }
}