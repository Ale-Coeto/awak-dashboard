using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dashboard.Pages
{
	public class PerfilModel : PageModel
    {
        [BindProperty]
        public string Nombre { set; get; } = "";
        public bool NombreCorrecto { set; get; }


        [BindProperty]
        public string Usuario { set; get; } = "";
        public bool ApellidoCorrecto { set; get; }


        [BindProperty]
        public string Correo { set; get; } = "";
        public bool CorreoCorrecto { set; get; }

        [BindProperty]
        public string Contrasena { set; get; } = "";
        public bool ContrasenaCorrecto { set; get; }

        [BindProperty]
        public string Red { set; get; } = "";
        public bool RedCorrecto { set; get; }

        [BindProperty]
        public string Bio { set; get; } = "";
        public bool BioCorrecto { set; get; }

        public bool IsUser { set; get; } = false;

        public int Id { set; get; }

        public void OnGet()
        {
            IsUser = Request.Query.ContainsKey("admin");
            IsUser = !IsUser;
            int id = 0;

            if (Request.Query.ContainsKey("id"))
            {
                int.TryParse(Request.Query["id"], out id);
                Id = id;
            }

        }

        public void OnPostPerfil()
        {

        }

        public void OnPostContrasena()
        {

        }
    }
}
