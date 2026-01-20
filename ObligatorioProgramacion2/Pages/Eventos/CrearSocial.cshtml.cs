using Dominio;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class CrearSocialModel : PageModel
    {
        private readonly ClienteRepositorio _clienteRepo;
        private readonly EventoRepositorio _eventoRepo;

        public CrearSocialModel(ClienteRepositorio clienteRepo, EventoRepositorio eventoRepo)
        {
            _clienteRepo = clienteRepo;
            _eventoRepo = eventoRepo;
        }

        [BindProperty]
        public EventoSociales NuevoEventoSocial { get; set; }
        public Cliente Cliente { get; set; }

        public IActionResult OnGet(int idCliente)
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null) return RedirectToPage("/Login");

            // Buscamos el cliente en la BD
            Cliente = _clienteRepo.ObtenerClientes().FirstOrDefault(c => c.IdCliente == idCliente);

            if (Cliente == null) return NotFound();
            return Page();
        }

        public IActionResult OnPost(int idCliente)
        {
            Cliente = _clienteRepo.ObtenerClientes().FirstOrDefault(c => c.IdCliente == idCliente);
            if (Cliente == null) return NotFound();

            NuevoEventoSocial.Cliente = Cliente;

            try
            {
                _eventoRepo.AgregarEvento(NuevoEventoSocial);
                TempData["Mensaje"] = "Evento Social creado con éxito.";
                return RedirectToPage("ListadoEventos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex.Message);
                return Page();
            }
        }
    }
}