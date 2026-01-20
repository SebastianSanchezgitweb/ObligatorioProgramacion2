using System;

namespace Dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string? Identificacion { get; set; }
        public string? TipoIdentificacion { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }

        public Cliente() { }

        public Cliente(int idcliente, string? identificacion, string? tipodeidentificacion, string? telefono, string? nombre, string? direccion)
        {
            IdCliente = idcliente;
            Identificacion = identificacion;
            TipoIdentificacion = tipodeidentificacion;
            Telefono = telefono;
            Nombre = nombre;
            Direccion = direccion;
        }
    }
}