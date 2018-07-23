using SegundoParcial.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SegundoParcial.UI.Consultas
{
    public partial class cArticulos : Form
    {
        public cArticulos()
        {
            InitializeComponent();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            System.Linq.Expressions.Expression<Func<Articulos, bool>> filtro = x => true;

            int id, inv, cost, gan, pre;
            switch (filtrarcomboBox.SelectedIndex)
            {
                case 0://ID
                    id = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = x => x.ArticuloId == id;
                    break;
                case 1:// Descripcion
                    filtro = x => x.Descripcion.Contains(CriteriotextBox.Text);
                    break;
                case 2:// Inventario
                    inv = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = x => x.Inventario == inv;
                    break;
                case 3:// costo
                    cost = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = x => x.Costo == cost;
                    break;
                case 4:// ganacia
                    gan = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = x => x.Ganancia == gan;
                    break;
                case 5:// Precio
                    pre = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = x => x.Precio == pre;
                    break;
            }


            ConsultadataGridView.DataSource = BLL.ArticulosBLL.GetList(filtro);
        }
    }
}
