using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public bool ValidUser { get; set; }

        public IActionResult OnPost()
        {
            

            if (!ModelState.IsValid)
            {
                return Page();
            }

            ValidUser = DatabaseManager.IsValidUser(Correo, Contrasenia);

            if (ValidUser)
                return RedirectToPage("./Index");
            else
                return Page();
        }
    }
}