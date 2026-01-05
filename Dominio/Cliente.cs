using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public int Identificacion { get; set; }
        public string TipodeIdentificacion { get; set; }
        public int Telefono { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        
        // CONSTRUCTOR VACÍO (OBLIGATORIO PARA RAZOR) porque 
        public Cliente()
        {
        }

        public Cliente(int idcliente,int identificacion,string tipodeidentificacion,int telefono,string nombre, string direccion)
        {
            IdCliente = idcliente;
            Identificacion = identificacion;
            TipodeIdentificacion = tipodeidentificacion;
            Telefono = telefono;
            Nombre = nombre;
            Direccion = direccion;
        }



    }
}
