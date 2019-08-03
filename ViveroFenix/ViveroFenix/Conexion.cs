using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;


namespace ViveroFenix
{
    class Conexion
    {
        SqlConnection conexion;
        SqlCommand comando;
        SqlDataAdapter adaptador;
        SqlDataReader lector;
        DataTable tabla;
        string cadenaConexion;
        
        public SqlConnection _conexion
        {
            set { conexion = value; }
            get { return conexion; }
        }
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
        public SqlDataReader _lector
        {
            set { lector = value; }
            get { return lector; }
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
            cadenaConexion = "Data Source=USUARIO-PC\\SQLEXPRESS;Initial Catalog=VIVERO_FENIX;Integrated Security=True";
            conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error de Conexion: " + "\n" + e.Message);
            }
            conexion.Close();
        }

        public int CargarDataGrid(DataGridView dgv,string cmd)
        {
            conexion.Open();
            int cantidad;
            comando = new SqlCommand(cmd, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            adaptador = new SqlDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);
            conexion.Close();
            cantidad = tabla.Rows.Count;
            dgv.DataSource = tabla;
            return cantidad;
        }
        public int CargarDetalles(DataGridView dgv,int cod)
        {
            conexion.Open();
            int cantidad;
            comando = new SqlCommand("spListarDestalles", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@codFactura", cod);
            adaptador = new SqlDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);
            conexion.Close();
            cantidad = tabla.Rows.Count;
            dgv.DataSource = tabla;
            conexion.Close();
            return cantidad;
        }

        public double PrecioTotalFactura(int nroFactura)
        {
            double total;
            conexion.Open();
            comando = new SqlCommand("spTotalPorFactura", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nroFactura", nroFactura);
            total = Convert.ToDouble(comando.ExecuteScalar());
            conexion.Close();
            return total;
        }

        public void ProductosPreferidos(Chart chart)
        {
            conexion.Open();
            ArrayList nombres = new ArrayList();
            ArrayList cantidad = new ArrayList();
            comando = new SqlCommand("SP_PRODUCTOS_PREFERIDOS",conexion);
            comando.CommandType = CommandType.StoredProcedure;
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                nombres.Add(lector.GetString(0));
                cantidad.Add(lector.GetInt32(1));
            }
            chart.Series[0].Points.DataBindXY(nombres, cantidad);
            
            lector.Close();
            conexion.Close();
        }

