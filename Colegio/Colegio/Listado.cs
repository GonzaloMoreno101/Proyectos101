using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colegio
{
    public partial class Listado : Form
    {
        Conexion conexion = new Conexion();
        public Listado()
        {
            InitializeComponent();
            conexion.CargarDataGrid(dgvListado);
        }

        
    }
}
