using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveroFenix
{
    class Planta
    {
        int codigo;
        string descripcion;
        int idTipo;
        double precio;
        int stock;

        public int _codigo
        {
            set { codigo = value; }
            get { return codigo; }
        }
        public string _descripcion
        {
            set { descripcion = value; }
            get { return descripcion; }
        }
        public int _idTipo
        {
            set { idTipo = value; }
            get { return idTipo; }
        }
        public double _precio
        {
            set { precio = value; }
            get { return precio; }
        }
        public int _stock
        {
            set { stock = value; }
            get { return stock; }
        }

        public string ToStringPlanta(string tipo)
        {
            return "Codigo: " + codigo.ToString() + "\n" + "Descripcion: " + descripcion + "\n" + "Tipo: " + tipo + "\n" + "Precio: " + precio.ToString() + "\n"
               + "Stock: " + stock.ToString(); 
        }

        public Planta()
        {
            codigo = 0;
            descripcion = "";
            idTipo = 0;
            precio = 0;
            stock = 0;
        }

        public Planta(int codigo,string descripcion,int idTipo,double precio,int stock)
        {
            this.codigo = codigo;
            this.descripcion = descripcion;
            this.idTipo = idTipo;
            this.precio = precio;
            this.stock = stock;
        }


    }
}
