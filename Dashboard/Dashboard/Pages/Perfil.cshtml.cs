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
        [Required(ErrorMessage = "El campo \'Anterior Contraseña\' es obligatorio.")]
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

        Usuario user = new Usuario();

        public void OnGet()
        {
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


        }

        public void OnPostPerfil()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ID")) == false)
            {
                Id = HttpContext.Session.GetString("ID");
            }

            Nombre = HttpContext.Session.GetString("Nombre");
            Correo = HttpContext.Session.GetString("Correo");


            user.Username = Username;
            user.RedSocial = Red;
            user.Cumpleanios = Cumpleanios;
            CumpleaniosTxt = Cumpleanios.ToString("yyyy-MM-dd");

            if (CumpleaniosTxt == "0001-01-01")
            {
                CumpleaniosTxt = "";
            }

            user.Bio = Bio;
            Console.WriteLine("test" + Nombre + user.Nombre + "ddd");

            Console.WriteLine(Username);
            Console.WriteLine(Id);
            DatabaseManager.UpdateUser(Id, user);
            RedirectToPage("./Perfil");
        }

        public void OnPostContrasenia()
        {
            Id = HttpContext.Session.GetString("ID");

            string pass = DatabaseManager.GetPassword(Id);
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

            //if (Contrasenia != pass)
            //{
            //    RedirectToPage("./Perfil");
            //}

            //else if (NuevaContrasenia1 != NuevaContrasenia2)
            //{
            //    RedirectToPage("./Perfil");
            //} 
            if (Contrasenia == pass && NuevaContrasenia1 == NuevaContrasenia2)
            {
                Console.WriteLine("D" + Id + " " + NuevaContrasenia1);
                DatabaseManager.UpdatePassword(Id, NuevaContrasenia1);
                RedirectToPage("./Perfil");

            }
            //RedirectToPage("/YourPage", new { user = user });
        }
    }
}
