using SegundoParcial.DAL;
using SegundoParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.BLL
{
    public class ArticulosBLL
    {
        public static bool Guardar(Articulos articulo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.articulos.Add(articulo) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }

                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static bool Modificar(Articulos articulo)
        {

            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(articulo).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;

                }

                contexto.Dispose();

            }

            catch (Exception)
            {

                throw;

            }

            return paso;



        }


        public static bool Eliminar(int id)
        {

            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {

                Articulos articulo = contexto.articulos.Find(id);
                contexto.articulos.Remove(articulo);
                if (contexto.SaveChanges() > 0)
                {

                    paso = true;

                }

                contexto.Dispose();

            }

            catch (Exception)
            {

                throw;

            }

            return paso;

        }


        public static Articulos Buscar(int id)
        {

            Articulos articulo = new Articulos();
            Contexto contexto = new Contexto();

            try
            {
                articulo = contexto.articulos.Find(id);
                contexto.Dispose();

            }

            catch (Exception)
            {

                throw;

            }

            return articulo;

        }


        public static List<Articulos> GetList(Expression<Func<Articulos, bool>> expression)
        {

            List<Articulos> articulo = new List<Articulos>();
            Contexto contexto = new Contexto();

            try
            {

                articulo = contexto.articulos.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {

                throw;
            }

            return articulo;
        }

        public static string MostrarDescripcion(string Nombre)
        {
            string Descripcion = string.Empty;
            var lista = GetList(x => x.Descripcion.Equals(Nombre));
            foreach (var item in lista)
            {
                Descripcion = item.Descripcion;
            }

            return Descripcion;
        }
        public static decimal CalcularCosto(decimal Ganancia, decimal precio)
        {
            Ganancia /= 100;
            return Convert.ToDecimal(precio) * Convert.ToDecimal(Ganancia);

        }

        public static decimal CalcularGanancia(decimal Costo, decimal Precio)
        {
            Precio -= Costo;
            Decimal totalganancia = (Convert.ToDecimal(Precio) / Convert.ToDecimal(Costo)) * 100;

            return totalganancia;

        }

        public static decimal CalcularPrecio(decimal Costo, decimal Ganancia)
        {
            Ganancia /= 100;
            Ganancia *= Costo;
            return Convert.ToDecimal(Costo) + Convert.ToDecimal(Ganancia);

        }
    }
}
