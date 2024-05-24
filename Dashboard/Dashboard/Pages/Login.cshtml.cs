using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Org.BouncyCastle.Asn1;


namespace Dashboard.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "El campo \'Correo\' es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Correo { get; set; } = "";

        [BindProperty]
        public string? Nombre { get; set; } = "";

        //[BindProperty]
        //[Required(ErrorMessage = "El campo 'Contraseña' es obligatorio.")]
        //public string Contrasenia { get; set; } = "";

        public int ID_Usuario { get; set; }


        // Code to bypass OAUTH (for testing without tenant classes)
        public IActionResult OnPost()
        {
           if (!ModelState.IsValid)
           {
               return Page();
           }

           ID_Usuario = DatabaseManager.GetUserIDByMail(Correo);

           
           if (ID_Usuario == -1)
           {
               DatabaseManager.InsertUser(Nombre ?? Correo, Correo, "TEMP");
               ID_Usuario = DatabaseManager.GetUserIDByMail(Correo);
           }

           Usuario user = DatabaseManager.GetUsuario(ID_Usuario.ToString());

           bool success = SetSessionData(user);

           if (success)
           {
               return RedirectToPage("./Inicio");
           }

           return Page();
        }

        public IActionResult OnGet()
        {
            
           Console.WriteLine(HttpContext.Session.GetString("Correo"));
           Console.WriteLine(HttpContext.Session.GetString("ID"));
           Console.WriteLine(HttpContext.Session.GetString("Nombre"));

           if (HttpContext.Session.GetString("Correo") != null)
           {
               return RedirectToPage("./Inicio");
           }

           if (User.Identity?.IsAuthenticated == true)
           {
                // Iterate through the claims obtained
                // foreach (var claim in User.Claims)
                // {

                //     Console.WriteLine($"{claim.Type}: {claim.Value}");
                // }

                // Extract user email
                var preferredUsernameClaim = User.Claims.FirstOrDefault(c => c.Type == "preferred_username");
                string nameClaim = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
                string Correo = preferredUsernameClaim.Value;

               ID_Usuario = DatabaseManager.GetUserIDByMail(Correo);

               if (ID_Usuario == -1)
               {
                   DatabaseManager.InsertUser(nameClaim ?? Correo, Correo, "OAUTH");
                   ID_Usuario = DatabaseManager.GetUserIDByMail(Correo);
               }

               Usuario user = DatabaseManager.GetUsuario(ID_Usuario.ToString());

               bool success = SetSessionData(user);
               if (success)
               {
                   return RedirectToPage("./Inicio");
               }
           }

           return Page();
        }

        private bool SetSessionData(Usuario? user)
        {
           if (user == null)
               return false;

           if (user.ID_usuario != -1)
           {
               HttpContext.Session.SetString("ID", user.ID_usuario.ToString());
               HttpContext.Session.SetString("Correo", user.Correo);
               HttpContext.Session.SetString("Nombre", user.Nombre);
               return true;
           }
           return false;
        }

    }
}