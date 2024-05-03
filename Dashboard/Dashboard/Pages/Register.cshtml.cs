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
		[EmailAddress(ErrorMessage = "El formato del correo electr�nico no es v�lido.")]
		public string Correo { get; set; } = "";

        [BindProperty]
		[Required(ErrorMessage = "El campo 'Contrase�a' es obligatorio.")]
		public string Contrasenia { get; set; } = "";



        public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			DatabaseManager.InsertUser(Nombre, Correo, Contrasenia);

			return RedirectToPage("./Index");
		}
	}
}