using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class MateriaController : ApiController
    {
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            //if(ModelState.IsValid)
            //{
            //    //toda la informacion esta bien

            //    //Nombre sjl
            //}
            ML.Materia materia = new ML.Materia();
            materia.Nombre = "";
            materia.Semestre = new ML.Semestre();
            materia.Semestre.IdSemestre = 0;
            ML.Result result = BL.Materia.GetAllEF(materia);

            if (result.Correct)
            {
                //esta bien
                //200
                return Ok(result);
                //return Content(Ok, result);
            } else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
