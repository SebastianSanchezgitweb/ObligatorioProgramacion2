using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CategoriaServicio
    {

        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        

        public static List<CategoriaServicio> ListaCategoriaServicio { get; set; }
        static CategoriaServicio(){

            ListaCategoriaServicio = new List<CategoriaServicio>();
            CategoriaServicio cat1 = new CategoriaServicio(1,"Decoracion");
            CategoriaServicio cat2 = new CategoriaServicio(2, "Fotografia");

            CategoriaServicio cat3 = new CategoriaServicio(3, "Comida");

            ListaCategoriaServicio.Add(cat1);
            ListaCategoriaServicio.Add(cat2);
            ListaCategoriaServicio.Add(cat3);




        }


       
        public CategoriaServicio(int idcategoria, string nombreCategoria)
        {
               IdCategoria = idcategoria;
            NombreCategoria = nombreCategoria;
        }



    }

         
        
    
}
