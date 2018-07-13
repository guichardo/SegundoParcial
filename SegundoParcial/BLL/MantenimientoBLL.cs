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
    public class MantenimientoBLL
    {

        public static decimal CalcularImporte(decimal precio, int cantidad)
        {
            return Convert.ToDecimal(precio) * Convert.ToInt32(cantidad);
        }

        public static decimal CalcularItbis(decimal subtotal)
        {
            return subtotal * Convert.ToDecimal(0.18);
        }

        public static decimal Total(decimal subtotal, decimal itbis)
        {
            return subtotal + itbis;
        }

        public static bool Guardar(Mantenimiento mantenimiento)
        {
            bool paso = false;
            Vehiculo vehiculos = new Vehiculo();
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.mantenimiento.Add(mantenimiento) != null)
                {
                    foreach (var item in mantenimiento.Detalle)
                    {
                        contexto.articulos.Find(item.ArticulosId).Inventario -= item.Cantidad;
                    }
                    contexto.vehiculos.Find(mantenimiento.VehiculoId).MantenimientoTotal += Convert.ToInt32(mantenimiento.Total);

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

        public static bool Modificar(Mantenimiento mantenimiento)
        {

            bool paso = false;

            Contexto contexto = new Contexto();
            var Mantenimiento = BLL.MantenimientoBLL.Buscar(mantenimiento.MantenimientoId);
            try
            {
                if (Mantenimiento != null)
                {


                    foreach (var item in Mantenimiento.Detalle)
                    {

                        contexto.articulos.Find(item.ArticulosId).Inventario += item.Cantidad;


                        if (!mantenimiento.Detalle.ToList().Exists(v => v.Id == item.Id))
                        {
                            contexto.entradas.Find(item.ArticulosId).Cantidad -= item.Cantidad;

                            item.articulos = null;
                            contexto.Entry(item).State = EntityState.Deleted;
                        }
                    }

                    foreach (var item in mantenimiento.Detalle)
                    {
                        var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                        contexto.Entry(item).State = estado;
                    }

                    contexto.Entry(mantenimiento).State = EntityState.Modified;

                    if (contexto.SaveChanges() > 0)
                    {
                        paso = true;
                    }
                    contexto.Dispose();
                }
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

                Mantenimiento mantenimiento = contexto.mantenimiento.Find(id);

                contexto.mantenimiento.Remove(mantenimiento);
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

        public static Mantenimiento Buscar(int id)
        {

            Mantenimiento mantenimiento = new Mantenimiento();
            Contexto contexto = new Contexto();

            try
            {
                mantenimiento = contexto.mantenimiento.Find(id);
                mantenimiento.Detalle.Count();

                foreach (var item in mantenimiento.Detalle)
                {
                    string s = item.articulos.Descripcion;
                }
                contexto.Dispose();
            }

            catch (Exception)
            {

                throw;

            }

            return mantenimiento;

        }

        public static List<Mantenimiento> GetList(Expression<Func<Mantenimiento, bool>> expression)
        {

            List<Mantenimiento> Mantenimiento = new List<Mantenimiento>();
            Contexto contexto = new Contexto();

            try
            {

                Mantenimiento = contexto.mantenimiento.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {

                throw;
            }

            return Mantenimiento;
        }
    }
}
