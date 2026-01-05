using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class CrearModel : PageModel
    {

        [BindProperty]
        public EventoCorporativo NuevoEventoCorporativo { get; set; }

        public Cliente Cliente { get; set; }
        

        public IActionResult OnGet(int idCliente) //OnGet - Carga inicial de la página

        {
            Cliente = Empresa.Instancia.ObtenerClientePorId(idCliente);

            if (Cliente == null) { 
                return NotFound();
            
            }
            return Page();
        }

        public IActionResult OnPost(int idCliente)//se ejecuta cuando envías el formulario (presionas el botón Submit/Guardar).
        {
           //recuperar cliente
            Cliente = Empresa.Instancia.ObtenerClientePorId(idCliente);

            // asigno el objeto Cliente al evento
            NuevoEventoCorporativo.Cliente = Cliente;
            

            // Guardar el evento
            Empresa.Instancia.AgregarEvento(NuevoEventoCorporativo);

            return RedirectToPage("ListadoEventos");
        }
    }
}


