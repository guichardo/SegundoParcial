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
    public partial class rEntradaArticulos : Form
    {
        public rEntradaArticulos()
        {
            InitializeComponent();
            LlenarComboBox();
        }

        private EntradaArticulos LlenarClase()
        {

            EntradaArticulos entrada = new EntradaArticulos();

            entrada.EntradaId = Convert.ToInt32(IdnumericUpDown.Value);
            entrada.ArticuloId = (int)ArticulocomboBox.SelectedValue;
            entrada.Fecha = FechadateTimePicker.Text;
            entrada.Cantidad = Convert.ToInt32(CantidadnumericUpDown.Value);

            return entrada;
        }

        private EntradaArticulos Vaciar()
        {
            EntradaArticulos entrada = new EntradaArticulos();
            IdnumericUpDown.Value = 0;
            CantidadnumericUpDown.Value = 0;

            return entrada;
        }

        private void LlenarComboBox()
        {
            Repositorio<Articulos> repositorio = new Repositorio<Articulos>(new Contexto());
            ArticulocomboBox.DataSource = repositorio.GetList(c => true);
            ArticulocomboBox.ValueMember = "ArticuloId";
            ArticulocomboBox.DisplayMember = "Descripcion";
        }

        private bool Validar(int validar)
        {

            bool paso = false;

            if (validar == 1 && IdnumericUpDown.Value == 0)
            {
                MyerrorProvider.SetError(IdnumericUpDown, "Ingrese un ID");
                paso = true;

            }

            if (validar == 1 && CantidadnumericUpDown.Value == 0)
            {
                MyerrorProvider.SetError(CantidadnumericUpDown, "Ingrese una cantidad");
                paso = true;

            }
            return paso;
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            MyerrorProvider.Clear();

            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
                return;
            }

            int id = Convert.ToInt32(IdnumericUpDown.Value);
            EntradaArticulos entrada = BLL.EntradaArticulosBLL.Buscar(id);

            if (entrada != null)
            {
                FechadateTimePicker.Text = entrada.Fecha;
                ArticulocomboBox.SelectedValue = entrada.ArticuloId;
                CantidadnumericUpDown.Value = Convert.ToDecimal(entrada.Cantidad);
            }
            else
                MessageBox.Show("No se encontro", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Vaciar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            if (Validar(2))
            {

                MessageBox.Show("Llenar todos los campos marcados");
                return;
            }

            MyerrorProvider.Clear();


            if (IdnumericUpDown.Value == 0)
                paso = BLL.EntradaArticulosBLL.Guardar(LlenarClase());
            else
                paso = BLL.EntradaArticulosBLL.Modificar(LlenarClase());


            if (paso)

                MessageBox.Show("Guardado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo guardar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Vaciar();
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            MyerrorProvider.Clear();

            if (Validar(1))
            {
                MessageBox.Show("Ingrese un ID");
                return;
            }

            int id = Convert.ToInt32(IdnumericUpDown.Value);

            if (BLL.EntradaArticulosBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo eliminar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
