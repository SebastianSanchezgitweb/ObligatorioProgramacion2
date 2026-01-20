using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
using AccesoDatos;
using System.Collections.Generic;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class ClienteModel : PageModel
    {
        private ClienteRepositorio _repo;

        public ClienteModel(ClienteRepositorio repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public Cliente nuevoCliente { get; set; }

        public List<Cliente> Clientes { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
            {
                return RedirectToPage("/Login");
            }

            nuevoCliente = new Cliente();
            Clientes = _repo.ObtenerClientes(); // ?? DB
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Clientes = _repo.ObtenerClientes();
                return Page();
            }

            _repo.AgregarCliente(nuevoCliente); // ?? GUARDA EN SQL

            return RedirectToPage();
        }

    }
}
