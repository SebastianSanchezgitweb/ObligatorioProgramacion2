using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class ClienteModel : PageModel
    {
        [BindProperty]
        public Cliente nuevoCliente { get; set; }

        



        public void OnGet()
        {
            nuevoCliente = new Cliente();
        }


        public IActionResult OnPost()
        {

             
            
            
            Empresa.Instancia.AgregarCliente(nuevoCliente);
         


          

            return RedirectToPage("ListaCliente");
        }

    }
}
