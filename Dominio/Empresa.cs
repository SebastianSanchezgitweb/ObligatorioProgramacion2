using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empresa
    {

        private List<Cliente> clientes;
        private List<Empleado> empleados;
        private List<Evento> eventos;
        private List<Evento> ListaEventosFecha;



        private static Empresa instancia = null;

        public static Empresa Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Empresa();
                }
                return instancia;
            }
        }

        int proximoIdEvento = 1;

        public Empresa()
        {
            eventos = new List<Evento>();
            empleados = new List<Empleado>();
            clientes = new List<Cliente>();
            ListaEventosFecha = new List<Evento>();

            PrecargaEmpleado();
            PrecargarCliente();
        }

        public void AgregarServicioEvento(ServiciosContratados servicio, Evento evento, int idcategoria)
        {

          

            servicio.IDServicios = evento.proximoIDServicio++;  //aumento el id de servicio 
                                                               
            servicio.Categoria = ObtenerCategoriaPorId(idcategoria); // servicio.IdCategoria = idcategoria;
            evento.servicioscontratados.Add(servicio);


        }
        public CategoriaServicio ObtenerCategoriaPorId(int id)
        {            //tipo          //Lista
            foreach (CategoriaServicio cat in CategoriaServicio.ListaCategoriaServicio)
            {
                if (cat.IdCategoria == id)
                {
                    return cat;
                }
            }
            return null;
        }

        //usar operador is, usar casting o conversion explicitas
        //if(evento is Corporativo)
        //{corporativo corp = (corporativo)evt;}
        //elseif(evt is social)

        public bool AgregarEvento(Evento constEvento)//Metodo para agregar 1 evento a la lista eventos
        {

            constEvento.idEvento = proximoIdEvento++; //para que idEvento vaya en incremento
            if (VerificarExisteEvento(constEvento) == false)
            {

                eventos.Add(constEvento);
                return true;
            }
            return false;
        }



        public bool VerificarExisteEvento(Evento constEvento)
        {
            foreach (Evento evento in eventos)
            {
                if (evento.idEvento == constEvento.idEvento)
                {

                    return true;
                }
            }
            return false;
        }


        private int proximoIdCliente = 6;
        public bool AgregarCliente(Cliente constCliente)
        {
            constCliente.IdCliente = proximoIdCliente++;
            if (VerificarSiExisteCliente(constCliente) == false  )
            {
                clientes.Add(constCliente);
                return true;
            }
            return false;
        }

        public bool VerificarSiExisteCliente(Cliente constCliente)
        {
            foreach (Cliente clien in clientes)
            {
                if (clien.Identificacion == constCliente.Identificacion)
                {

                    return true;
                }
            }
            return false;
        }


        public void PrecargarCliente()
        {
            Cliente c1 = new Cliente(1, 123456789, "CI", 091222444, "seba", "barrio ceibal");
            Cliente c2 = new Cliente(2, 20456789, "RUT", 092334455, "Marcos", "Salto Nuevo");
            Cliente c3 = new Cliente(3, 179998881, "CI", 098112233, "Lucía", "Zona Este");
            Cliente c4 = new Cliente(4, 21487301, "RUT", 097556677, "Ana", "Barrio Artigas");
            Cliente c5 = new Cliente(5, 123456789, "CI", 091222444, "Seba", "Barrio Ceibal");

            clientes.Add(c1);
            clientes.Add(c2);
            clientes.Add(c3);
            clientes.Add(c4);
            clientes.Add(c5);
        }

        public void PrecargaEmpleado()
        {
            Empleado e1 = new Empleado(1, "Juan", "Pérez", "45678912", 098765432, "juanp", "pass123");
            Empleado e2 = new Empleado(2, "María", "Gómez", "51234987", 091234567, "mariag", "clave456");
            Empleado e3 = new Empleado(3, "Lucas", "Rodríguez", "47812345", 099876543, "lucasr", "abc789");
            Empleado e4 = new Empleado(4, "Ana", "Fernández", "50123498", 092345678, "anafer", "pass999");
            Empleado e5 = new Empleado(5, "Diego", "Sosa", "44567890", 097112233, "diegos", "qwerty");

            empleados.Add(e1);
            empleados.Add(e2);
            empleados.Add(e3);
            empleados.Add(e4);
            empleados.Add(e5);

        }

        public List<Empleado> ObtenerEmpleados()
        {
            return new List<Empleado>(empleados);
        }


        public List<Evento> ObtenerEventos()
        {
            return new List<Evento>(eventos);
        }

        public List<Cliente> ObtenerCliente()
        {
            return new List<Cliente>(clientes);
        }

        public Cliente ObtenerClientePorId(int idcliente)
        {            //tipo          //Lista
            foreach (Cliente clien in clientes) //recorremos la lista articulos que es de tipo Articulo 
            {
                if (clien.IdCliente == idcliente)
                {
                    return clien;
                }
            }
            return null;
        }





        public EventoCorporativo ObtenerEventoCorporativoPorId(int idEventoCorporativo)
        {            //tipo          //Lista

            foreach (Evento evento in eventos) //recorremos la lista eventos
            {


                if (evento is EventoCorporativo)
                {
                    EventoCorporativo Corporativo = (EventoCorporativo)evento;//casteo


                    if (Corporativo.idEvento == idEventoCorporativo)
                    {
                        return Corporativo;
                    }

                }
            }
            return null;
        }


        public EventoSociales ObtenerEventoSocialesPorId(int idEventoSocial)
        {            //tipo          //Lista
            foreach (Evento evento in eventos) //recorremos la lista eventos
            {
                if (evento is EventoSociales)
                {
                    EventoSociales evenSocial = (EventoSociales)evento;//casteo


                    if (evenSocial.idEvento == idEventoSocial)
                    {
                        return evenSocial;
                    }
                }
            }
            return null;
        }



        public Evento ObtenerEventosPorID(int id)
        {
            foreach (Evento even in eventos)
                if (even.idEvento == id)
                {
                    return even;
                }

            return null;

        }


        public void EditarEvento(Evento evento)
        {
            if (evento is EventoCorporativo)
            {
                EventoCorporativo objEventoCorp = (EventoCorporativo)evento;
                EventoCorporativo EventoCorporativoAEditar = ObtenerEventoCorporativoPorId(evento.idEvento);



                // Validar y actualizar Nombre
                if (!string.IsNullOrEmpty(objEventoCorp.Nombre))
                {
                    EventoCorporativoAEditar.Nombre = objEventoCorp.Nombre;
                }

                // Validar y actualizar NombreEmpresa
                if (!string.IsNullOrEmpty(objEventoCorp.NombreEmpresa))
                {
                    EventoCorporativoAEditar.NombreEmpresa = objEventoCorp.NombreEmpresa;
                }

                // Actualizar RequiereEquipamientoTecnologico (siempre se actualiza, es bool)
                EventoCorporativoAEditar.RequiereEquipamientoTecnologico = objEventoCorp.RequiereEquipamientoTecnologico;

                // Validar y actualizar Ubicacion
                if (!string.IsNullOrEmpty(objEventoCorp.Ubicacion))
                {
                    EventoCorporativoAEditar.Ubicacion = objEventoCorp.Ubicacion;
                }

                // Validar y actualizar FechaRealizacion
                if (objEventoCorp.FechaRealizacion != DateTime.MinValue)
                {
                    EventoCorporativoAEditar.FechaRealizacion = objEventoCorp.FechaRealizacion;
                }
                if (objEventoCorp.FechaFinalizacion != DateTime.MinValue)
                {
                    EventoCorporativoAEditar.FechaFinalizacion = objEventoCorp.FechaFinalizacion;
                }

                // Validar y actualizar Estado
                if (objEventoCorp.Estado != null)
                {
                    EventoCorporativoAEditar.Estado = objEventoCorp.Estado;
                }

                // Validar y actualizar CantidadAsistentes
                if (objEventoCorp.CantidadAsistentes > 0) // Asumiendo que debe ser mayor a 0
                {
                    EventoCorporativoAEditar.CantidadAsistentes = objEventoCorp.CantidadAsistentes;
                }

            }
            else if (evento is EventoSociales)
            {

                EventoSociales objEventoSociales = (EventoSociales)evento;
                EventoSociales EditarEventoSocial = ObtenerEventoSocialesPorId(evento.idEvento);



                // Validar y actualizar Nombre
                if (!string.IsNullOrEmpty(objEventoSociales.Nombre))
                {
                    EditarEventoSocial.Nombre = objEventoSociales.Nombre;
                }

                if (!string.IsNullOrEmpty(objEventoSociales.TipoColaboracion))
                {
                    EditarEventoSocial.TipoColaboracion = objEventoSociales.TipoColaboracion;
                }
                // Validar y actualizar Ubicacion
                if (!string.IsNullOrEmpty(objEventoSociales.Ubicacion))
                {
                    EditarEventoSocial.Ubicacion = objEventoSociales.Ubicacion;
                }

                // Validar y actualizar FechaRealizacion
                if (objEventoSociales.FechaRealizacion != DateTime.MinValue)
                {
                    EditarEventoSocial.FechaRealizacion = objEventoSociales.FechaRealizacion;
                }
                if (objEventoSociales.FechaFinalizacion != DateTime.MinValue)
                {
                    EditarEventoSocial.FechaFinalizacion = objEventoSociales.FechaFinalizacion;
                }

                // Validar y actualizar Estado
                if (objEventoSociales.Estado != null)
                {
                    EditarEventoSocial.Estado = objEventoSociales.Estado;
                }

                // Validar y actualizar CantidadAsistentes
                if (objEventoSociales.CantidadAsistentes != null)
                {
                    EditarEventoSocial.CantidadAsistentes = objEventoSociales.CantidadAsistentes;
                }
                
                EditarEventoSocial.IncluyeCatering = objEventoSociales.IncluyeCatering;

            }
        }

        public void EliminarEvento(int id)
        {
            Evento eve = ObtenerEventosPorID(id);

            if (eve != null)
            {
                eventos.Remove(eve);
            }
        }



        public List<Evento> ListaEventosFiltradosPorFechas(DateTime FechaRealizacion, DateTime FechaFinalizacion, int Monto)
        {
            List<Evento> Lista = new List<Evento>();

            foreach (Evento even in eventos)
            {
                if (even.FechaRealizacion >= FechaRealizacion && even.FechaFinalizacion <= FechaFinalizacion)
                {
                    if (Monto <= even.CalcularCosto())
                    {
                        Lista.Add(even);
                    }
                }
            }

            ListaEventosFecha = Lista;
            return Lista;
        }



        public List<Evento> ObtenerEventosListaFiltradosPorFecha()
        {
            return new List<Evento>(ListaEventosFecha);
        }






        public float PromedioEventoCorporativo()
        {
            float suma = 0;
            float contador = 0;
            foreach (Evento even in eventos )
            {
               if (even is EventoCorporativo)
                {
                    EventoCorporativo Corporativo = (EventoCorporativo)even;//casteo

                    suma += even.CalcularCosto();
                    contador++;
                }
            }

            return suma / contador;
        }
        public float PromedioEventoSociales()
        {
            float suma = 0;
            float contador = 0;
         
            foreach (Evento even in eventos)
            {
                if (even is EventoSociales)
                {
                    EventoSociales Corporativo = (EventoSociales)even;//casteo

                    suma += even.CalcularCosto();
                    contador++;
                }
            }

            return suma / contador;
        }






    }


}
