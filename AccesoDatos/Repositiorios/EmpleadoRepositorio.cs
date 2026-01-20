using Dominio;
using Microsoft.Data.SqlClient;

namespace AccesoDatos
{
    public class EmpleadoRepositorio
    {
        private SqlConnection _connection;

        public EmpleadoRepositorio(SqlConnection connection)
        {
            _connection = connection;
        }

        public Empleado? Login(string usuario, string contrasenia)
        {
            string sql = @"SELECT * FROM EMPLEADO 
                           WHERE Usuario = @usuario AND Contrasenia = @contrasenia";

            SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@contrasenia", contrasenia);

            _connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Empleado emp = new Empleado(
                    (int)reader["IdEmpleado"],
                    reader["Nombre"].ToString(),
                    reader["Apellido"].ToString(),
                    reader["Documento"].ToString(),
                    (int)reader["Telefono"],
                    reader["Usuario"].ToString(),
                    reader["Contrasenia"].ToString()
                );

                _connection.Close();
                return emp;
            }

            _connection.Close();
            return null;
        }
    }
}
