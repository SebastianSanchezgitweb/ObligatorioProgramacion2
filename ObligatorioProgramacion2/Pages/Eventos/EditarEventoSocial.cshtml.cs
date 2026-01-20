using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class EditarEventoSocialModel : PageModel
    {
        [BindProperty]
        public EventoSociales EventoSocialEditar { get; set; }




        public IActionResult OnGet(int idEvento) //OnGet - Carga inicial de la página

        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
            {
                return RedirectToPage("/Login");
            }


            EventoSocialEditar = Empresa.Instancia.ObtenerEventoSocialesPorId(idEvento);

            if (EventoSocialEditar == null)
            {
                return NotFound();

            }
            return Page();
        }

        public IActionResult OnPost()//Se ejecuta cuando envías el formulario (presionas el botón Submit/Guardar).
        {
            Empresa.Instancia.EditarEvento(EventoSocialEditar);

            return RedirectToPage("ListadoEventos");
        }
    }
}
