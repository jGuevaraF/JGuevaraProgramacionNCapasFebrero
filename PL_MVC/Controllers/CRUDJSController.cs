using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class CRUDJSController : Controller
    {
        // GET: CRUDJS
        public ActionResult GetAll()
        {
            return View();
        }


        //[HttpGet]
        //public JsonResult Add(ML.Usuario usuario)
        //{
        //    if(usuario.ImagenBase64 != "") //httppostfilebase
        //    {
        //        usuario.Imagen = Convert.FromBase64String(usuario.ImagenBase64);
        //    }

        //    BL.Usuario.Add(usuario);

        //    ML.Materia materia = new ML.Materia();
        //    materia.Nombre = "";

        //    //JsonResultMaxLenght
        //    ML.Result result = BL.Materia.GetAll();

        //    JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

        //    //Set the MaxJsonLength to the maximum value.
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}

        //public JsonResult Add(ML.Materia materia)
        //{
        //    materia.Imagen = Convert.FromBase64String(materia.ImagenBase64);

        //    BL.Materia.Add(materia);
        //}
    }
}