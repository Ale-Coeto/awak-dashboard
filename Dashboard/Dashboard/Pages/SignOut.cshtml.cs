using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Dashboard.Pages
{
    public class SignOutModel : PageModel
    {

        // Code to bypass OAUTH (for testing without tenant classes)
        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("OpenIdConnect");
            HttpContext.Session.Clear();


            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("./login");
            } 

            return Redirect("./MicrosoftIdentity/Account/SignOut");
        }

    }
}