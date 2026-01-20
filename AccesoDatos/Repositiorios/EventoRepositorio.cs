using Dominio;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AccesoDatos
{
    public class EventoRepositorio
    {
        private readonly SqlConnection _connection;

        public EventoRepositorio(SqlConnection connection)
        {
            _connection = connection;
        }

        // 1. LISTADO GENERAL
        public List<Evento> ObtenerEventos()
        {
            List<Evento> lista = new List<Evento>();
            string sql = "SELECT * FROM Evento";

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();

            _connection.Open();

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tipo = reader["TipoEvento"].ToString();
                    Evento e;

                    if (tipo == "Corporativo")
                    {
                        e = new EventoCorporativo
                        {
                            NombreEmpresa = reader["NombreEmpresa"]?.ToString(),
                            RequiereEquipamientoTecnologico =
                                reader["RequiereEquipamientoTecnologico"] != DBNull.Value &&
                                (bool)reader["RequiereEquipamientoTecnologico"]
                        };
                    }
                    else
                    {
                        e = new EventoSociales
                        {
                            TipoColaboracion = reader["TipoColaboracion"]?.ToString(),
                            IncluyeCatering =
                                reader["IncluyeCatering"] != DBNull.Value &&
                                (bool)reader["IncluyeCatering"]
                        };
                    }

                    // Propiedades comunes
                    e.idEvento = (int)reader["IdEvento"];
                    e.Nombre = reader["Nombre"].ToString();
                    e.Ubicacion = reader["Ubicacion"].ToString();
                    e.Estado = Enum.Parse<EstadoEvento>(reader["Estado"].ToString());
                    e.FechaRealizacion = (DateTime)reader["FechaRealizacion"];
                    e.FechaFinalizacion = (DateTime)reader["FechaFinalizacion"];
                    e.CantidadAsistentes = (int)reader["CantidadAsistentes"];

                    lista.Add(e);
                }
            }

            _connection.Close();
            return lista;
        }

        // 2. EVENTO SOCIAL POR ID
        public EventoSociales ObtenerEventoSocialesPorId(int id)
        {
            EventoSociales evento = null;
            string sql = "SELECT * FROM Evento WHERE IdEvento = @id AND TipoEvento = 'Social'";

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();

            _connection.Open();

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        evento = new EventoSociales
                        {
                            idEvento = (int)reader["IdEvento"],
                            Nombre = reader["Nombre"].ToString(),
                            Ubicacion = reader["Ubicacion"].ToString(),
                            FechaRealizacion = (DateTime)reader["FechaRealizacion"],
                            FechaFinalizacion = (DateTime)reader["FechaFinalizacion"],
                            CantidadAsistentes = (int)reader["CantidadAsistentes"],
                            Estado = Enum.Parse<EstadoEvento>(reader["Estado"].ToString()),
                            TipoColaboracion = reader["TipoColaboracion"]?.ToString(),
                            IncluyeCatering =
                                reader["IncluyeCatering"] != DBNull.Value &&
                                (bool)reader["IncluyeCatering"],
                            Cliente = new Cliente { IdCliente = (int)reader["IdCliente"] }
                        };
                    }
                }
            }

            _connection.Close();

            if (evento != null)
                CargarServicios(evento);

            return evento;
        }

        // 3. EVENTO CORPORATIVO POR ID
        public EventoCorporativo ObtenerEventoCorporativoPorId(int id)
        {
            EventoCorporativo evento = null;
            string sql = "SELECT * FROM Evento WHERE IdEvento = @id AND TipoEvento = 'Corporativo'";

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();

            _connection.Open();

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        evento = new EventoCorporativo
                        {
                            idEvento = (int)reader["IdEvento"],
                            Nombre = reader["Nombre"].ToString(),
                            Ubicacion = reader["Ubicacion"].ToString(),
                            FechaRealizacion = (DateTime)reader["FechaRealizacion"],
                            FechaFinalizacion = (DateTime)reader["FechaFinalizacion"],
                            CantidadAsistentes = (int)reader["CantidadAsistentes"],
                            Estado = Enum.Parse<EstadoEvento>(reader["Estado"].ToString()),
                            NombreEmpresa = reader["NombreEmpresa"]?.ToString(),
                            RequiereEquipamientoTecnologico =
                                reader["RequiereEquipamientoTecnologico"] != DBNull.Value &&
                                (bool)reader["RequiereEquipamientoTecnologico"],
                            Cliente = new Cliente { IdCliente = (int)reader["IdCliente"] }
                        };
                    }
                }
            }

            _connection.Close();

            if (evento != null)
                CargarServicios(evento);

            return evento;
        }

        // 4. CARGAR SERVICIOS
        public void CargarServicios(Evento evento)
        {
            string sql = @"
        SELECT sc.IdServicio,
               sc.TipoServicio,
               sc.Costo,
               sc.Proveedor,
               cs.idCategoria,
               cs.NombreCategoria
        FROM SERVICIOS_CONTRATADOS sc
        INNER JOIN CATEGORIA_SERVICIO cs 
            ON sc.idCategoria = cs.idCategoria
        WHERE sc.idEvento = @idEvento";

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();

            _connection.Open();

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@idEvento", evento.idEvento);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        evento.servicioscontratados.Add(new ServiciosContratados
                        {
                            IdServicio = (int)reader["IdServicio"],
                            TipoServicio = reader["TipoServicio"].ToString(),
                            Costo = Convert.ToSingle(reader["Costo"]),
                            Proveedor = reader["Proveedor"].ToString(),
                            Categoria = new CategoriaServicio(
                                (int)reader["idCategoria"],
                                reader["NombreCategoria"].ToString()
                            )
                        });
                    }
                }
            }

            _connection.Close();
        }


        // 5. INSERTAR EVENTO
        public void AgregarEvento(Evento evento)
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();

            _connection.Open();
            SqlTransaction tx = _connection.BeginTransaction();

            try
            {
                string sql = @"
                INSERT INTO Evento
                (IdCliente, Nombre, Ubicacion, FechaRealizacion, FechaFinalizacion,
                 CantidadAsistentes, Estado, TipoEvento, NombreEmpresa,
                 RequiereEquipamientoTecnologico, TipoColaboracion, IncluyeCatering)
                VALUES
                (@idCliente, @nombre, @ubicacion, @fechaR, @fechaF,
                 @cant, @estado, @tipo, @nomEmp, @reqEq, @tipoCol, @incCat)";

                SqlCommand cmd = new SqlCommand(sql, _connection, tx);

                cmd.Parameters.AddWithValue("@idCliente", evento.Cliente.IdCliente);
                cmd.Parameters.AddWithValue("@nombre", evento.Nombre);
                cmd.Parameters.AddWithValue("@ubicacion", evento.Ubicacion);
                cmd.Parameters.AddWithValue("@fechaR", evento.FechaRealizacion);
                cmd.Parameters.AddWithValue("@fechaF", evento.FechaFinalizacion);
                cmd.Parameters.AddWithValue("@cant", evento.CantidadAsistentes);
                cmd.Parameters.AddWithValue("@estado", evento.Estado.ToString());

                if (evento is EventoCorporativo corp)
                {
                    cmd.Parameters.AddWithValue("@tipo", "Corporativo");
                    cmd.Parameters.AddWithValue("@nomEmp", corp.NombreEmpresa);
                    cmd.Parameters.AddWithValue("@reqEq", corp.RequiereEquipamientoTecnologico);
                    cmd.Parameters.AddWithValue("@tipoCol", DBNull.Value);
                    cmd.Parameters.AddWithValue("@incCat", DBNull.Value);
                }
                else
                {
                    EventoSociales soc = (EventoSociales)evento;
                    cmd.Parameters.AddWithValue("@tipo", "Social");
                    cmd.Parameters.AddWithValue("@nomEmp", DBNull.Value);
                    cmd.Parameters.AddWithValue("@reqEq", DBNull.Value);
                    cmd.Parameters.AddWithValue("@tipoCol", soc.TipoColaboracion);
                    cmd.Parameters.AddWithValue("@incCat", soc.IncluyeCatering);
                }

                cmd.ExecuteNonQuery();
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        // 6. AGREGAR SERVICIO A EVENTO
        public void AgregarServicioAEvento(ServiciosContratados servicio, int idEvento, int idCategoria)
        {
            string sql = @"
            INSERT INTO SERVICIOS_CONTRATADOS
            (IdEvento, IdCategoria, TipoServicio, Costo, Proveedor)
            VALUES
            (@idEvento, @idCat, @tipo, @costo, @prov)";

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();

            _connection.Open();

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@idEvento", idEvento);
                cmd.Parameters.AddWithValue("@idCat", idCategoria);
                cmd.Parameters.AddWithValue("@tipo", servicio.TipoServicio);
                cmd.Parameters.AddWithValue("@costo", servicio.Costo);
                cmd.Parameters.AddWithValue("@prov", servicio.Proveedor);
                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }


        public void EliminarEvento(int idEvento)
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();

            _connection.Open();

            // Primero eliminar servicios asociados
            string sqlServicios = "DELETE FROM SERVICIOS_CONTRATADOS WHERE idEvento = @id";
            using (SqlCommand cmdServ = new SqlCommand(sqlServicios, _connection))
            {
                cmdServ.Parameters.AddWithValue("@id", idEvento);
                cmdServ.ExecuteNonQuery();
            }

            // Luego eliminar el evento
            string sqlEvento = "DELETE FROM EVENTO WHERE idEvento = @id";
            using (SqlCommand cmdEvento = new SqlCommand(sqlEvento, _connection))
            {
                cmdEvento.Parameters.AddWithValue("@id", idEvento);
                cmdEvento.ExecuteNonQuery();
            }

            _connection.Close();
        }

        public Evento BuscarPorId(int id)
        {
            Evento e = null;

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();

            _connection.Open();

            string sql = "SELECT idEvento, Nombre, Estado FROM EVENTO WHERE idEvento = @id";
            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        e = new Evento
                        {
                            idEvento = (int)dr["idEvento"],
                            Nombre = dr["Nombre"].ToString(),
                            Estado = Enum.Parse<EstadoEvento>(dr["Estado"].ToString())
                        };
                    }
                }
            }

            _connection.Close();
            return e;
        }





    }
}
