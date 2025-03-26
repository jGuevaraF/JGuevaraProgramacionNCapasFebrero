using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMateria" in both code and config file together.
    [ServiceContract]
    public interface IMateria
    {

        [OperationContract]
        void DoWork();
        void DoWork2();
        void DoWork4();

        [OperationContract]
        SL_WCF.Result Delete(int IdMateria); //firma de metodo
            
        [OperationContract]
        SL_WCF.Result Add(ML.Materia materia);


    }

    public interface CalculadoraTest
    {
        int Suma(int numero1, int numero2);
        int Resta(int numero1, int numero2);
    }
}
