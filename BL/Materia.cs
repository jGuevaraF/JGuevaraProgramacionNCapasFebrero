using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
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
            catch (Exception ex)
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
            catch (Exception ex)
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
                    int rowsAffect = contex.MateriaAdd(materia.Nombre, materia.Creditos, materia.Costo, DateTime.Parse(materia.Fecha));

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

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasFebreroEntities context = new DL_EF.JGuevaraProgramacionNCapasFebreroEntities())
                {
                    var query = context.MateriaGetAll().ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();



                        foreach (var objBD in query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = objBD.IdMateria;
                            materia.Fecha = objBD.Fecha; //ya no traigo la hora
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
                    var query = context.MateriaGetAll().FirstOrDefault();

                    if (query != null)
                    {
                        //si trajo registro
                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = query.IdMateria;
                        materia.Nombre = query.Nombre;

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
                    materiaBD.Fecha = DateTime.Parse(materia.Fecha);

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
    }
}
