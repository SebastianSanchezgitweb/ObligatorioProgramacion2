using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EventoCorporativo : Evento
    {
        
        
        public string NombreEmpresa { get; set; }
        public bool RequiereEquipamientoTecnologico { get; set; }

       
        public static float CostoPorAsistente = 1000;
        public static float CostoEquipamientoTecnologico = 3000;
        public static float DescuentoMas100P = 0.10f;




        public override float CalcularCosto()
        {
            float total = 0;
         
            //   total = total + PrecioFijoAlquiler;
            total += PrecioFijoAlquiler;

            // pprecio por asistente
            total += CantidadAsistentes * CostoPorAsistente;

            // servicios contratados
            total += CalcularCostoServicios();

            // equipamiento tecnologico
            if (RequiereEquipamientoTecnologico)
            {
                total += CostoEquipamientoTecnologico;
            }

            // descuento si supera 100 asistentes
            if (CantidadAsistentes > 100)
            {
                total = total - (total * 0.10f);
            }

            return total;
        }

        
    }
}
