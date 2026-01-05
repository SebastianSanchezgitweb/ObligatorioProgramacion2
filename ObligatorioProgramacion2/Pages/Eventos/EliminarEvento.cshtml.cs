using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class EliminarEventoModel : PageModel
    {
        [BindProperty]
        public Evento EventoAEliminar { get; set; }
        public IActionResult OnGet(int idEvento)
        {
            EventoAEliminar = Empresa.Instancia.ObtenerEventosPorID(idEvento);
            if (EventoAEliminar == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            Evento even = Empresa.Instancia.ObtenerEventosPorID(EventoAEliminar.idEvento);


            if (even.Estado != EstadoEvento.Realizado)
            {
                TempData["Error"] = "No Se Puede Eliminar Un EVENTO que no este Realizado";
                return Page();
            }
            else {
                Empresa.Instancia.EliminarEvento(even.idEvento);

            }
        

            TempData["Mensaje"] = $"Evento {even.Nombre} eliminado!";
            return RedirectToPage("ListadoEventos");
    }
 } 

}