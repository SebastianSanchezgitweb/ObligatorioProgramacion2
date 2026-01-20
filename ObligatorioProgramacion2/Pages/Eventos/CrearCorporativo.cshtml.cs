using Dominio;
using AccesoDatos; // 🔹 Asegúrate de agregar el using de tu capa de datos
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class CrearModel : PageModel
    {
        // 1. Declaramos los repositorios que vamos a usar
        private readonly ClienteRepositorio _clienteRepo;
        private readonly EventoRepositorio _eventoRepo;

        // 2. El constructor recibe los repositorios (Inyectados desde Program.cs)
        public CrearModel(ClienteRepositorio clienteRepo, EventoRepositorio eventoRepo)
        {
            _clienteRepo = clienteRepo;
            _eventoRepo = eventoRepo;
        }

        [BindProperty]
        public EventoCorporativo NuevoEventoCorporativo { get; set; }

        public Cliente Cliente { get; set; }

        public IActionResult OnGet(int idCliente)
        {
            if (!ModelState.IsValid)
            {
                // Si hay errores de validación, se queda en la página actual
                return Page();
            }

            // Verificación de sesión
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
            {
                return RedirectToPage("/Login");
            }

            // 3. Buscamos el cliente en la BD usando el repositorio
            // (Nota: Asegúrate de tener un método para buscar por ID o filtramos la lista)
            Cliente = _clienteRepo.ObtenerClientes().FirstOrDefault(c => c.IdCliente == idCliente);

            if (Cliente == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int idCliente)
        {
            // 4. Recuperamos el cliente nuevamente para asociarlo al evento
            Cliente = _clienteRepo.ObtenerClientes().FirstOrDefault(c => c.IdCliente == idCliente);

            if (Cliente == null) return NotFound();

            // Asignamos el objeto Cliente completo al evento
            NuevoEventoCorporativo.Cliente = Cliente;

            // 5. GUARDAR EN BD: Usamos el repositorio en lugar de la clase Empresa
            try
            {
                _eventoRepo.AgregarEvento(NuevoEventoCorporativo);
                TempData["Mensaje"] = "Evento Corporativo creado con éxito en la base de datos.";
                return RedirectToPage("ListadoEventos");
            }
            /* catch (System.Exception ex)
             {
                 // En caso de error (ej: problema de conexión)
                 ModelState.AddModelError(string.Empty, "Error al guardar en la base de datos: " + ex.Message);
                 return Page();
             }*/
            catch (System.Exception ex)
            {
                // Esto te mostrará el error exacto de SQL en la parte superior de la página
                ModelState.AddModelError(string.Empty, "ERROR REAL: " + ex.Message + " | " + ex.InnerException?.Message);
                return Page();
            }
        }
    }
}