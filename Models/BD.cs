using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;


    public class BD
    {
        private static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=PreguntadosDB;Trusted_Connection=True;";

        public static List<categoria> ObtenerCategorias()
        {
            List<categoria> categorias = new List<categoria>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT nombre FROM categoria";
                categorias = connection.Query<categoria>(query).ToList();
            }
            return categorias;
        }

        public static List<dificultad> ObtenerDificultades()
        {
            List<dificultad> dificultades = new List<dificultad>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT nombre FROM dificultades";
                dificultades = connection.Query<dificultad>(query).ToList();
            }
            return dificultades;
        }

        public static List<pregunta> ObtenerPreguntas(int dificultad, int categoria)
        {
            List<pregunta> preguntas = new List<pregunta>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT enunciado FROM preguntas";

                if (dificultad == -1 && categoria == -1)
                {
                    query = "SELECT * FROM preguntas";
                }
                else if (dificultad == -1)
                {
                    query = "SELECT * FROM preguntas WHERE idCategoria = @categoria";
                    preguntas = connection.Query<pregunta>(query, new { categoria }).ToList();
                    return preguntas;
                }
                else if (categoria == -1)
                {
                    query = "SELECT * FROM preguntas WHERE idDificultad = @dificultad";
                    preguntas = connection.Query<pregunta>(query, new { dificultad }).ToList();
                    return preguntas;
                }
                else
                {
                    query = "SELECT * FROM preguntas WHERE idCategoria = @categoria AND idDificultad = @dificultad";
                    preguntas = connection.Query<pregunta>(query, new { dificultad, categoria }).ToList();
                }

                preguntas = connection.Query<pregunta>(query, new { dificultad, categoria }).ToList();
            }
            return preguntas;
        }

        public static List<respuesta> ObtenerRespuestas(int idPregunta)
        {
            List<respuesta> respuestas = new List<respuesta>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM respuestas WHERE idPregunta = @idPregunta";
                respuestas = connection.Query<respuesta>(query, new { idPregunta }).ToList();
            }
            return respuestas;
        }
        

    }

