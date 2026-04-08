using AccesoDatos;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class DetalleSocialModel : PageModel
    {
        private readonly EventoRepositorio _repo;

        public DetalleSocialModel(EventoRepositorio repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public EventoSociales? EventoSocialesDetalle { get; set; }

        public List<ServiciosContratados> servicios { get; set; } = new();

        public IActionResult OnGet(int idEvento)
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
            {
                return RedirectToPage("/Login");
            }

            EventoSocialesDetalle = _repo.ObtenerEventoSocialesPorId(idEvento);

            if (EventoSocialesDetalle == null)
            {
                return NotFound();
            }

            // El repo ya carga servicios en ObtenerEventoSocialesPorId (CargarServicios)
            servicios = EventoSocialesDetalle.ObtenerServicios();

            return Page();
        }
    }
}