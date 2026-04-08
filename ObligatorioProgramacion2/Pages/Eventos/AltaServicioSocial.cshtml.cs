using AccesoDatos;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class AltaServicioSocialModel : PageModel
    {
        private readonly EventoRepositorio _eventoRepo;

        public AltaServicioSocialModel(EventoRepositorio eventoRepo)
        {
            _eventoRepo = eventoRepo;
        }

        [BindProperty]
        public EventoSociales EventoAAgregarServicio { get; set; } = new EventoSociales();

        [BindProperty]
        public ServiciosContratados servicio { get; set; } = new ServiciosContratados();

        [BindProperty]
        public int IdCategoria { get; set; }

        public IActionResult OnGet(int idEvento)
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null) return RedirectToPage("/Login");

            var evt = _eventoRepo.ObtenerEventoSocialesPorId(idEvento);
            if (evt == null) return NotFound();

            // Inicializa el id y cliente para que el hidden tenga valor
            EventoAAgregarServicio = new EventoSociales { idEvento = idEvento, Cliente = evt.Cliente };
            return Page();
        }

        public IActionResult OnPost()
        {
            // Fallback: leer idEvento de la ruta si el hidden no vino
            if (EventoAAgregarServicio == null || EventoAAgregarServicio.idEvento <= 0)
            {
                if (RouteData.Values.TryGetValue("idEvento", out var routeId) && int.TryParse(routeId?.ToString(), out var parsedId))
                {
                    EventoAAgregarServicio.idEvento = parsedId;
                }
            }

            if (EventoAAgregarServicio == null || EventoAAgregarServicio.idEvento <= 0)
            {
                ModelState.AddModelError(string.Empty, "Id de evento inválido.");
                return Page();
            }

            // Comprobaciones
            if (_eventoRepo.BuscarPorId(EventoAAgregarServicio.idEvento) == null)
            {
                ModelState.AddModelError(string.Empty, $"Evento {EventoAAgregarServicio.idEvento} no existe.");
                return Page();
            }

            if (!CategoriaServicio.ListaCategoriaServicio.Any(c => c.IdCategoria == IdCategoria))
            {
                ModelState.AddModelError(string.Empty, "Categoría inválida.");
                return Page();
            }

            if (servicio.Costo < 0)
            {
                ModelState.AddModelError(string.Empty, "El costo no puede ser negativo.");
                return Page();
            }

            try
            {
                _eventoRepo.AgregarServicioAEvento(servicio, EventoAAgregarServicio.idEvento, IdCategoria);
                TempData["Mensaje"] = "Servicio social contratado.";
                return RedirectToPage("ListadoEventos");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (SqlException ex)
            {
                ModelState.AddModelError(string.Empty, "Error SQL: " + ex.Message);
                return Page();
            }
        }
    }
}

