namespace Dominio
{
    public class Empleado
    {
        public int idEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; } // Agregado ?
        public string? Documento { get; set; }
        public int? Telefono { get; set; }
        public string? Usuario { get; set; } // Agregado ?
        public string? Contrasenia { get; set; } // Agregado ?

        public Empleado() { } // Te recomiendo agregar un constructor vacío también

        public Empleado(int idempleado, string? nombre, string? apellido, string? documento, int? telefono, string? usuario, string? contraseña)
        {
            idEmpleado = idempleado;
            Nombre = nombre;
            Apellido = apellido;
            Documento = documento;
            Telefono = telefono;
            Usuario = usuario;
            Contrasenia = contraseña;
        }
    }
}