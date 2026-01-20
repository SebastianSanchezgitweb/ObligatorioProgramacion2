using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
using AccesoDatos;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class EliminarEventoModel : PageModel
    {
        private readonly EventoRepositorio _repo;

        public EliminarEventoModel(EventoRepositorio repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public Evento EventoAEliminar { get; set; }

        public IActionResult OnGet(int idEvento)
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
            {
                return RedirectToPage("/Eventos/Login");
            }

            EventoAEliminar = _repo.BuscarPorId(idEvento);

            if (EventoAEliminar == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            Evento evento = _repo.BuscarPorId(EventoAEliminar.idEvento);

            if (evento == null)
            {
                return NotFound();
            }

            // ❌ No permitir eliminar eventos ya realizados
            if (evento.Estado == EstadoEvento.Realizado)
            {
                TempData["Error"] = "No se puede eliminar un evento que ya fue realizado.";
                return Page();
            }

            _repo.EliminarEvento(evento.idEvento);

            TempData["Mensaje"] = $"Evento '{evento.Nombre}' eliminado correctamente.";
            return RedirectToPage("/Eventos/ListadoEventos");
        }
    }
}
