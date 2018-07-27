using SegundoParcial.DAL;
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

namespace SegundoParcial.UI.Registros
{
    public partial class rMantenimiento : Form
    {
        public rMantenimiento()
        {
            InitializeComponent();
            LlenarComboBox();
        }

        decimal itbis = 0;
        decimal importe = 0;
        decimal Total = 0;
        decimal subtotal = 0;

        public void Vaciar()
        {
            IdnumericUpDown.Value = 0;
            FechadateTimePicker.Value = DateTime.Now;
            PFechadateTimePicker.Value = DateTime.Now;
            CantidadNumericUpDown.Value = 0;
            TotalTextBox.Clear();
            ImporteTextBox.Clear();
            SubTextBox.Text = 0.ToString();
            TotalTextBox.Text = 0.ToString();
            ItbisTextBox.Text = 0.ToString();
            MantenimientoDataGridView.DataSource = null;
            itbis = 0;
            importe = 0;
            Total = 0;
            subtotal = 0;
        }

        private int ToInt(object valor)
        {
            int retorno = 0;
            int.TryParse(valor.ToString(), out retorno);
            return retorno;
        }

        private Mantenimiento LlenaClase()
        {
            Mantenimiento mantenimiento = new Mantenimiento();

            mantenimiento.MantenimientoId = Convert.ToInt32(IdnumericUpDown.Value);
            mantenimiento.VehiculoId = Convert.ToInt32(VehiculocomboBox.SelectedValue);
            mantenimiento.Fecha = FechadateTimePicker.Value;
            mantenimiento.PFecha = PFechadateTimePicker.Value;
            mantenimiento.Subtotal = Convert.ToDecimal(SubTextBox.Text);
            mantenimiento.itbis = Convert.ToDecimal(ItbisTextBox.Text);
            mantenimiento.Total = Convert.ToDecimal(TotalTextBox.Text);

            MantenimientoDataGridView.Columns["MantenimientoId"].Visible = false;
            MantenimientoDataGridView.Columns["Id"].Visible = false;
            MantenimientoDataGridView.Columns["MantenimientoId"].Visible = false;
            MantenimientoDataGridView.Columns["TallerId"].Visible = false;
            MantenimientoDataGridView.Columns["ArticulosId"].Visible = false;
            MantenimientoDataGridView.Columns["articulo"].Visible = false;


            foreach (DataGridViewRow item in MantenimientoDataGridView.Rows)
            {
                mantenimiento.AgregarDetalle(ToInt(item.Cells["id"].Value),
                mantenimiento.MantenimientoId, ToInt(item.Cells["tallerId"].Value),
                ToInt(item.Cells["articulosId"].Value), 
                Convert.ToString(item.Cells["articulo"].Value),
                ToInt(item.Cells["cantidad"].Value), 
                ToInt(item.Cells["precio"].Value),
                ToInt(item.Cells["importe"].Value));
            }
            return mantenimiento;
        }
        private void LlenarCampos(Mantenimiento mantenimiento)
        {
            Vaciar();
            IdnumericUpDown.Value = mantenimiento.MantenimientoId;
            FechadateTimePicker.Value = mantenimiento.Fecha;
            PFechadateTimePicker.Value = mantenimiento.PFecha;
            SubTextBox.Text = mantenimiento.Subtotal.ToString();
            ItbisTextBox.Text = mantenimiento.itbis.ToString();
            TotalTextBox.Text = mantenimiento.Total.ToString();

            MantenimientoDataGridView.DataSource = mantenimiento.Detalle;

            MantenimientoDataGridView.Columns["Id"].Visible = false;
            MantenimientoDataGridView.Columns["MantenimientoId"].Visible = false;
            MantenimientoDataGridView.Columns["TallerId"].Visible = false;
            MantenimientoDataGridView.Columns["ArticulosId"].Visible = false;
        }

