using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Semestre
    {
        //LINQ = hacer DropDownList
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    var query = (from semestre in context.Semestres
                                 select new
                                 {
                                     IdSemestre = semestre.IdSemestre,
                                     Nombre = semestre.Nombre
                                 }).ToList();

                    if(query.Count > 0 )
                    {
                        //trae registros
                        result.Objects = new List<object>(); //lo instancia para poder guardar informacion

                        foreach(var item in query)
                        {
                            ML.Semestre semestre = new ML.Semestre();
                            semestre.Nombre = item.Nombre;
                            semestre.IdSemestre = item.IdSemestre;

                            result.Objects.Add(semestre);
                        }

                        result.Correct = true;
                    }
                }
                 
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
