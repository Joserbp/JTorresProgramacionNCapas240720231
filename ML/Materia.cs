using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    { 
        public Materia() 
        { 

        }

        public Materia (byte IdMateria)
        {

        }

        public byte IdMateria { get; set; }
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
        public byte Creditos { get; set; }
        public ML.Semestre Semestre { get; set; } //Rol

        //ML. Usuarios
        public List<object> Materias { get; set; }
    }
}
