using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class PromedioModel : PageModel
    {
        public float promediocorp { get; set; }
        public float promediosocial { get; set; }

        public bool MostrarCorp { get; set; }
        public bool MostrarSocial { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
            {
                return RedirectToPage("/Login");
            }

            return Page();
        }

        public IActionResult OnPost(string accion)
        {
            if (accion == "corp")
            {
                MostrarCorp = true;
                promediocorp = Empresa.Instancia.PromedioEventoCorporativo();
            }
            else if (accion == "social")
            {
                MostrarSocial = true;
                promediosocial = Empresa.Instancia.PromedioEventoSociales();
            }

            return Page();
        }
    }
}
