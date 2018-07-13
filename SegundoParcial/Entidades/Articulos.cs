using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.Entidades
{
    public class Articulos
    {
        [Key]
        public int ArticuloId { get; set; }
        public string Descripcion { get; set; }
        public float Costo { get; set; }
        public float Ganancia { get; set; }
        public float Precio { get; set; }
        public int Inventario { get; set; }

        public Articulos(){ }

        public override string ToString()
        {
            return this.Descripcion;
        }

    }
}
