using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colegio
{
    class Curso
    {
        int aula;
        int idTurno;
        int capacidad;
        Bedel bedel;

        public int _aula
        {
            set { aula = value; }
            get { return aula; }
        }

        public int _idTurno
        {
            set { idTurno = value; }
            get { return idTurno; }
        }

        public int _capacidad
        {
            set { capacidad = value; }
            get { return capacidad; }
        }

        public Bedel _bedel
        {
            set { bedel = value; }
            get { return bedel; }
        }

        public Curso()
        {
            aula = 0;
            idTurno = 0;
            capacidad = 0;
            bedel = null;
        }

        public Curso(int aula,int idTurno,int capacidad,Bedel bedel)
        {
            this.aula = aula;
            this.idTurno = idTurno;
            this.capacidad = capacidad;
            this.bedel = bedel;
        }

        public string ToStringTurno()
        {
            string turnoString;
            switch (idTurno)
            {
                case 0:turnoString = "Mañana"; break;
                case 1:turnoString = "Tarde";break;
                case 2:turnoString = "Nombre";break;
                default:turnoString = "Otro";break;   
            }
            return turnoString; 
        }

        public string ToStringCurso()
        {
            return "Aula: " + aula.ToString() + "\n" + "Turno: " + ToStringTurno() + "\n" + "Capacidad: " + capacidad.ToString() + "\n" + "Bedel: " + bedel.ToStringBedel();
        }
    }
}
