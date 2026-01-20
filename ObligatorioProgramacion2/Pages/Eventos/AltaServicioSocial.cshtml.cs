using AccesoDatos;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    // ... (Usings similares)

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
            public EventoSociales EventoAAgregarServicio { get; set; }

            [BindProperty]
            public ServiciosContratados servicio { get; set; }

            [BindProperty]
            public int IdCategoria { get; set; }

            public IActionResult OnGet(int idEvento)
            {
                if (HttpContext.Session.GetInt32("IdEmpleado") == null) return RedirectToPage("/Login");

                // Aquí podrías necesitar un método ObtenerEventoSocialPorId en el repo similar al corporativo
                return Page();
            }

            public IActionResult OnPost()
            {
                if (servicio.Costo < 0)
                {
                    TempData["Error"] = "El costo no puede ser negativo";
                    return Page();
                }

                _eventoRepo.AgregarServicioAEvento(servicio, EventoAAgregarServicio.idEvento, IdCategoria);

                TempData["Mensaje"] = "Servicio social contratado.";
                return RedirectToPage("ListadoEventos");
            }
        }
    }
}
