using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class FiltrarPorFechaYMontoModel : PageModel
    {
        [BindProperty]
        public DateTime FechaRealizacion { get; set; }

        [BindProperty]
        public DateTime FechaFinalizacion { get; set; }

        [BindProperty]
        public int Monto { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
            {
                return RedirectToPage("/Login");
            }
             return Page();

        }

        public IActionResult OnPost()
        {

           
            Empresa.Instancia.ListaEventosFiltradosPorFechas(FechaRealizacion, FechaFinalizacion, Monto);
            return Page();
        }

    }
}
