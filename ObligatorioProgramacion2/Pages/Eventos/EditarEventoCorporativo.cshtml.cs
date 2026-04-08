using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccesoDatos;

namespace ObligatorioProgramacion2.Pages.Eventos
{   
    public class EditarEventoCorporativoModel : PageModel
    {
        private readonly EventoRepositorio _repo;

        public EditarEventoCorporativoModel(EventoRepositorio repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public EventoCorporativo EventoCorporativoEditar { get; set; }

        public IActionResult OnGet(int idEvento)
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
                return RedirectToPage("/Login");

            EventoCorporativoEditar = _repo.ObtenerEventoCorporativoPorId(idEvento);

            if (EventoCorporativoEditar == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
                return RedirectToPage("/Login");

            if (!ModelState.IsValid)
                return Page();

            // Preservar IdCliente en caso de que no venga en el form
            if (EventoCorporativoEditar.Cliente == null || EventoCorporativoEditar.Cliente.IdCliente == 0)
            {
                var existente = _repo.ObtenerEventoCorporativoPorId(EventoCorporativoEditar.idEvento);
                if (existente != null)
                    EventoCorporativoEditar.Cliente = existente.Cliente;
            }

            _repo.ModificarEventoCorporativo(EventoCorporativoEditar);

            return RedirectToPage("ListadoEventos");
        }
    }
}