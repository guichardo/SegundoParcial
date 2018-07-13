using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.Entidades
{
    public class EntradaArticulos
    {
        [Key]

        public int EntradaId { get; set; }
        public string Fecha { get; set; }
        public int Cantidad { get; set; }
        public int ArticuloId { get; set; }

        public EntradaArticulos(){ }

        public override string ToString()
        {
            return this.Fecha;
        }
    }
}
