using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class DetalleCorporativoModel : PageModel
    {

        [BindProperty]
        public EventoCorporativo EventoCorporativoDetalle { get; set; }

        public List<ServiciosContratados> servicios;

        public int IDEvento { get; set; }

        float costoCorporativo {  get; set; }


        public IActionResult OnGet(int idEvento) //OnGet - Carga inicial de la página

        {

            EventoCorporativoDetalle = Empresa.Instancia.ObtenerEventoCorporativoPorId(idEvento);
            
            IDEvento = EventoCorporativoDetalle.idEvento;

            if (EventoCorporativoDetalle == null)
            {
                return NotFound();

            }

            servicios = EventoCorporativoDetalle.ObtenerServicios();

          
            return Page();
        }



        public IActionResult OnPost()
        {


            return RedirectToPage("ListadoEventos");
        }
    }
}
