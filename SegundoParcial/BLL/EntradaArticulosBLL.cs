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
    public class EntradaArticulosBLL
    {

        public static bool Guardar(EntradaArticulos entrada)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.entradas.Add(entrada) != null)
                {
                    Articulos articulo = BLL.ArticulosBLL.Buscar(entrada.ArticuloId);
                    articulo.Inventario += entrada.Cantidad;
                    BLL.ArticulosBLL.Modificar(articulo);

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

        public static bool Modificar(EntradaArticulos entrada)
        {

            bool paso = false;

            Contexto contexto = new Contexto();

            EntradaArticulos EntradaAnterior = BLL.EntradaArticulosBLL.Buscar(entrada.EntradaId);

            int diferencia;

            diferencia = entrada.Cantidad - EntradaAnterior.Cantidad;

            Articulos articulo = BLL.ArticulosBLL.Buscar(entrada.ArticuloId);
            articulo.Inventario += diferencia;
            BLL.ArticulosBLL.Modificar(articulo);

            try
            {
                contexto.Entry(entrada).State = EntityState.Modified;

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

                EntradaArticulos entrada = contexto.entradas.Find(id);

                Articulos articulo = BLL.ArticulosBLL.Buscar(entrada.ArticuloId);
                articulo.Inventario -= entrada.Cantidad;
                BLL.ArticulosBLL.Modificar(articulo);

                contexto.entradas.Remove(entrada);
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


        public static EntradaArticulos Buscar(int id)
        {

            EntradaArticulos entrada = new EntradaArticulos();
            Contexto contexto = new Contexto();

            try
            {
                entrada = contexto.entradas.Find(id);
                contexto.Dispose();
            }

            catch (Exception)
            {
                throw;
            }

            return entrada;

        }


        public static List<EntradaArticulos> GetList(Expression<Func<EntradaArticulos, bool>> expression)
        {

            List<EntradaArticulos> entrada = new List<EntradaArticulos>();
            Contexto contexto = new Contexto();

            try
            {
                entrada = contexto.entradas.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return entrada;
        }
    }
}
