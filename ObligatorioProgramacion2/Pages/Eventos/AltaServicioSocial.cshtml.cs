using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class AltaServicioSocialModel : PageModel
    {
        [BindProperty]
        public EventoSociales EventoAAgregarServicio { get; set; }

        [BindProperty]
        public ServiciosContratados servicio { get; set; }

        [BindProperty]
        public int IdCategoria { get; set; }



        public IActionResult OnGet(int idEvento)
        {


            EventoAAgregarServicio = Empresa.Instancia.ObtenerEventoSocialesPorId(idEvento);

            if (EventoAAgregarServicio == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {


            Evento even = Empresa.Instancia.ObtenerEventoSocialesPorId(EventoAAgregarServicio.idEvento);

            if (servicio.Costo < 0)
            {
                TempData["Error"] = "El Costo no Puede ser Menor a 0";
                return Page();
            }
            else
            {

                Empresa.Instancia.AgregarServicioEvento(servicio, even, IdCategoria);
                TempData["Mensaje"] = $"Servicio {servicio.Categoria.NombreCategoria} Agregado!";
            }

            return RedirectToPage("ListadoEventos");





           

        }
    }
}
