using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccesoDatos;
using Dominio;

namespace ObligatorioProgramacion2.Pages
{
    public class LoginModel : PageModel
    {
        private EmpleadoRepositorio _repo;

        public LoginModel(EmpleadoRepositorio repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public string Usuario { get; set; }

        [BindProperty]
        public string Contrasenia { get; set; }

        public string MensajeError { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Empleado emp = _repo.Login(Usuario, Contrasenia);

            if (emp == null)
            {
                MensajeError = "Usuario o contraseña incorrectos";
                return Page();
            }

            HttpContext.Session.SetInt32("IdEmpleado", emp.idEmpleado);
            HttpContext.Session.SetString("NombreEmpleado", emp.Nombre);

            return RedirectToPage("/Eventos/ListaCliente");
        }

    }
}
