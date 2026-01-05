namespace Dominio
{
    public class Evento
    {



        public Cliente Cliente { get; set; }

        public int idEvento { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public DateTime FechaFinalizacion { get; set; }

        public EstadoEvento Estado { get; set; }

        public string Ubicacion { get; set; }

        public int CantidadAsistentes { get; set; }

        


        public List<ServiciosContratados> servicioscontratados { get; set; } = new List<ServiciosContratados>();

        // Precio fijo común a todos los eventos
        public static float PrecioFijoAlquiler = 1000;


        public int proximoIDServicio = 1;



        public List<ServiciosContratados> ObtenerServicios()
        {
            return new List<ServiciosContratados>(servicioscontratados);
        }



        
        public float CalcularCostoServicios()
        {
            float total = 0;

            foreach (ServiciosContratados serv in servicioscontratados)
            {
                total += serv.Costo;
            }

            return total;
        }

       
        public virtual float CalcularCosto()
        {
            return 0;
        }

       


    }
}


    

