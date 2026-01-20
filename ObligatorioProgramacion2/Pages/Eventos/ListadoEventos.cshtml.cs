using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Dominio;
using AccesoDatos;
using System.Collections.Generic;

namespace ObligatorioProgramacion2.Pages.Eventos
{
    public class ListadoEventosModel : PageModel
    {
        private readonly EventoRepositorio _eventoRepo;

        public ListadoEventosModel(EventoRepositorio eventoRepo)
        {
            _eventoRepo = eventoRepo;
        }

        // Listas donde guardaremos los eventos que vienen de la BD
        public List<EventoCorporativo> ListaCorporativos { get; set; } = new List<EventoCorporativo>();
        public List<EventoSociales> ListaSociales { get; set; } = new List<EventoSociales>();

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("IdEmpleado") == null)
            {
                return RedirectToPage("/Login");
            }

            // Traemos todos los eventos desde el Repositorio (SQL)
            var todosLosEventos = _eventoRepo.ObtenerEventos();

            // Los separamos por tipo para las tablas de la vista
            foreach (var ev in todosLosEventos)
            {
                if (ev is EventoCorporativo corp) ListaCorporativos.Add(corp);
                
                else if (ev is EventoSociales soc) ListaSociales.Add(soc);
            }

            return Page();
        }
    }
}