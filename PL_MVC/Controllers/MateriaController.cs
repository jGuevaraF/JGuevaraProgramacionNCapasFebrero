using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        [HttpGet] //DECORADORES
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = BL.Materia.GetAllEF();

            if (result.Correct)
            {
                //Obtuvo toda la informacion

                //result.Objects
                materia.Materias = result.Objects;  //>0
            }
            else
            {
                //Imprimir un mensaje de ERROR
                materia.Materias = new List<object>(); //0
            }

            return View(materia); //SOLO PUEDES PASAR UN VALOR
                                  //INT, STRING, 
                                  //OBJETO, LISTA
        }

        [HttpGet] //MOSTRANDO UNA VISTA
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia(); //materia es vacia
                                                    //la puerta con Semestre esta Cerrada
            materia.Semestre = new ML.Semestre(); //ABRIR LA PUERTA

            ML.Result resultDDL =  BL.Semestre.GetAll();
            materia.Semestre.Semestres = resultDDL.Objects;


            if (IdMateria == null)
            {
                //ADD
                //Vacio

            }
            else
            {
                //ACTUALIZAR
                //Lleno
                //GetById
                ML.Result result = BL.Materia.GetByIdEF(IdMateria.Value);
                //result.Object
                //UNBOXING
                materia = (ML.Materia)result.Object; //Materia LLENA
            }

            return View(materia);

        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            if (materia.IdMateria == 0)
            {
                //ADD
                BL.Materia.AddEF(materia);
            }
            else
            {
                BL.Materia.UpdateLINQ(materia);
            }

            //GetAll
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Delete(int? IdMateria)
        {
            BL.Materia.DeleteEF(IdMateria.Value);


            //return View("GetAll");

            return RedirectToAction("GetAll");



        }

    }
}