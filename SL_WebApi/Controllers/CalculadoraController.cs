using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SL_WebApi.Models;

namespace SL_WebApi.Controllers
{
    [RoutePrefix("api")]
    public class CalculadoraController : ApiController
    {
        [HttpPost]
        [Route("Calculadora/Suma")]
        public IHttpActionResult Suma([FromBody] Calculadora calculadora)
        {
            int suma = calculadora.Numero1 + calculadora.Numero2;
            //BL
            //CORRECT TRUE 200
            //CORRECT FALSE 500 
            return Content(HttpStatusCode.InternalServerError, suma);
        }

        [HttpDelete]
        [Route("Calculadora/Resta/{numero1}/{numero2}")]
        public IHttpActionResult Resta(int numero1, int numero2)
        {
            int suma = numero1 + numero2;
            return Content(HttpStatusCode.OK, suma);
        }
    }
}
