using AccesoDatos;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class ContratarServicio_CorporativoModel : PageModel
    {
        private readonly EventoRepositorio _eventoRepo;

        public ContratarServicio_CorporativoModel(EventoRepositorio eventoRepo)
        {
            _eventoRepo = eventoRepo;
        }

        [BindProperty]
        public EventoCorporativo EventoAAgregarServicio { get; set; } = new EventoCorporativo();

        [BindProperty]
        public ServiciosContratados servicio { get; set; } = new ServiciosContratados();

        [BindProperty]
        public int IdCategoria { get; set; }

        public IActionResult OnGet(int idEvento)
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null) return RedirectToPage("/Login");

            // Inicializamos el objeto con el ID que viene de la URL
            EventoAAgregarServicio = new EventoCorporativo { idEvento = idEvento };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (servicio.Costo < 0)
            {
                TempData["Error"] = "El costo no puede ser negativo";
                return Page();
            }

            try
            {
                // IMPORTANTE: Usamos el ID que viene del objeto bindeado
                _eventoRepo.AgregarServicioAEvento(servicio, EventoAAgregarServicio.idEvento, IdCategoria);

                TempData["Mensaje"] = "Servicio agregado correctamente.";
                return RedirectToPage("ListadoEventos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al guardar: " + ex.Message);
                return Page();
            }
        }
    }
}