using SegundoParcial.UI.Consultas;
using SegundoParcial.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SegundoParcial
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ArticulostoolStripButton_Click(object sender, EventArgs e)
        {
            rArticulos registro = new rArticulos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void EntradatoolStripButton_Click(object sender, EventArgs e)
        {
            rEntradaArticulos registro = new rEntradaArticulos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void MantenimientotoolStripButton_Click(object sender, EventArgs e)
        {
            rMantenimiento registro = new rMantenimiento();
            registro.MdiParent = this;
            registro.Show();
        }

        private void TallertoolStripButton_Click(object sender, EventArgs e)
        {
            rTalleres registro = new rTalleres();
            registro.MdiParent = this;
            registro.Show();
        }

        private void VehiculotoolStripButton_Click(object sender, EventArgs e)
        {
            rVehiculos registro = new rVehiculos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void ArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rArticulos registro = new rArticulos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void EntradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rEntradaArticulos registro = new rEntradaArticulos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void mantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rMantenimiento registro = new rMantenimiento();
            registro.MdiParent = this;
            registro.Show();
        }

        private void tallerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTalleres registro = new rTalleres();
            registro.MdiParent = this;
            registro.Show();
        }

        private void vehiculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rVehiculos registro = new rVehiculos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void articulosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cArticulos consulta = new cArticulos();
            consulta.MdiParent = this;
            consulta.Show();
        }

        private void vehiculosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cVehiculos consulta = new cVehiculos();
            consulta.MdiParent = this;
            consulta.Show();
        }
    }
}
