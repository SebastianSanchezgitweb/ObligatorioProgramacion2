using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ServiciosContratados
    {
        

        public string TipoServicio { get; set; }

        public CategoriaServicio Categoria { get; set; }


        public int IdCategoria { get; set; }
        public float Costo { get; set; }
        public string Proveedor { get; set; }

        public int IdServicio { get; set; }


       

        public ServiciosContratados()
        {

        }


        public ServiciosContratados(int Idservicios,string tiposervicio,int precio, string proveedor)
        {
            IdServicio = Idservicios;
            TipoServicio = tiposervicio;
            Costo = precio;
            Proveedor = proveedor;
        }

    }
}
