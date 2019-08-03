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
    public partial class Detalles : Form
    {
        Conexion conexion = new Conexion();
        public Detalles()
        {
            InitializeComponent();
            
        }

        private void Detalles_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = Owner as Form1;
            form.Enabled = true;
        }
    }
}