        private void LlenarComboBox()
        {
            Repositorio<Vehiculo> vehiculo = new Repositorio<Vehiculo>(new Contexto());
            VehiculocomboBox.DataSource = vehiculo.GetList(c => true);
            VehiculocomboBox.ValueMember = "VehiculoId";
            VehiculocomboBox.DisplayMember = "Descripcion";

            Repositorio<Taller> talleres = new Repositorio<Taller>(new Contexto());
            TallercomboBox.DataSource = talleres.GetList(c => true);
            TallercomboBox.ValueMember = "TallerId";
            TallercomboBox.DisplayMember = "Nombre";

            Repositorio<Articulos> Entrada = new Repositorio<Articulos>(new Contexto());
            ArticuloComboBox.DataSource = Entrada.GetList(c => true);
            ArticuloComboBox.ValueMember = "ArticuloId";
            ArticuloComboBox.DisplayMember = "Descripcion";
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(IdnumericUpDown.Value);
            Mantenimiento mantenimiento = BLL.MantenimientoBLL.Buscar(id);

            if (mantenimiento != null)
            {
                LlenarCampos(mantenimiento);
            }
            else
                MessageBox.Show("No se encontro!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Vaciar();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {

            Mantenimiento mantenimiento = LlenaClase();
            bool Paso = false;

            if (IdnumericUpDown.Value == 0)
            {
                Paso = BLL.MantenimientoBLL.Guardar(mantenimiento);
            }
            else
            {
                Paso = BLL.MantenimientoBLL.Modificar(mantenimiento);
            }

            if (Paso)
            {
                Vaciar();
                MessageBox.Show("Guardado!!", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No se pudo guardar!!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Por favor llenar los campos!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int id = Convert.ToInt32(IdnumericUpDown.Value);
            if (BLL.MantenimientoBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Vaciar();
            }
            else
                MessageBox.Show("No se pudo eliminar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void CantidadNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ImporteTextBox.Text = BLL.MantenimientoBLL.CalcularImporte(Convert.ToInt32(PrecioTextBox.Text), Convert.ToInt32(CantidadNumericUpDown.Value)).ToString();
        }

        private void ArticuloComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in BLL.ArticulosBLL.GetList(x => x.Descripcion == ArticuloComboBox.Text))
            {
                PrecioTextBox.Text = item.Precio.ToString();
            }
        }

        private void rMantenimiento_Load(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(PFechadateTimePicker.Text);
            fecha = fecha.AddMonths(3);

            PFechadateTimePicker.Text = fecha.ToString();
        }

        private void AgregarButton_Click(object sender, EventArgs e)
        {
            List<MantenimientoDetalle> detalle = new List<MantenimientoDetalle>();
            Mantenimiento mantenimiento = new Mantenimiento();


            if (MantenimientoDataGridView.DataSource != null)
            {
                mantenimiento.Detalle = (List<MantenimientoDetalle>)MantenimientoDataGridView.DataSource;
            }

            
            foreach (var item in BLL.ArticulosBLL.GetList(x => x.Inventario < CantidadNumericUpDown.Value))
            {

                MessageBox.Show("No hay existencia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
}
    

            if (string.IsNullOrEmpty(ImporteTextBox.Text))
            {
                MessageBox.Show("Favor digitar una cantidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                mantenimiento.Detalle.Add(
                    new MantenimientoDetalle(id: 0,
                    mantenimientoId: (int)Convert.ToInt32(IdnumericUpDown.Value),
                    tallerId: (int)TallercomboBox.SelectedValue,
                    articulosId: (int)ArticuloComboBox.SelectedValue,
                    articulo: (string)BLL.ArticulosBLL.MostrarDescripcion(ArticuloComboBox.Text),
                    cantidad: Convert.ToInt32(CantidadNumericUpDown.Value),
                    precio: Convert.ToInt32(PrecioTextBox.Text),
                    importe: Convert.ToInt32(ImporteTextBox.Text)));


                MantenimientoDataGridView.DataSource = null;
                MantenimientoDataGridView.DataSource = mantenimiento.Detalle;

                MantenimientoDataGridView.Columns["Id"].Visible = false;
                MantenimientoDataGridView.Columns["TallerId"].Visible = false;
                MantenimientoDataGridView.Columns["ArticulosId"].Visible = false;
                MantenimientoDataGridView.Columns["Articulo"].Visible = false;
            }

            importe += BLL.MantenimientoBLL.CalcularImporte(Convert.ToDecimal(PrecioTextBox.Text), Convert.ToInt32(CantidadNumericUpDown.Value));

            if (IdnumericUpDown.Value != 0)
            {

                subtotal += importe;
                SubTextBox.Text = subtotal.ToString();
            }
            else
            {

                subtotal = importe;
                SubTextBox.Text = subtotal.ToString();
            }

            itbis = BLL.MantenimientoBLL.CalcularItbis(Convert.ToDecimal(SubTextBox.Text));

            ItbisTextBox.Text = itbis.ToString();

            Total = BLL.MantenimientoBLL.Total(Convert.ToDecimal(SubTextBox.Text), Convert.ToDecimal(ItbisTextBox.Text));

            TotalTextBox.Text = Total.ToString();
        }
    }
}
