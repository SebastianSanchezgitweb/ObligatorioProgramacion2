using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Usuario { get; set; }

        [BindProperty]
        public string Contraseña { get; set; }

        public string MensajeError { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Empleado empleadoEncontrado = null;

            foreach (Empleado emp in Empresa.Instancia.ObtenerEmpleados())
            {
                if (emp.Usuario == Usuario && emp.Contraseña == Contraseña)
                {
                    empleadoEncontrado = emp;
                    break;
                }
            }

            if (empleadoEncontrado != null)
            {
                // Login correcto
                return RedirectToPage("ListaCliente");
            }
            else
            {
                MensajeError = "Usuario o contraseña incorrectos";
                return Page();
            }
        }
    }
}
