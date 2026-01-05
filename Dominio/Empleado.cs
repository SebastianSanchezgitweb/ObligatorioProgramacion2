using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empleado
    {

        public int idEmpleado { get; set; }
      
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }

        public int Telefono { get; set; }

        public string Usuario { get; set; }
        public string Contraseña { get; set; }


        public Empleado(int idempleado,string nombre,string apellido, string documento, int telefono, string usuario,string contraseña)//constructor
        {
            idEmpleado = idempleado;
            Nombre = nombre;
            Apellido = apellido;
            Documento = documento;
            Telefono = telefono;
            Usuario = usuario;
            Contraseña = contraseña;

        }

    }
}
