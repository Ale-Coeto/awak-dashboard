﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dashboard.Pages
{
	public class InicioModel : PageModel
    {
        public string Nombre { set; get; } = "";

        public void OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Nombre")) == false)
            {
                Nombre = HttpContext.Session.GetString("Nombre");
            }
        }
    }
}
