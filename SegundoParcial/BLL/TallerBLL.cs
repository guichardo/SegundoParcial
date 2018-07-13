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
    public class TallerBLL
    {
        public static bool Guardar(Taller taller)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.talleres.Add(taller) != null)
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

        public static bool Modificar(Taller taller)
        {

            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(taller).State = EntityState.Modified;
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

                Taller taller = contexto.talleres.Find(id);
                contexto.talleres.Remove(taller);
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


        public static Taller Buscar(int id)
        {

            Taller taller = new Taller();
            Contexto contexto = new Contexto();

            try
            {
                taller = contexto.talleres.Find(id);
                contexto.Dispose();
            }

            catch (Exception)
            {
                throw;
            }

            return taller;
        }


        public static List<Taller> GetList(Expression<Func<Taller, bool>> expression)
        {

            List<Taller> taller = new List<Taller>();
            Contexto contexto = new Contexto();

            try
            {
                taller = contexto.talleres.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return taller;
        }
    }
}
