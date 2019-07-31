using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colegio
{
    class Bedel : Empleado
    {
        int idCategoria;

        public int _idCategoria
        {
            set { idCategoria = value; }
            get { return idCategoria; }
        }

        public double CalcularSueldoNeto()
        {
            double neto = base._sueldoBasico * 1.05;
            if(idCategoria == 1)
                neto = base._sueldoBasico - 1.1;
            return neto;
            
        }

        public Bedel():base()
        {
            idCategoria = 0;
        }

        public Bedel(int idCategoria,string nombre, string apellido,int legajo,double sueldoBasico,string direccion) : base(nombre, apellido, legajo, sueldoBasico, direccion)
        {
            this.idCategoria = idCategoria;
        }

        public string ToStringCategoria()
        {
            string categoriaString = "Interino";
            if (idCategoria == 1)
                categoriaString = "Concursado";
            return categoriaString;
        }

        public string ToStringBedel()
        {
            return base.ToStringEmpleado() + "\n" + "Categoria: " + ToStringCategoria();
        }
    }
}
