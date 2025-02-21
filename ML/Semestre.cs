using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Semestre
    {
        public int IdSemestre { get; set; }
        public string Nombre { get; set; }

        public List<object> Semestres { get; set; } //para mandar la informacion a la vista y colocarlo en DropDownList (DDL)

    }
}
