using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Colegio
{
    public partial class Form1 : Form
    {
        Conexion conexion = new Conexion();
        public Form1()
        {
            InitializeComponent();
            conexion._comando = new SqlCommand("select * from Categorias",conexion._conexion);
            conexion.CargarComboBox(cboCategoria, conexion._comando, "categoria", "idCategoria");
            conexion._comando = new SqlCommand("select * from Turnos",conexion._conexion);
            conexion.CargarComboBox(cboTurno,conexion._comando, "turno", "id");
            txtTurnoMañana.Text =  conexion.CantPorTurno(0).ToString();
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                Bedel bedel = new Bedel();
                bedel._legajo = Convert.ToInt32(txtLegajo.Text);
                bedel._nombre = txtNombre.Text;
                bedel._apellido = txtApellido.Text;
                bedel._direccion = txtDireccion.Text;
                bedel._idCategoria = Convert.ToInt32(cboCategoria.SelectedValue);
                bedel._sueldoBasico = Convert.ToDouble(txtSueldo.Text);

                Curso curso = new Curso();
                curso._aula = Convert.ToInt32(txtAula.Text);
                curso._bedel = bedel;
                curso._capacidad = Convert.ToInt32(txtCapacidad.Text);
                curso._idTurno = Convert.ToInt32(cboTurno.SelectedValue);

                DialogResult dialogo = MessageBox.Show("Seguro desea registrar? " + "\n" + curso.ToStringCurso(), "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogo == DialogResult.Yes)
                {
                    conexion.RegistrarBedel(bedel._legajo, bedel._nombre, bedel._apellido, bedel._direccion, bedel._sueldoBasico, bedel._idCategoria);
                    MessageBox.Show( conexion.RegistrarCurso(curso._aula, curso._idTurno,curso._capacidad, bedel._legajo));
                    txtTurnoMañana.Text = conexion.CantPorTurno(0).ToString();


                }
            }
            
        }
        private void LimpiarCampos()
        {
            foreach(Control c in groupBox1.Controls)
            {
                if(c is TextBox || c is ComboBox)        
                    c.Text = "";  
            }
            foreach(Control c in groupBox2.Controls)
            {
                if(c is TextBox || c is ComboBox)
                c.Text = "";
            }
        }
        private bool Validar()
        {
            if(txtLegajo.Text == "")
            {
                MessageBox.Show("Error en el campo Legajo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLegajo.Focus();
                return false;
            }
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Error en el campo Nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                return false;
            }
            if (txtApellido.Text == "")
            {
                MessageBox.Show("Error en el campo Apellido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtApellido.Focus();
                return false;
            }
            if (txtDireccion.Text == "")
            {
                MessageBox.Show("Error en el campo Direccion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDireccion.Focus();
                return false;
            }
            if (txtSueldo.Text == "")
            {
                MessageBox.Show("Error en el campo Sueldo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSueldo.Focus();
                return false;
            }
            if (cboCategoria.Text == "")
            {
                MessageBox.Show("Seleccione una categoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboCategoria.Focus();
                return false;
            }
            if (txtAula.Text == "")
            {
                MessageBox.Show("Error en el campo Aula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAula.Focus();
                return false;
            }
            if (txtCapacidad.Text == "")
            {
                MessageBox.Show("Error en el campo Capacidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCapacidad.Focus();
                return false;
            }
            if (cboTurno.Text == "")
            {
                MessageBox.Show("Seleccione un turno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboTurno.Focus();
                return false;
            }
            return true;

        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            Listado listado = new Listado();
            AddOwnedForm(listado);
            listado.Show();
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }



}
