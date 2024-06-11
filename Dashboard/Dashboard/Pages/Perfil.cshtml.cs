using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dashboard.Pages
{
	public class PerfilModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "El campo \'Nombre\' es obligatorio.")]
        public string Nombre { set; get; }


        [BindProperty]
        public string Username { set; get; } = "";


        [BindProperty]
        //[Required(ErrorMessage = "El campo \'Correo\' es obligatorio.")]
        public string Correo { set; get; } = "";

        [BindProperty]
        public string Contrasenia { set; get; } = "";

        [BindProperty]
        [Required(ErrorMessage = "El campo \'Nueva Contraseña\' es obligatorio.")]
        public string NuevaContrasenia1 { set; get; } = "";

        [BindProperty]
        [Required(ErrorMessage = "El campo \'Confirmar Contraseña\' es obligatorio.")]
        public string NuevaContrasenia2 { set; get; } = "";

        [BindProperty]
        public string Red { set; get; } = "";

        [BindProperty]
        public string Bio { set; get; } = "";

        [BindProperty]
        public DateOnly Cumpleanios { set; get; }

        public string CumpleaniosTxt { set; get; } = "";

        public bool IsUser { set; get; } = true;

        public string Id { set; get; } = "";

        public string ErrorMessage { set; get; } = "";

        public string SuccessMessage { set; get; } = "";

        Usuario user = new Usuario();

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Nombre")))
            {
                return RedirectToPage("./Login");
            }
            //string id = "";
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ID")) == false)
            {
                Id = HttpContext.Session.GetString("ID");
            }

            user = DatabaseManager.GetUsuario(Id);

            Nombre = user.Nombre;
            Username = user.Username;
            Correo = user.Correo;
            Red = user.RedSocial;
            Bio = user.Bio;

            Cumpleanios = user.Cumpleanios;
            CumpleaniosTxt = Cumpleanios.ToString("yyyy-MM-dd");

            if (CumpleaniosTxt == "0001-01-01")
            {
                CumpleaniosTxt = "";
            }
            // Cumpleanios = DateOnly.FromDateTime(DateTime.ParseExact(formattedDate, "yyyy/MM/dd", null));

            Console.WriteLine(CumpleaniosTxt);


            //IsUser = true;

            return Page();
        }

        public void OnPost()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ID")) == false)
            {
                Id = HttpContext.Session.GetString("ID");
            }

            Nombre = HttpContext.Session.GetString("Nombre");
            Correo = HttpContext.Session.GetString("Correo");
            string pass = DatabaseManager.GetPassword(Id);

            if (NuevaContrasenia1 != "")
            {
                if (Contrasenia == pass && NuevaContrasenia1 == NuevaContrasenia2) {
                    user = DatabaseManager.GetUsuario(Id);
                    Nombre = user.Nombre;
                    Username = user.Username;
                    Correo = user.Correo;
                    Red = user.RedSocial;
                    Bio = user.Bio;
                    Cumpleanios = user.Cumpleanios;
                    CumpleaniosTxt = Cumpleanios.ToString("yyyy-MM-dd");

                    if (CumpleaniosTxt == "0001-01-01")
                    {
                        CumpleaniosTxt = "";
                    }
                    DatabaseManager.UpdatePassword(Id, NuevaContrasenia1);
                    SuccessMessage = "Contraseña actualizada correctamente.";

                } else if (Contrasenia != pass) {
                    ErrorMessage = "La anterior contraseña es incorrecta.";
                } else if (NuevaContrasenia1 != NuevaContrasenia2) {
                    ErrorMessage = "Las nuevas contraseñas no coinciden.";
                }


                // RedirectToPage("./Perfil");

            } else {
                user.Username = Username;
                user.RedSocial = Red;
                user.Cumpleanios = Cumpleanios;
                CumpleaniosTxt = Cumpleanios.ToString("yyyy-MM-dd");

                if (CumpleaniosTxt == "0001-01-01")
                {
                    CumpleaniosTxt = "";
                }
                user.Bio = Bio;
                DatabaseManager.UpdateUser(Id, user);
            }

            Console.WriteLine("USERNAMEPRE : " + Username);

            

            // if (NuevaContrasenia1 != "" && Contrasenia == pass && NuevaContrasenia1 == NuevaContrasenia2)
            // {
            //     // Console.WriteLine("D" + Id + " " + NuevaContrasenia1);
            //     DatabaseManager.UpdatePassword(Id, NuevaContrasenia1);
            //     // RedirectToPage("./Perfil");

            // } else {
            //     DatabaseManager.UpdateUser(Id, user);
            // }

            
            // Console.WriteLine("test" + Nombre + user.Nombre + "ddd");

            // Console.WriteLine("USERNAME : " + Username);
            // Console.WriteLine(Id);
            
            RedirectToPage("./Perfil");
        }

        // public void OnPostContrasenia()
        // {
        //     Id = HttpContext.Session.GetString("ID");

        //     // string pass = DatabaseManager.GetPassword(Id);
        //     user = DatabaseManager.GetUsuario(Id);
        //     Nombre = user.Nombre;
        //     Username = user.Username;
        //     Correo = user.Correo;
        //     Red = user.RedSocial;
        //     Bio = user.Bio;
        //     Cumpleanios = user.Cumpleanios;
        //     string pass = user.Contrasenia;
        //     CumpleaniosTxt = Cumpleanios.ToString("yyyy-MM-dd");

        //     if (CumpleaniosTxt == "0001-01-01")
        //     {
        //         CumpleaniosTxt = "";
        //     }

        //     //if (Contrasenia != pass)
        //     //{
        //     //    RedirectToPage("./Perfil");
        //     //}

        //     //else if (NuevaContrasenia1 != NuevaContrasenia2)
        //     //{
        //     //    RedirectToPage("./Perfil");
        //     //} 
        //     if (Contrasenia == pass && NuevaContrasenia1 == NuevaContrasenia2)
        //     {
        //         // Console.WriteLine("D" + Id + " " + NuevaContrasenia1);
        //         DatabaseManager.UpdatePassword(Id, NuevaContrasenia1);
        //         RedirectToPage("./Perfil");

        //     }
        //     //RedirectToPage("/YourPage", new { user = user });
        // }
    }
}
