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
    public partial class cVehiculos : Form
    {
        public cVehiculos()
        {
            InitializeComponent();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            System.Linq.Expressions.Expression<Func<Vehiculo, bool>> filtro = x => true;

            int id, mantTotal;
            switch (filtrarcomboBox.SelectedIndex)
            {
                case 0://ID
                    id = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = x => x.VehiculoId == id;
                    break;
                case 1:// Descripcion
                    filtro = x => x.Descripcion.Contains(CriteriotextBox.Text);
                    break;
                case 2:// Mantenimiento Total
                    mantTotal = Convert.ToInt32(CriteriotextBox.Text);
                    filtro = x => x.MantenimientoTotal == mantTotal;
                    break;
            }
            ConsultadataGridView.DataSource = BLL.VehiculoBLL.GetList(filtro);
        }
    }
}
