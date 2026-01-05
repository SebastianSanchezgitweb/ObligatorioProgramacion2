using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EventoSociales : Evento
    {
        public string TipoColaboracion { get; set; }
        public bool IncluyeCatering { get; set; }

        public static float PrecioPorInvitadoCatering = 30f;


        public override float CalcularCosto()
        {
            float total = 0;

            // precio fijo
            total += PrecioFijoAlquiler;

            // servicios contratados
            total += CalcularCostoServicios();

            // catering
            if (IncluyeCatering)
            {
                total += CantidadAsistentes * PrecioPorInvitadoCatering;
            }

            return total;
        }




    }

      

    }
