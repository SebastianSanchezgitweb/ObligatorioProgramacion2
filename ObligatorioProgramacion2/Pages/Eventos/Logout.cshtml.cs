using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
            {
                return RedirectToPage("/Login");
            }


            HttpContext.Session.Clear(); // 🔥 BORRA LA SESIÓN
            return RedirectToPage("/Eventos/Login");
        }
    }
}