        public double TotalVentas()
        {
            conexion.Open();
            double total = 0;
            comando = new SqlCommand("SELECT SUM(DET.CANTIDAD * P.PRECIO) AS TOTAL FROM DETALLES_FACTURAS DET,PLANTAS P " +
                "WHERE DET.COD_PLANTA = P.COD_PLANTA GROUP BY(DET.NRO_FACTURA)", conexion);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
               total += Convert.ToDouble( lector.GetDecimal(0));
            }
            lector.Close();
            conexion.Close();
            return total;
            
        }

        public void PlantasPorTipo(Chart chart)
        {
            ArrayList tipos = new ArrayList();
            ArrayList cantidad = new ArrayList();
            comando = new SqlCommand("spPlantasPorTipo", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                tipos.Add(lector.GetString(0));
                cantidad.Add(lector.GetInt32(1));
            }
            chart.Series[0].Points.DataBindXY(tipos, cantidad);
            chart.Series[0].Name = "Plantas Por Tipo";
            conexion.Close();
        }
        public void DashBoard(Label lblcantPlantas,Label lblcantClientes,Label lblcantVentas,Label lblcantProdVend)
        {
            conexion.Open();
            comando = new SqlCommand("spDashBoard", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter cantPlantas = new SqlParameter("@cantPlantas", 0);cantPlantas.Direction = ParameterDirection.Output;
            SqlParameter cantClientes = new SqlParameter("@cantClientes", 0);cantClientes.Direction = ParameterDirection.Output;
            SqlParameter cantVentas = new SqlParameter("@cantVentas", 0);cantVentas.Direction = ParameterDirection.Output;
            SqlParameter cantProdVendidos = new SqlParameter("@cantProdVendidos", 0);cantProdVendidos.Direction = ParameterDirection.Output;
            comando.Parameters.Add(cantPlantas);
            comando.Parameters.Add(cantClientes);
            comando.Parameters.Add(cantVentas);
            comando.Parameters.Add(cantProdVendidos);
            comando.ExecuteNonQuery();
            lblcantPlantas.Text = comando.Parameters["@cantPlantas"].Value.ToString();
            lblcantClientes.Text = comando.Parameters["@cantClientes"].Value.ToString();
            lblcantProdVend.Text = comando.Parameters["@cantProdVendidos"].Value.ToString();
            lblcantVentas.Text = comando.Parameters["@cantVentas"].Value.ToString();
            conexion.Close();

        }

        public void BuscarPor(RadioButton nombre,RadioButton id,RadioButton tipo,string busqueda,DataGridView dgv,Label lbl)
        {
            
            conexion.Open();
            if (nombre.Checked)
            {
                comando = new SqlCommand("spBuscarPorNombre", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@NOMBRE", busqueda);
                adaptador = new SqlDataAdapter(comando);
                tabla = new DataTable();
                adaptador.Fill(tabla);
                dgv.DataSource = tabla;
                lbl.Text = tabla.Rows.Count.ToString();
                
            }
            if (id.Checked)
            {
                comando = new SqlCommand("spBuscarPorId", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@ID", busqueda);
                adaptador = new SqlDataAdapter(comando);
                tabla = new DataTable();
                adaptador.Fill(tabla);
                dgv.DataSource = tabla;
                lbl.Text = tabla.Rows.Count.ToString();

            }
            if (tipo.Checked)
            {
                comando = new SqlCommand("spBuscarPorTipo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@TIPO", busqueda);
                adaptador = new SqlDataAdapter(comando);
                tabla = new DataTable();
                adaptador.Fill(tabla);
                dgv.DataSource = tabla;
                lbl.Text = tabla.Rows.Count.ToString();

            }
            conexion.Close();
            


        }

        public string CargarPlanta(int codigo,string nombre,int tipo,double precio,int stock)
        {
            string salida;
            comando = new SqlCommand("spInsertarPlanta", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@COD", codigo);
            comando.Parameters.AddWithValue("@NOMBRE", nombre);
            comando.Parameters.AddWithValue("@TIPO", tipo);
            comando.Parameters.AddWithValue("@STOCK", stock);
            comando.Parameters.AddWithValue("@PRECIO", precio);

            conexion.Open();
            try
            {
                comando.ExecuteNonQuery();
                salida = "Se registro correctamente";
            }
            catch (Exception e)
            {
                salida = "Error de registro" + "\n" + e.Message;
                
            }
            conexion.Close();
            return salida;
            
        }

        public void CargarCombo(ComboBox combo,string procedimiento)
        {
            comando = new SqlCommand(procedimiento, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            adaptador = new SqlDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);
            conexion.Close();
            combo.DataSource = tabla;
            combo.DisplayMember = tabla.Columns[1].ColumnName;
            combo.ValueMember = tabla.Columns[0].ColumnName;

            
        }

        public int ValorMiembro(string texto)
        {
            int valor;
            comando = new SqlCommand("spObtenerValueMember", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NOMBRE", texto);
            conexion.Open();
            valor = Convert.ToInt32(comando.ExecuteScalar());
            conexion.Close();
            return valor;
            
        }

        public string ActualizarPlanta(int id,string desc,double precio,int tipo,int stock)
        {
            string salida;
            comando = new SqlCommand("spActualizarRegistro", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@DESC", desc);
            comando.Parameters.AddWithValue("@PRECIO", precio);
            comando.Parameters.AddWithValue("@TIPO", tipo);
            comando.Parameters.AddWithValue("@STOCK", stock);
            conexion.Open();
            try
            {
                comando.ExecuteNonQuery();
                salida = "Registro Actualizado";
            }
            catch (Exception e)
            {
                salida = "Error de actualizacion de registro: " + "\n" + e.Message;
                
            }
            conexion.Close();
            return salida;
            
        }

        public void CargarProdSeleccionados(DataGridView dgv,int id)
        {
            comando = new SqlCommand("spListarProdSelec", conexion);
            conexion.Open();
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@ID", id);
            adaptador = new SqlDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);
            dgv.DataSource = tabla;
            conexion.Close();
        }

        public string RegistrarVenta(int id,DateTime fecha,int formaPago,int cliente)
        {
            string salida;
            comando = new SqlCommand("spRegistrarVenta", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@FECHA", fecha);
            comando.Parameters.AddWithValue("@FORMA_PAGO", formaPago);
            comando.Parameters.AddWithValue("@CLIENTE", cliente);
            conexion.Open();
            try
            {
                comando.ExecuteNonQuery();
                salida = "Registrado";
            }
            catch (Exception e)
            {
                salida = "Error: " + "\n" + e.Message;
            }
            conexion.Close();
            return salida;
            
        }
        public string RegistrarDetalle(int codDetalle,int idPlanta,int cantidad,int idFact)
        {
            string salida;
            comando = new SqlCommand("spRegistrarDetalleVenta", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@COD_DETALLE", codDetalle);
            comando.Parameters.AddWithValue("@ID_PLANTA", idPlanta);
            comando.Parameters.AddWithValue("@CANTIDAD", cantidad);
            comando.Parameters.AddWithValue("@ID_FACT", idFact);
            conexion.Open();
            try
            {
                comando.ExecuteNonQuery();
                salida = "Registrado";
            }
            catch (Exception e)
            {
                salida = "Error: " + "\n" + e.Message;
            }
            conexion.Close();
            return salida;

        }

        public int ObtenerIdDetalle()
        {
            int mayor;
            comando = new SqlCommand("select max(cod_detalle) from DETALLES_FACTURAS", conexion);
            conexion.Open();
            mayor = Convert.ToInt32(comando.ExecuteScalar());
            conexion.Close();
            return mayor + 1;

        }

        public int ObtenerIdFactura()
        {
            int mayor;
            comando = new SqlCommand("SELECT MAX(NRO_FACTURA + 1) FROM FACTURAS", conexion);
            conexion.Open();
            mayor = Convert.ToInt32(comando.ExecuteScalar());
            conexion.Close();
            return mayor;
        }

        public void MejoresClientes(Chart chart)
        {
            ArrayList nombres = new ArrayList();
            ArrayList total = new ArrayList();
            comando = new SqlCommand("spMejoresClientes", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                nombres.Add(lector.GetString(0));
                total.Add(Convert.ToDouble( lector.GetDecimal(1)));
            }
            chart.Series[0].Points.DataBindXY(nombres, total);
            conexion.Close();
            
        }

    }
}
