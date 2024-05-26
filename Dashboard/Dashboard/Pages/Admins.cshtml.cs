using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dashboard.Pages
{
	public class AdminsModel : PageModel
    {
        [BindProperty]
        public string SearchString { get; set; } = "";

        [BindProperty]
        public string Type { get; set; } = "";

        [BindProperty]
        public Usuario selectedUser { get; set; }

        public List<Usuario> usuarios = new List<Usuario>();
        public List<Usuario> admins = new List<Usuario>();
        public List<Usuario> colaboradores = new List<Usuario>();


        public IActionResult OnGet()
        {
                
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Nombre")))
            {
                return RedirectToPage("./Login");
            }

            if (HttpContext.Session.GetString("Rol") != "superadmin")
            {
                return RedirectToPage("./Inicio");
            }

            Console.WriteLine("Admins");
            usuarios = DatabaseManager.GetUsuarios();
            Console.WriteLine("Usuarios: " + usuarios.Count);

            // Check if the search query is present
            bool search = false;
            if (Request.Query.ContainsKey("search"))
            {
                search = true;
                // usuarios = usuarios.Where(x => x.Nombre.ToLower().Contains(Request.Query["search"].ToString().ToLower())).ToList();
                SearchString = Request.Query["search"].ToString();
            }

            //Exclude id 1 (superadmin)
            // usuarios = usuarios.Where(x => x. != 1).ToList();

            // Sort users alphabetically
            // usuarios = usuarios.OrderBy(x => x.Nombre).ToList();
            selectedUser = new Usuario();
            selectedUser.ID_usuario = -1;

            // Set admins and colaboradores
            foreach (var usuario in usuarios)
            {   
                if (search && !usuario.Nombre.ToLower().Contains(SearchString.ToLower())) {
                        continue;
                }

                if (Request.Query.ContainsKey("usuario") && usuario.ID_usuario == Convert.ToInt32(Request.Query["usuario"]))
                {
                    selectedUser = usuario;
                }

                if (usuario.Admin)
                {
                    admins.Add(usuario);
                }
                else
                {
                    colaboradores.Add(usuario);
                }
            }

            Console.WriteLine("Admins: " + admins.Count);
            
            // if (Request.Query.ContainsKey("usuario"))
            // {
            //     // selectedUser = DatabaseManager.GetUsuario(Request.Query["usuario"].ToString());
            //     selectedUser = usuarios.Where(x => x.ID_usuario == Convert.ToInt32(Request.Query["usuario"])).FirstOrDefault();
            // } else {
                
            // }

            

            return Page();
        }

        public void OnPost()
        {
            

            if (Request.Form.ContainsKey("setAdmin")) {

                usuarios = DatabaseManager.GetUsuarios();

                if (Request.Query.ContainsKey("usuario"))
                {
                    selectedUser = usuarios.Where(x => x.ID_usuario == Convert.ToInt32(Request.Query["usuario"])).FirstOrDefault();
                } else {
                    selectedUser = new Usuario();
                    selectedUser.ID_usuario = -1;
                }

                // Console.WriteLine(selectedUser.ID_usuario + " " + selectedUser.Admin);

                if (selectedUser.ID_usuario != -1)
                {
                    DatabaseManager.UpdateAdmin(selectedUser.ID_usuario, selectedUser.Admin == true ? false : true);
                }
                Response.Redirect("Admins");
                return;
            }
            
            string path = $"Admins?";
            
            if (Request.Form.ContainsKey("reset"))
            {
                Response.Redirect(path);
                return;
            }


            if (!String.IsNullOrEmpty(SearchString) && SearchString != "")
            {
                path += $"&search={SearchString}";
            }
            
            Console.WriteLine(path);
            Response.Redirect(path);
        }

        
    }
}
