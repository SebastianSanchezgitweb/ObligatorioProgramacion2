using AccesoDatos;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Para el Session

namespace ObligatorioProgramacion2.Pages.Eventos // <--- ESTO ES LO QUE FALTA
{
    public class DetalleCorporativoModel : PageModel
    {
        private readonly EventoRepositorio _eventoRepo;

        public DetalleCorporativoModel(EventoRepositorio eventoRepo)
        {
            _eventoRepo = eventoRepo;
        }

        [BindProperty]
        public EventoCorporativo EventoCorporativoDetalle { get; set; }

        // Agregamos esta propiedad para que el foreach de tu HTML funcione
        public List<ServiciosContratados> servicios { get; set; } = new List<ServiciosContratados>();

        public IActionResult OnGet(int idEvento)
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
                return RedirectToPage("/Login");

            EventoCorporativoDetalle = _eventoRepo.ObtenerEventoCorporativoPorId(idEvento);

            if (EventoCorporativoDetalle == null)
                return NotFound();

            // Llenamos la lista de servicios para que no de error el @foreach
            servicios = EventoCorporativoDetalle.servicioscontratados;

            return Page();
        }
    }
}