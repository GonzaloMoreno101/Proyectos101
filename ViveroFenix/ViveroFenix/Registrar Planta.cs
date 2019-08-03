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
    public partial class Registrar_Planta : Form
    {
        Conexion conexion = new Conexion();
        public bool actualizar;
        public Registrar_Planta()
        {
            InitializeComponent();
            conexion.CargarCombo(cboTipo, "spListarTipos");
            
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            Form1 form = Owner as Form1;
            if (Validar())
            {
                Planta planta = new Planta();
                planta._codigo = Convert.ToInt32(txtCodigo.Text);
                planta._descripcion = txtDesc.Text;
                planta._idTipo = Convert.ToInt32(cboTipo.SelectedValue);
                planta._precio = Convert.ToDouble(txtPrecio.Text);
                planta._stock = Convert.ToInt32(txtStock.Text);
                if(actualizar)
                {
                    MessageBox.Show(conexion.ActualizarPlanta(planta._idTipo, planta._descripcion, planta._precio, planta._idTipo, planta._stock));
                    actualizar = false;
                    conexion.ProductosPreferidos(form.chartProdPreferidos);
                    form.lblTotalVentas.Text = "$" + "" + conexion.TotalVentas().ToString();
                    conexion.DashBoard(form.lblCantProd, form.lblCantClientes, form.lblCantVentas, form.lblCantProdVed);
                    conexion.PlantasPorTipo(form.chPlantasPorTipo);
                    this.Close();
                }
                else
                {
                    DialogResult dialogo = MessageBox.Show("Seguro desea registrar? " + planta.ToStringPlanta(cboTipo.Text), "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogo == DialogResult.Yes)
                    {
                        MessageBox.Show(conexion.CargarPlanta(planta._codigo, planta._descripcion, planta._idTipo, planta._precio, planta._stock));
                        conexion.ProductosPreferidos(form.chartProdPreferidos);
                        form.lblTotalVentas.Text = "$" + "" + conexion.TotalVentas().ToString();
                        conexion.DashBoard(form.lblCantProd, form.lblCantClientes, form.lblCantVentas, form.lblCantProdVed);
                        conexion.PlantasPorTipo(form.chPlantasPorTipo);
                    }
                }
                
            }
        }

        public void LimpiarCampos()
        {
            foreach (Control c in this.Controls)
            {
                if(c is TextBox)
                {
                    c.Text = "";
                }
            }
        }

        public bool Validar()
        {
            foreach(Control c in this.Controls)
            {
                if((c is TextBox || c is ComboBox) && c.Text == "")
                {
                    MessageBox.Show("Complete todos los campos");
                    return false;
                }
                

            }
            return true;
        }

        private void Registrar_Planta_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = Owner as Form1;
            form.lblCantRegPlantas.Text = conexion.CargarDataGrid(form.dgvPlanta, "spListarPlantas").ToString();
            form.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
