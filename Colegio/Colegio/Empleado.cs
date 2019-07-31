using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colegio
{
    abstract class Empleado
    {
        string nombre;
        string apellido;
        int legajo;
        double sueldoBasico;
        string direccion;

        public string _nombre
        {
            set { nombre = value; }
            get { return nombre; }
        }

        public string _apellido
        {
            set { apellido = value; }
            get { return apellido; }
        }

        public int _legajo
        {
            set { legajo = value; }
            get { return legajo; }
        }

        public double _sueldoBasico
        {
            set { sueldoBasico = value; }
            get { return sueldoBasico; }
        }

        public string _direccion
        {
            set { direccion = value; }
            get { return direccion; }
        }

        public Empleado()
        {
            nombre = "";
            apellido = "";
            legajo = 0;
            sueldoBasico = 0;
            direccion = "";
        }

        public Empleado(string nombre,string apellido,int legajo,double sueldoBasico,string direccion)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.legajo = legajo;
            this.sueldoBasico = sueldoBasico;
            this.direccion = direccion;
        }

        public string ToStringEmpleado()
        {
            return "Nombre: " + nombre + "\n" + "Apellido: " + apellido + "\n" +"Direccion: " + direccion + "\n" + "Legajo: " + legajo.ToString() + "\n" 
                + "Sueldo Basico: " + sueldoBasico.ToString();
        }
    }
}
