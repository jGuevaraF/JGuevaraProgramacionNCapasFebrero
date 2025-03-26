using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Materia" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Materia.svc or Materia.svc.cs at the Solution Explorer and start debugging.
    public class Materia : IMateria, CalculadoraTest
    {
        public int Suma(int numer1, int numer2)
        {
            return numer1+numer2;
        }

        public int Resta(int numer1, int numer2)
        {
            return numer1 + numer2;
        }
        public void DoWork()
        {
        }

        public void DoWork2()
        {
        }

        public void DoWork4()
        {
        }


        public SL_WCF.Result Delete(int IdMateria) //metodo implementado
        {
            ML.Result result = BL.Materia.DeleteEF(IdMateria);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result Add(ML.Materia materia) //metodo implementado
        {
            ML.Result result = BL.Materia.Add(materia);

            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage + result.Ex.InnerException,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result GetAll(ML.Materia materia)
        {
            ML.Result result = BL.Materia.GetAllEF(materia);

            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

    }
}
