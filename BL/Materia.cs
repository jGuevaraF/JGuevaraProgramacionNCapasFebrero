using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {

        public static ML.Result GetByIdSemestre (int IdSemestre)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    var query = context.MateriaGetByIdSemestre(IdSemestre).ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(var objBD in query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = objBD.IdMateria;
                            materia.Nombre = objBD.Nombre;

                            result.Objects.Add(materia);
                        }

                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
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
        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "MateriaAdd";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", materia.Nombre);
                    cmd.Parameters.AddWithValue("@Creditos", materia.Creditos);
                    cmd.Parameters.AddWithValue("@Costo", materia.Costo);

                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo insertar";
                    }
                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static void AddSqlClient(ML.Materia materia)
        {
            //1 abrir una conexion con la BD
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "INSERT INTO Materia VALUES (@nombre, @creditos, @costo)";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query; //query que va a ejecutar
                    cmd.Connection = context; //BD

                    cmd.Parameters.AddWithValue("@nombre", materia.Nombre);
                    cmd.Parameters.AddWithValue("@creditos", materia.Creditos);
                    cmd.Parameters.AddWithValue("@costo", materia.Costo);

                    context.Open(); //abrir la conexion con la BD
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("EL registro se inserto correctamente");
                    }
                    else
                    {
                        Console.WriteLine("Error al insertar");
                    }
                }
                //CERRAR

            }
            catch (Exception)
            {
                Console.WriteLine("Error de conexion");
            }
        }
        public static void Delete(int idMateria)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "DELETE FROM Materia WHERE IdMateria = @IdMateria";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@IdMateria", idMateria);

                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("El registro se ha eliminado");
                    }
                    else
                    {
                        Console.WriteLine("Error al eliminar");
                    }
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Error de conexion");
            }
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "SELECT * FROM Materia";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        //si trae registros
                        result.Objects = new List<object>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = Convert.ToInt32(row[0].ToString());
                            materia.Nombre = row[1].ToString();
                            materia.Creditos = Convert.ToDecimal(row[2].ToString());
                            materia.Costo = Convert.ToDecimal(row[3].ToString());

                            //materia.ImagenBase64 = Convert.ToBase64String(row[5]);

                            result.Objects.Add(materia);
                        }
                        result.Correct = true;

                    }
                    else
                    {
                        //no hay registros
                        result.Correct = false;
                        result.ErrorMessage = "No hay datos/registros";
                    }

                }

            }
            catch (Exception ex)
            {
                //HUBO UN ERROR
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "SELECT * FROM Materia WHERE IdMateria = @IdMateria";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@IdMateria", IdMateria);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];

                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = Convert.ToInt32(row[0].ToString());
                        materia.Nombre = row[1].ToString();
                        materia.Creditos = Convert.ToDecimal(row[2].ToString());
                        materia.Costo = Convert.ToDecimal(row[3].ToString());

                        result.Object = materia; //BOXING

                        result.Correct = true;

                    }
                    else
                    {
                        //no hay registros
                        result.Correct = false;
                        result.ErrorMessage = "No hay datos/registros";
                    }

                }

            }
            catch (Exception ex)
            {
                //HUBO UN ERROR
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result AddEF(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasFebreroEntities contex = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    int rowsAffect = contex.MateriaAdd(materia.Nombre, materia.Creditos, materia.Costo, Convert.ToDateTime(materia.Fecha), materia.Semestre.IdSemestre,materia.Imagen);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }
        public static ML.Result DeleteEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    context.MateriaDelete(IdUsuario);
                    context.SaveChanges();

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetAllEF(ML.Materia materiaObj)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    var query = context.MateriaGetAll(materiaObj.Nombre,materiaObj.Semestre.IdSemestre).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();



                        foreach (var objBD in query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.Semestre = new ML.Semestre();

                            materia.IdMateria = objBD.IdMateria;
                            materia.Nombre = objBD.NombreMateria;
                            materia.Costo = Convert.ToDecimal(objBD.Costo);
                            materia.Creditos = Convert.ToDecimal(objBD.Creditos);
                            materia.Status = Convert.ToBoolean(objBD.Status);
                            materia.Fecha = objBD.Fecha; //ya no traigo la hora
                            materia.Semestre.Nombre = objBD.NombreSemestre;
                            materia.Imagen = objBD.Imagen;

                            result.Objects.Add(materia);
                        }

                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }



        public static ML.Result GetByIdEF(int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    var query = context.MateriaGetById(idMateria).FirstOrDefault();

                    if (query != null)
                    {
                        //si trajo registro
                        ML.Materia materia = new ML.Materia();
                        materia.Semestre = new ML.Semestre(); //abri la puerta

                        materia.IdMateria = query.IdMateria;
                        materia.Nombre = query.Nombre;
                        materia.Costo = Convert.ToInt32(query.Costo);
                        materia.Creditos = Convert.ToInt32(query.Creditos);
                        materia.Fecha = query.Fecha;

                        if (query.IdSemestre != null)
                        {
                            //trae un id
                            materia.Semestre.IdSemestre = query.IdSemestre.Value;
                        } else
                        {
                            materia.Semestre.IdSemestre = 0;
                        }

                        result.Object = materia; //BOXING

                        result.Correct = true;
                    }
                    else
                    {
                        //no trajo registros
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result AddLINQ(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    DL_EF.Materia materiaBD = new DL_EF.Materia();
                    materiaBD.Nombre = materia.Nombre;
                    materiaBD.Creditos = materia.Creditos;
                    materiaBD.Costo = materia.Costo;
                    //materiaBD.Fecha = DateTime.Parse(materia.Fecha);

                    context.Materias.Add(materiaBD); //GENERA EL INSERT
                                                     //INSERT INTO MATERIA VALUES ()

                    int filasAfectadas = context.SaveChanges();  //EJECUTA EL insert

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result DeleteLINQ(int idMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    //1. Busca => SELECT
                    var soloDelete = (from materia in context.Materias
                                      where materia.IdMateria == idMateria
                                      select materia).FirstOrDefault();
                    //SELECT * FROM Materia

                    var buscarBIEN = (from materia in context.Materias
                                      where materia.IdMateria == idMateria
                                      select new
                                      {
                                          //ALIAS = VALOR
                                          IdMateria = materia.IdMateria,
                                          Nombre = materia.Nombre,
                                          Creditos = materia.Creditos,
                                          Costo = materia.Costo,
                                          Fecha = materia.Fecha
                                      }).SingleOrDefault();
                    //SELECT IdMateria, Nombre, Costo, Creditos, Fecha FROM Materia
                    //WHERE IdMateria = @IdMateria

                    if (soloDelete != null)
                    {
                        //encontro un registro

                        //2. Delete = DELETE
                        context.Materias.Attach(soloDelete);

                        context.Materias.Remove(soloDelete); //generando el query
                                                             //DELETE MATERIA WHERE ID = @ID

                        int filasAfectadas = context.SaveChanges(); //ejecuta el query

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo eliminar";
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result UpdateLINQ(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    var busqueda = (from materiaBD in context.Materias
                                    where materiaBD.IdMateria == materia.IdMateria
                                    select materiaBD).SingleOrDefault();
                    //SELECT * FROM MATERIA WHERE

                    if (busqueda != null)
                    {
                        //encontro algo

                        //SET NOMBRE = @NOMBRE
                        busqueda.Nombre = materia.Nombre;

                        //COSTO = @COSTO
                        busqueda.Costo = materia.Costo;
                        busqueda.Creditos = materia.Creditos;
                        busqueda.Fecha = Convert.ToDateTime(materia.Fecha);
                        busqueda.IdSemestre = materia.Semestre.IdSemestre;
                        //busqueda.Fecha = Convert.ToDateTime(materia.Fecha);

                        int filasAfectadas = context.SaveChanges(); //ejeucta el update

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Hubo un error";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result CambioStatus(int IdMateria, bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    int rowsAffect = context.CambioStatus(IdMateria, Status);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }



        //SQL CLIENT
        //GETALL
        public static ML.Result GetByIdSqlClient(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "MateriaGetById";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdMateria", IdMateria);

                    //SqlDataAdapter
                    //DataTable

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 1)
                    {
                        //si trajo informacion
                        //sacar/obtener la informacion de la BD, para pasarsela a un modelo de ML.Materia, para pasarsela a result.objects

                        //NULL cambio a VACIO

                        //UNBOXING
                        //Lista guardas ML.Materia
                        //Obtienes/Sacas una materia como un objeto
                        //Trabajar con ese objeto, puedas ocupar sus propiedades

                        DataRow fila = dataTable.Rows[0];

                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = int.Parse(fila[0].ToString());
                        materia.Nombre = fila["Nombre"].ToString();

                        //los demas registros

                        result.Object = materia;

                        //OBTUVE TODA LA INFORMACION CORRECTAMENTE, CUANDO HAYA TERMINADO EL FOREACH

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }



        //LINQ
        //NO SE VAN A OCUPAR LOS SP
        //NO OCUPO SqlClient, ENTITY FRAMEWORK
        //GETALL

        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    var query = (from materia in context.Materias
                                 where materia.IdMateria == 10
                                 select new
                                 {
                                     //alias = valor
                                     Id = materia.IdMateria,
                                     NombreMateria = materia.Nombre,
                                     CostoMateria = materia.Costo
                                 }).SingleOrDefault();

                    if (query != null)
                    {


                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = query.Id;
                        materia.Nombre = query.NombreMateria;

                        //sacar los demas datos

                        result.Object = materia;
                        //OBTENER TODA LA INFORMACION CORRECTAMENTE
                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public static ML.Result LeerExcel(string cadenaConexion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(cadenaConexion))
                {
                    string query = "Select * from[Hoja1$]";
                    using(OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText =  query;
                        cmd.Connection = context;

                        OleDbDataAdapter adapter = new OleDbDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable tablaMateria = new DataTable();
                        adapter.Fill(tablaMateria);

                        if(tablaMateria.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaMateria.Rows)
                            {
                                ML.Materia materia = new ML.Materia();
                                materia.Nombre = row[0].ToString();
                                materia.Creditos = Convert.ToUInt16(row[1]);
                                materia.Costo = Convert.ToUInt16(row[2]);

                                result.Objects.Add(materia);

                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct= false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.ResultExcel ValidarExcel(List<object> registros) //result.Objects Lee el excel
        {
            ML.ResultExcel result = new ML.ResultExcel();
            result.Errores = new List<object>();

            int contador = 1;

            foreach(ML.Materia materia in registros)
            {
                ML.ResultExcel errorRegistro = new ML.ResultExcel();

                errorRegistro.NumeroRegistro = contador;

                if(materia.Nombre.Length > 50 || materia.Nombre == "" || materia.Nombre == null)
                {
                    errorRegistro.ErrorMessage += "la columna A2, esta mal"; 
                }

                if(materia.Creditos > 50 || materia.Creditos == 0)
                {
                    errorRegistro.ErrorMessage += "La columna de Creditos esta mal, poruqejn f";
                }

                //las demas

                if(errorRegistro.ErrorMessage != "" || errorRegistro.ErrorMessage != null)
                {
                    //hubo un error
                    result.Errores.Add(errorRegistro);
                }

                contador++;


            }

            return result;
        }


    }
}
