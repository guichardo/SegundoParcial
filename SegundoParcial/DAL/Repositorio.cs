﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.DAL
{
    public class Repositorio<T> : IDisposable, IRepository<T> where T : class
    {
        internal Contexto _contexto;

        public Repositorio(Contexto contexto)
        {
            _contexto = contexto;
        }


        public bool Guardar(T entity)
        {
            bool paso = false;

            try
            {
                if (_contexto.Set<T>().Add(entity) != null)
                {
                    _contexto.SaveChanges();
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }


        public bool Modificar(T entity)
        {
            bool paso = false;
            try
            {
                _contexto.Entry(entity).State = EntityState.Modified;
                if (_contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public bool Eliminar(int id)
        {
            bool paso = false;

            try
            {
                T entity = _contexto.Set<T>().Find(id);

                _contexto.Set<T>().Remove(entity);

                if (_contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                _contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }


        public T Buscar(int id)
        {
            T entity;
            try
            {
                entity = _contexto.Set<T>().Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }


        public List<T> GetList(Expression<Func<T, bool>> expression)
        {
            List<T> Lista = new List<T>();


            Lista = _contexto.Set<T>().Where(expression).ToList();


            return Lista;
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
