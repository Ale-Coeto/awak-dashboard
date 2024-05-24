using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;


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

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                foreach (var claim in User.Claims)
                {

                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }
                var preferredUsernameClaim = User.Claims.FirstOrDefault(c => c.Type == "preferred_username");
                Console.WriteLine("preferredUsernameClaim");
                Console.WriteLine(preferredUsernameClaim.Value);

                ID_Usuario = DatabaseManager.GetUserIDByMail(Correo);

                //if (ID_Usuario != -1)
                //{
                //    DatabaseManager.InsertUser(nombre, correo, constrasenia);
                //    ID_Usuario = DatabaseManager.GetUserIDByMail(Correo);
                //}

                Console.WriteLine(ID_Usuario);
                Usuario user = DatabaseManager.GetUsuario(ID_Usuario.ToString());

                if (ID_Usuario != -1)
                {
                    HttpContext.Session.SetString("ID", ID_Usuario.ToString());
                    HttpContext.Session.SetString("Correo", Correo);
                    HttpContext.Session.SetString("Nombre", user.Nombre);

                    //ViewData["Nombre"] = 
                    return RedirectToPage("./Inicio");
                }
            }

            return Page();
        }

    }
}