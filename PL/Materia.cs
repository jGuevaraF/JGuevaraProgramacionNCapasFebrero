using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Materia
    {
        public static void Add() //logica para pedir la informacion
        {

            var objeto = new 
            {
                id = "media",
                nombre = "21654"
            };



            ML.Materia materia = new ML.Materia();

            Console.WriteLine("Dame el nombre de la materia");
            materia.Nombre = Console.ReadLine();
            Console.WriteLine("Dame los creditos");
            materia.Creditos = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Dame el costo");
            materia.Costo = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Dame la fecha");
            materia.Fecha = Console.ReadLine();

            BL.Materia.AddLINQ(materia);
        }
        public static void Delete() //logica para pedir la informacion
        {

            Console.WriteLine("Dame el id de la materia que quieres eliminar");
            int idMateria = Convert.ToInt32(Console.ReadLine());

            BL.Materia.DeleteLINQ(idMateria);
        }
        //public static void GetAll()
        //{
        //    ML.Result result = BL.Materia.GetAllEF();

        //    if (result.Correct)
        //    {
        //        //mostrar los registros
        //        foreach(ML.Materia materia in result.Objects)
        //        {
        //            Console.WriteLine(materia.IdMateria);
        //            Console.WriteLine(materia.Creditos);
        //        }
        //    } else
        //    {
        //        Console.WriteLine("Hubo un error "+result.ErrorMessage);
        //    }
        //}
        public static void GetById() //logica para pedir la informacion
        {
            Console.WriteLine("Dame el id que quieres buscar");
            int idMateria = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Materia.GetByIdEF(idMateria);

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


        public static void Update() //logica para pedir la informacion
        {
            ML.Materia materia = new ML.Materia();

            Console.WriteLine("Dame el id de la materia");
            materia.IdMateria = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Dame el nombre de la materia");
            materia.Nombre = Console.ReadLine();
            Console.WriteLine("Dame los creditos");
            materia.Creditos = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Dame el costo");
            materia.Costo = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Dame la fecha");
            materia.Fecha = Console.ReadLine();

            BL.Materia.UpdateLINQ(materia);
        }

        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();

            string ruta = @"C:\Users\digis\Downloads\Nuevos.txt";
            try
            {
                StreamReader streamReader = new StreamReader(ruta);
                string fila = "";
                while ((fila = streamReader.ReadLine()) != null) {

                    string[] valores = fila.Split('|');

                    ML.Materia materia = new ML.Materia();
                    materia.Nombre = valores[0];
                    materia.Creditos = Convert.ToDecimal(valores[1]);
                    materia.Costo = Convert.ToDecimal(valores[2]);

                    //"1" o "0" => status bool
                        //1 o 0 => INT
                    //convert


                    //"true" o "false"
                        //CONVERT


                    //los demas campos

                    BL.Materia.Add(materia);
                }
            }
            catch (Exception ex) {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
