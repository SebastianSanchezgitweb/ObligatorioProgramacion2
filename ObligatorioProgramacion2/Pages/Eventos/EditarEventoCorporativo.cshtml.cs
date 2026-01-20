using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class EditarEventoCorporativoModel : PageModel
    {
        [BindProperty]
        public EventoCorporativo EventoCorporativoEditar { get; set; }

        

        //public List<SelectListItem> ListaEstados { get; set; }

        public IActionResult OnGet(int idEvento) //OnGet - Carga inicial de la página

        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
            {
                return RedirectToPage("/Login");
            }



            EventoCorporativoEditar = Empresa.Instancia.ObtenerEventoCorporativoPorId(idEvento);

            if (EventoCorporativoEditar == null)
            {
                return NotFound();

            }
            return Page();
        }

        public IActionResult OnPost()
        {
            Empresa.Instancia.EditarEvento(EventoCorporativoEditar);

            return RedirectToPage("ListadoEventos");
        }
    }
}