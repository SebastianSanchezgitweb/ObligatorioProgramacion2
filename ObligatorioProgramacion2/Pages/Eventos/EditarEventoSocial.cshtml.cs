using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccesoDatos;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class EditarEventoSocialModel : PageModel
    {
        private readonly EventoRepositorio _repo;

        public EditarEventoSocialModel(EventoRepositorio repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public EventoSociales EventoSocialEditar { get; set; }

        public IActionResult OnGet(int idEvento)
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
                return RedirectToPage("/Login");

            EventoSocialEditar = _repo.ObtenerEventoSocialesPorId(idEvento);
            if (EventoSocialEditar == null) return NotFound();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
                return RedirectToPage("/Login");

            if (!ModelState.IsValid) return Page();

            // Preservar Cliente si no viene en el formulario
            if (EventoSocialEditar.Cliente == null || EventoSocialEditar.Cliente.IdCliente == 0)
            {
                var existente = _repo.ObtenerEventoSocialesPorId(EventoSocialEditar.idEvento);
                if (existente != null)
                    EventoSocialEditar.Cliente = existente.Cliente;
            }

            _repo.ModificarEventoSocial(EventoSocialEditar);

            return RedirectToPage("ListadoEventos");
        }
    }
}
