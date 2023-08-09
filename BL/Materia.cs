using DL_EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//import
//include
//using

namespace BL
{
    public class Materia
    {
        public static ML.Result GetById(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                //METODO
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {

                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT IdMateria, Nombre, Costo, Creditos FROM Materia"; //Orden y filtrar  //JOIN
                    cmd.Connection = context;

                    SqlDataAdapter da = new SqlDataAdapter(cmd); //Puente Base de datos y Programa ObtenerDatos
                    DataTable tablaMateria = new DataTable();

                    da.Fill(tablaMateria);

                    if (tablaMateria.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row in tablaMateria.Rows)
                        {                                                        
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = byte.Parse(row[0].ToString());
                            materia.Nombre = row[1].ToString();
                            materia.Costo = decimal.Parse(row[2].ToString());
                            materia.Creditos = byte.Parse(row[3].ToString());

                            result.Objects.Add(materia);
                        }

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Tabla no contiene información";
                    }

                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result GetById(byte IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT IdMateria, Nombre, Costo, Creditos FROM Materia WHERE IdMateria = @IdMateria"; //Orden y filtrar  //JOIN
                    
                    cmd.Parameters.AddWithValue("@IdMateria", IdMateria);
                    
                    cmd.Connection = context;

                    SqlDataAdapter da = new SqlDataAdapter(cmd); //Puente Base de datos y Programa ObtenerDatos
                    DataTable tablaMateria = new DataTable();

                    da.Fill(tablaMateria);

                    if (tablaMateria.Rows.Count > 0)
                    {
                        DataRow row = tablaMateria.Rows[0];

                        ML.Materia materia = new ML.Materia();

                        materia.IdMateria = byte.Parse(row[0].ToString());

                        materia.Nombre = row[1].ToString();

                        materia.Costo = decimal.Parse(row[2].ToString());

                        materia.Creditos = byte.Parse(row[3].ToString());

                        result.Object = materia;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Tabla no contiene información";
                    }

                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                result.Correct = false;
            }
            return result;
        }
        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            //Destruir los objetos utilizados dentro de el

            try
            {
                //Bloque codigo que puede tener error
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    // SqlCommand cmd = new SqlCommand("INSERT INTO [Materia]([Nombre],[Creditos],[Costo]) VALUES (@Nombre, @Creditos,@Costo)", conn);
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = "INSERT INTO[Materia]([Nombre],[Creditos],[Costo]) VALUES(@Nombre, @Creditos, @Costo)";
                    cmd1.Connection = conn;

                    cmd1.Parameters.AddWithValue("@Nombre", materia.Nombre);
                    cmd1.Parameters.AddWithValue("@Creditos", materia.Creditos);
                    cmd1.Parameters.AddWithValue("@Costo", materia.Costo);

                    cmd1.Connection.Open();

                    int rowsAffected = cmd1.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al insertar el materia " + materia.Nombre;
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

        public static ML.Result AddEF(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JTorresProgramacionNCapas24072023Entities context = new DL_EF.JTorresProgramacionNCapas24072023Entities())
                {
                    var queryResult = context.MateriaAdd
                        (
                            materia.Nombre, 
                            materia.Creditos, 
                            materia.Costo, 
                            materia.Semestre.IdSemestre
                        );
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


        public static ML.Result AddLinQ(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JTorresProgramacionNCapas24072023Entities context = new DL_EF.JTorresProgramacionNCapas24072023Entities())
                {
                   
                    DL_EF.Materia materiaLinq = new DL_EF.Materia();
                    materiaLinq.Nombre = materia.Nombre;
                    materiaLinq.Costo = materia.Costo;
                    materiaLinq.Creditos = materia.Creditos;
                    materiaLinq.IdSemestre = materia.Semestre.IdSemestre;

                    var queryResult = context.Materias.Add(materiaLinq);

                    context.SaveChanges();

                    if(queryResult != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El registro no pudo insertar";

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

        public static ML.Result UpdateLinQ(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JTorresProgramacionNCapas24072023Entities context = new DL_EF.JTorresProgramacionNCapas24072023Entities())
                {

                    var queryResult = (from materiaLinq in context.Materias
                                      where materiaLinq.IdMateria == materia.IdMateria
                                      select materiaLinq).SingleOrDefault();

                    if (queryResult != null)
                    {
                        queryResult.Nombre = materia.Nombre;
                        queryResult.Creditos = materia.Creditos;
                        queryResult.Costo = materia.Costo;
                        queryResult.IdSemestre = materia.Semestre.IdSemestre;

                        context.SaveChanges();

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el registro a actualizar";

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
        //cannot convert int? to int
        public static ML.Result DeleteLinQ(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JTorresProgramacionNCapas24072023Entities context = new DL_EF.JTorresProgramacionNCapas24072023Entities())
                {

                    var queryResult = (from materiaLinq in context.Materias
                                       where materiaLinq.IdMateria == materia.IdMateria
                                       select materiaLinq).SingleOrDefault();

                    if (queryResult != null)
                    {
                        context.Materias.Remove(queryResult);
                        context.SaveChanges();

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el registro a actualizar";

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

        public static ML.Result GetAllLinQ()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JTorresProgramacionNCapas24072023Entities context = new DL_EF.JTorresProgramacionNCapas24072023Entities())
                {

                    var queryResult = (from materiaLinq in context.Materias                                       
                                       select materiaLinq).ToList();

                    if (queryResult != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var objMateria in queryResult)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = objMateria.IdMateria;
                            materia.Nombre = objMateria.Nombre;
                            materia.Creditos = objMateria.Creditos.Value;
                            materia.Costo = objMateria.Costo.Value;
                            materia.Semestre = new ML.Semestre();
                                                                            //1,2 2 3 5
                            materia.Semestre.IdSemestre = (objMateria.IdSemestre != null) ? byte.Parse(objMateria.IdSemestre.Value.ToString()) : byte.Parse("0");

                            //operador ternario ->

                            result.Objects.Add(materia);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el registro a actualizar";

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
