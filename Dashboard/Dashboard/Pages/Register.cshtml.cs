using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dashboard.Pages
{
	public class RegisterModel : PageModel
	{
		[BindProperty]
		[Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
		public string Nombre { get; set; } = "";

		[BindProperty]
		[Required(ErrorMessage = "El campo 'Correo' es obligatorio.")]
		[EmailAddress(ErrorMessage = "El formato del correo electrónico no es v�lido.")]
		public string Correo { get; set; } = "";

        [BindProperty]
		[Required(ErrorMessage = "El campo 'Contraseña' es obligatorio.")]
		public string Contrasenia { get; set; } = "";



        public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

            
            int id = DatabaseManager.InsertUser(Nombre, Correo, Contrasenia);
            HttpContext.Session.SetString("ID", id.ToString());
            HttpContext.Session.SetString("Correo", Correo);
            HttpContext.Session.SetString("Nombre", Nombre);
            //ViewData["Nombre"] = Nombre;
            return RedirectToPage("./Inicio");
		}
	}
}