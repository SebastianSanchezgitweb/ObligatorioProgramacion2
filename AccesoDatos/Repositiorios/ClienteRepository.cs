using Dominio;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace AccesoDatos
{
    public class ClienteRepositorio
    {
        private SqlConnection _connection;

        public ClienteRepositorio(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            _connection.Open();

            string sql = "SELECT * FROM Cliente";
            SqlCommand cmd = new SqlCommand(sql, _connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Cliente cliente = new Cliente
                {
                    IdCliente = (int)reader["IdCliente"],
                    Identificacion = reader["Identificacion"].ToString(),
                    TipoIdentificacion = reader["TipoIdentificacion"].ToString(),
                    Nombre = reader["Nombre"].ToString(),
                    Telefono = reader["Telefono"].ToString(),
                    Direccion = reader["Direccion"].ToString()
                };

                clientes.Add(cliente);
            }

            _connection.Close();
            return clientes;
        }


        public void AgregarCliente(Cliente cliente)
        {
            string sql = @"INSERT INTO Cliente 
                   (Identificacion, TipoIdentificacion, Nombre, Telefono, Direccion)
                   VALUES 
                   (@Identificacion, @TipoIdentificacion, @Nombre, @Telefono, @Direccion)";

            SqlCommand cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.AddWithValue("@Identificacion", cliente.Identificacion);
            cmd.Parameters.AddWithValue("@TipoIdentificacion", cliente.TipoIdentificacion);
            cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
            cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
       


    }
}
