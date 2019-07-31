using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Colegio
{
    class Conexion
    {
        SqlCommand comando;
        SqlDataAdapter adaptador;
        SqlConnection conexion;
        DataTable tabla;
        string cadenaConexion;

        public SqlCommand _comando
        {
            set { comando = value; }
            get { return comando; }
        }

        public SqlDataAdapter _adaptador
        {
            set { adaptador = value; }
            get { return adaptador; }
        }

        public SqlConnection _conexion
        {
            set { conexion = value; }
            get { return conexion; }
        }

        public DataTable _tabla
        {
            set { tabla = value; }
            get { return tabla; }
        }

        public string _cadenaConexion
        {
            set { cadenaConexion = value; }
            get { return cadenaConexion; }
        }

        public Conexion()
        {
            cadenaConexion = "Data Source=USUARIO-PC\\SQLEXPRESS;Initial Catalog=Colegio2;Integrated Security=True";
            conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error de conexion: " + e.Message); 
            }
            conexion.Close();
        }

        public string RegistrarBedel(int idBedel,string nombre,string apellido,string direccion,double sueldo,int idCategoria)
        {
            string salida = "";
            conexion.Open();
            comando = new SqlCommand("insert into Bedeles(id,nombre,apellido,direccion,sueldoBasico,idCategoria) " +
                "values(@idBedel,@nombre,@apellido,@direccion,@sueldo,@idCategoria) ",conexion);
            comando.Parameters.AddWithValue("nombre", nombre);
            comando.Parameters.AddWithValue("apellido", apellido);
            comando.Parameters.AddWithValue("direccion", direccion);
            comando.Parameters.AddWithValue("sueldo", sueldo);
            comando.Parameters.AddWithValue("idCategoria", idCategoria);
            comando.Parameters.AddWithValue("idBedel", idBedel);
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                salida = "Error al registrar El Bedel" + e.Message;
            }
            conexion.Close();
            return salida;

        }

        public string RegistrarCurso(int aula,int turno,int capacidad,int idBedel)
        {
            conexion.Open();
            string salida;
            comando = new SqlCommand("insert into Cursos(aula,idTurno,capacidad,idBedel) values(@aula,@turno,@capacidad,@idBedel)", conexion);
            comando.Parameters.AddWithValue("aula", aula);
            comando.Parameters.AddWithValue("turno", turno);
            comando.Parameters.AddWithValue("capacidad", capacidad);
            comando.Parameters.AddWithValue("idBedel", idBedel);
            try
            {
                comando.ExecuteNonQuery();
                salida = "Registro realizado correctamente";
            }
            catch (Exception e)
            {
                salida = "Error al registrar el curso" + e.Message;
            }
            conexion.Close();
            return salida;
        }

        public void CargarComboBox(ComboBox cbo, SqlCommand comando,string display,string valor)
        {
            conexion.Open();
            
            
            adaptador = new SqlDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);
            cbo.DataSource = tabla;
            cbo.DisplayMember = display;
            cbo.ValueMember = valor;
            conexion.Close();
        }

        public void CargarDataGrid(DataGridView dgv)
        {
            conexion.Open();
            comando = new SqlCommand(" select b.id 'ID', b.nombre 'Nombre', b.apellido 'Apellido', b.direccion 'Direccion', b.sueldoBasico 'Sueldo', " +
                "cat.categoria 'Categoria', t.turno 'Turno', c.capacidad 'Capacidad' from Bedeles b, cursos c, Categorias cat, turnos t " +
                "where b.id = c.idBedel and(b.idCategoria = cat.idCategoria) and(c.idTurno = t.id) ", conexion);
            adaptador = new SqlDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);
            conexion.Close();
            dgv.DataSource = tabla;
            
        }

        public int CantPorTurno(int idTurno)
        {
            int valor;
            conexion.Open();
            comando = new SqlCommand("spCantBedelesPorTurno", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idTurno", idTurno);
            try
            {
               valor = Convert.ToInt32( comando.ExecuteScalar());
            }
            catch (Exception)
            {

                throw;
            }
            conexion.Close();
            return valor;
        }


    }
}
