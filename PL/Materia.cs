using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Materia
    {
        public static void Add() //logica para pedir la informacion
        {
            ML.Materia materia = new ML.Materia();

            Console.WriteLine("Dame el nombre de la materia");
            materia.Nombre = Console.ReadLine();
            Console.WriteLine("Dame los creditos");
            materia.Creditos = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Dame el costo");
            materia.Costo = Convert.ToDecimal(Console.ReadLine());

            BL.Materia.AddEF(materia);
        }
        public static void Delete() //logica para pedir la informacion
        {

            Console.WriteLine("Dame el id de la materia que quieres eliminar");
            int idMateria = Convert.ToInt32(Console.ReadLine());

            BL.Materia.Delete(idMateria);
        }
        public static void GetAll()
        {
            ML.Result result = BL.Materia.GetAll();

            if (result.Correct)
            {
                //mostrar los registros
                foreach(ML.Materia materia in result.Objects)
                {
                    Console.WriteLine(materia.IdMateria);
                    Console.WriteLine(materia.Creditos);
                }
            } else
            {
                Console.WriteLine("Hubo un error "+result.ErrorMessage);
            }
        }
        public static void GetById() //logica para pedir la informacion
        {
            Console.WriteLine("Dame el id que quieres buscar");
            int idMateria = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Materia.GetById(idMateria);

            if (result.Correct)
            {
                //imprimir la informacion de la materia
                ML.Materia materia = (ML.Materia)result.Object; //UNBOXING
                Console.WriteLine(materia.Nombre);
                Console.WriteLine(materia.Creditos);
                Console.WriteLine(materia.Costo);
            } else
            {
                Console.WriteLine("error " + result.ErrorMessage);
            }
        }
    }
}
