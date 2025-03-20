using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; }
        [DisplayName ("Nombre con data annotation")]
        [Required(ErrorMessage =  "Nombre Obligatorio")]
        [RegularExpression(@"/[a-z A-Z]/", ErrorMessage = " Nombre solo se acepta letras ")]
        public string Nombre { get; set; }
        public decimal Creditos { get; set; }
        public decimal Costo { get; set; }
        public string Fecha { get; set; }
        public ML.Semestre Semestre { get; set; } //FK
        public bool Status { get; set; }
        public byte[] Imagen { get; set; }
        public List<object> Materias { get; set; } //para mandar la informacion a la vista
    }
}
