using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class DetalleSocialModel : PageModel
{
    [BindProperty]
    public EventoSociales? EventoSocialesDetalle { get; set; }

    public List<ServiciosContratados> servicios;

    public IActionResult OnGet(int idEvento)
    {
        // Verifica si el empleado está logueado
        if (HttpContext.Session.GetInt32("IdEmpleado") == null)
        {
            return RedirectToPage("/Login");
        }

        // Obtiene los datos del evento social por su ID
        EventoSocialesDetalle = Empresa.Instancia.ObtenerEventoSocialesPorId(idEvento);

        if (EventoSocialesDetalle == null)
        {
            return NotFound();
        }

        // Carga la lista de servicios contratados para este evento
        servicios = EventoSocialesDetalle.ObtenerServicios();

        return Page();
    }
}