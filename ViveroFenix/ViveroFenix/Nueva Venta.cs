using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViveroFenix
{
    public partial class Nueva_Venta : Form
    {
        
        Conexion conexion = new Conexion();
        int idDet;
        DataTable tabla = new DataTable();
        DataColumn columna;
        DataRow fila;
       
        public Nueva_Venta()
        {
            InitializeComponent();
            txtCodigo.Text = conexion.ObtenerIdFactura().ToString();
            idDet = conexion.ObtenerIdDetalle();
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.Int32");
            columna.ColumnName = "Cantidad";
            tabla.Columns.Add(columna);
            idDet = conexion.ObtenerIdDetalle();
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.Int32");
            columna.ColumnName = "Detalle";
            tabla.Columns.Add(columna);
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.Int32");
            columna.ColumnName = "ID";
            tabla.Columns.Add(columna);
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            tabla.Columns.Add(columna);
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.Int32");
            columna.ColumnName = "Precio";
            tabla.Columns.Add(columna);
            dgvProdSelec.DataSource = tabla;
            conexion.CargarDataGrid(dgvProdDisp, "spListarPlantas");
            txtFecha.Text = DateTime.Today.ToShortDateString();
            conexion.CargarCombo(cboFormaPago, "spListarFormasPago");
            conexion.CargarCombo(cboCliente, "spListarClientes");
            
        }

      

        private void Nueva_Venta_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = Owner as Form1;
            form.Enabled = true;
        }
       
        public void DgvProdDisp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if(txtCantidadProd.Text != "")
            {
                double total = 0;
                int cantidad = 0;
                fila = tabla.NewRow();
                fila["ID"] = dgvProdDisp.CurrentRow.Cells[0].Value;
                fila["Nombre"] = dgvProdDisp.CurrentRow.Cells[1].Value.ToString();
                fila["Detalle"] = idDet;
                fila["Precio"] = dgvProdDisp.CurrentRow.Cells[3].Value;
                fila["Cantidad"] = txtCantidadProd.Text;

                tabla.Rows.Add(fila);
                dgvProdSelec.DataSource = tabla;
                foreach (DataGridViewRow row in dgvProdSelec.Rows)
                {
                    total += Convert.ToDouble(Convert.ToDouble( row.Cells[4].Value) * Convert.ToInt32(row.Cells[0].Value));
                    cantidad++;
                }
                txtTotal.Text = "$ " + total.ToString();
                txtCantidad.Text = (cantidad - 1).ToString();
                txtCantidadProd.Text = "";
                idDet++;
            }
            else
            {
                MessageBox.Show("Indique la cantidad");
                txtCantidadProd.Focus();
            }
        }
            
              
        

        private void BtnNuevaVenta_Click(object sender, EventArgs e)
        {
            Form1 form = Owner as Form1;
            try
            {
                conexion.RegistrarVenta(Convert.ToInt32(txtCodigo.Text), Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(cboFormaPago.SelectedValue), Convert.ToInt32(cboCliente.SelectedValue));
                foreach (DataGridViewRow row in dgvProdSelec.Rows)
                {
                    conexion.RegistrarDetalle(Convert.ToInt32(row.Cells[1].Value), Convert.ToInt32(row.Cells[2].Value),
                         Convert.ToInt32(row.Cells[0].Value), Convert.ToInt32(txtCodigo.Text));
                }
                form.lblCantRegVentas.Text = conexion.CargarDataGrid(form.dgvVenta, "spListarVentas").ToString();
                MessageBox.Show("La Venta Se Registro Correctamente");
                txtCodigo.Text = conexion.ObtenerIdFactura().ToString();
                conexion.MejoresClientes(form.chPlantasPorTipo);
                conexion.ProductosPreferidos(form.chartProdPreferidos);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error de registro" + error.Message);
            }
            
            
        }

        private void Nueva_Venta_Load(object sender, EventArgs e)
        {

        }
    }
}
