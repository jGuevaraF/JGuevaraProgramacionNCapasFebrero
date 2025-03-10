﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
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
            materia.Semestre = new ML.Semestre();
            materia.Nombre = "";
            materia.Semestre.IdSemestre = 0;

            //Llenamos DDL de roles en busqueda abierta
            ML.Result resultDDL = BL.Semestre.GetAll();
            materia.Semestre.Semestres = resultDDL.Objects;

            ML.Result result = BL.Materia.GetAllEF(materia);

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

        [HttpPost]
        public ActionResult GetAll(ML.Materia materia)
        {
            materia.Nombre = materia.Nombre == null ? "" : materia.Nombre;
            //materia.Semestre.IdSemestre = materia.Semestre.IdSemestre == 0 ? 0 : materia.Semestre.IdSemestre;

            ML.Result result = BL.Materia.GetAllEF(materia);
            if (result.Correct)
            {
                materia.Materias = result.Objects;
            }
            else
            {
                materia.Materias = new List<object>();
            }

            //Llenamos DDL de roles en busqueda abierta
            ML.Result resultDDL = BL.Semestre.GetAll();
            materia.Semestre.Semestres = resultDDL.Objects;

            return View(materia);
        }

        [HttpGet] //MOSTRANDO UNA VISTA
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia(); //materia es vacia



            if (IdMateria == null)
            {
                //ADD
                //Vacio
                materia.Semestre = new ML.Semestre();

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
                //ID ESTADO
                //ID MUNICIPIO
                //ID COLONIA


                //DDL MUNICIPIO
                //DDL COLONIA

                //BL
                //usuario.direccion.colonia.municipio.municipios = BL.Municipio.GetByIdEstado(usuario.Direccion.COlonia.Municipio.Estado.IdEstado)

                //BL.Colonia.GetByIdMunicipio(usuario.Direccion.COlonia.Municipio.IdMunicipio)
            }

            ML.Result resultSemestres = BL.Semestre.GetAll(); //todos los semestres

            materia.Semestre.Semestres = resultSemestres.Objects; //le paso todos los valores a Semestres, para que pueda pintar el DDL


            //materia.Semestre = new ML.Semestre();

            return View(materia);

        }

        [HttpPost] //Add y Update
        public ActionResult Form(ML.Materia materia)
        {
            //materia.Imagen => valorOriginal

            HttpPostedFileBase file = Request.Files["inptFileImagen"];
            //byte = 0
            //NO TRAE NINGUN ARCHIVO

            if (file != null && file.ContentLength > 0)
            {
                //materia.Imagen => valorOriginal
                materia.Imagen = ConvertirAArrayBytes(file);
                //materia.Imagen => nuevoValor

            }

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

        [HttpPost]
        public JsonResult CambioStatus(int IdMateria, bool Status)
        {
            ML.Result JsonResult = BL.Materia.CambioStatus(IdMateria, Status);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult GetByIdSemestre(int idSemestre)
        {
            ML.Result result = BL.Materia.GetByIdSemestre(idSemestre);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }

    }
}