using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using Dashboard.Models;
using Microsoft.AspNetCore.Identity;
using Dashboard;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dashboard.Pages
{
    public class FeedbackModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Nombre { set; get; }

        [BindProperty]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Correo { set; get; }

        [BindProperty]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Sugerencias { set; get; }

        [BindProperty]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Remover { set; get; }

        [BindProperty]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Preguntas { set; get; }

        public bool IsUser { set; get; } = true;
        public string Id { set; get; } = "";

        Feedback feedback = new Feedback();

        /*/ public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Hola");
                DatabaseManager.InsertFeedback(1, Nombre, Correo, Sugerencias, Remover, Preguntas);

                Nombre = string.Empty;
                Correo = string.Empty;
                Sugerencias = string.Empty;
                Remover = string.Empty;
                Preguntas = string.Empty;

                TempData["SuccessMessage"] = "¡Gracias por tu feedback!";
                return RedirectToPage();
            }
            else
            {
                Console.WriteLine("Ehhh");
            }
            return Page();
        }
        /*/

        public void OnPost()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ID")) == false)
            {
                Id = HttpContext.Session.GetString("ID");
            }

            Nombre = HttpContext.Session.GetString("Nombre");
            Correo = HttpContext.Session.GetString("Correo");

            feedback.Sugerencias = Sugerencias;
            feedback.Remover = Remover;
            feedback.Preguntas = Preguntas;

            DatabaseManager.InsertFeedback(Convert.ToInt32(Id), Nombre, Correo, Sugerencias, Remover, Preguntas);
            RedirectToPage("./Perfil");
        }
    }
}
