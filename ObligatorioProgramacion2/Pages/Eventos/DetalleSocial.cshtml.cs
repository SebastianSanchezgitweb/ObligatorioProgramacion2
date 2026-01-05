using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class DetalleSocialModel : PageModel
    {
        [BindProperty]
        public EventoSociales EventoSocialesDetalle { get; set; }

        public List<ServiciosContratados> servicios;

        public int IDEvento { get; set; }

        float costoCorporativo { get; set; }


        public IActionResult OnGet(int idEvento) //OnGet - Carga inicial de la página

        {

            EventoSocialesDetalle = Empresa.Instancia.ObtenerEventoSocialesPorId(idEvento);

            IDEvento = EventoSocialesDetalle.idEvento;

            if (EventoSocialesDetalle == null)
            {
                return NotFound();

            }

            servicios = EventoSocialesDetalle.ObtenerServicios();


            return Page();
        }



        public IActionResult OnPost()
        {


            return RedirectToPage("ListadoEventos");
        }
    }
}
