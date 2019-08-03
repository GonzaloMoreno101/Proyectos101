using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ViveroFenix
{
    public partial class Form1 : Form
    {
        Conexion conexion = new Conexion();
        public Form1()
        {
            InitializeComponent();
            lblCantRegPlantas.Text =  conexion.CargarDataGrid(dgvPlanta, "spListarPlantas").ToString();
            lblCantRegVentas.Text = conexion.CargarDataGrid(dgvVenta, "spListarVentas").ToString();
            conexion.ProductosPreferidos(chartProdPreferidos);
            
            lblTotalVentas.Text = "$" + "" +conexion.TotalVentas().ToString();
            conexion.DashBoard(lblCantProd, lblCantClientes, lblCantVentas, lblCantProdVed);
            
            conexion.MejoresClientes(chPlantasPorTipo);

        }

        private void DgvVenta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvVenta.SelectedRows.Count == 1)
            {
                Detalles det = new Detalles();
                det.Show();
                AddOwnedForm(det);
                this.Enabled = false;
                det.txtCantProd.Text = conexion.CargarDetalles(det.dgvDetalles, Convert.ToInt32(dgvVenta.CurrentRow.Cells[0].Value)).ToString();
                det.txtTotal.Text = conexion.PrecioTotalFactura(Convert.ToInt32(dgvVenta.CurrentRow.Cells[0].Value)).ToString();
                det.txtFecha.Text = Convert.ToDateTime(dgvVenta.CurrentRow.Cells[1].Value).ToShortDateString();
                det.txtFormaPago.Text = dgvVenta.CurrentRow.Cells[2].Value.ToString();
                det.txtNombre.Text = dgvVenta.CurrentRow.Cells[3].Value.ToString();
                det.txtNroFact.Text = dgvVenta.CurrentRow.Cells[0].Value.ToString();
            }
            
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
          conexion.BuscarPor(rdbNombre, rdbId, rdbTipo, txtBusqueda.Text,dgvPlanta, lblCantRegPlantas);
        }

        private void TxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            conexion.BuscarPor(rdbNombre, rdbId, rdbTipo, txtBusqueda.Text, dgvPlanta, lblCantRegPlantas);
            if(txtBusqueda.Text == " ")
            {
                lblCantRegPlantas.Text = conexion.CargarDataGrid(dgvPlanta, "spListarPlantas").ToString();
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            Registrar_Planta registrar = new Registrar_Planta();
            AddOwnedForm(registrar);
            registrar.Show();
            this.Enabled = false;
           
            
        }

        private void DgvPlanta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Registrar_Planta registrar = new Registrar_Planta();
            AddOwnedForm(registrar);
            registrar.Show();
            this.Enabled = false;
            registrar.txtCodigo.Text = dgvPlanta.CurrentRow.Cells[0].Value.ToString();
            registrar.txtDesc.Text = dgvPlanta.CurrentRow.Cells[1].Value.ToString();
            registrar.txtPrecio.Text = dgvPlanta.CurrentRow.Cells[3].Value.ToString();
            registrar.txtStock.Text = dgvPlanta.CurrentRow.Cells[4].Value.ToString();
            registrar.cboTipo.SelectedValue = conexion.ValorMiembro(dgvPlanta.CurrentRow.Cells[2].Value.ToString());
            registrar.actualizar = true;
            registrar.txtCodigo.Enabled = false;
        }

        private void BtnNuevaVenta_Click(object sender, EventArgs e)
        {
            Nueva_Venta venta = new Nueva_Venta();
            AddOwnedForm(venta);
            venta.Show();
            this.Enabled = false;
        }
    }
}
