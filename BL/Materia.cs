using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                    int rowsAffect = contex.MateriaAdd(materia.Nombre, materia.Creditos, materia.Costo);

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
    }
}
