using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;


    public class BD
    {
        private static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=PreguntadosDB;Trusted_Connection=True;";

        public static List<Categoria> ObtenerCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT nombre FROM categoria";
                categorias = connection.Query<Categoria>(query).ToList();
            }
            return categorias;
        }

        public static List<Dificultad> ObtenerDificultades()
        {
            List<Dificultad> dificultades = new List<Dificultad>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT nombre FROM dificultades";
                dificultades = connection.Query<Dificultad>(query).ToList();
            }
            return dificultades;
        }

        public static List<Pregunta> ObtenerPreguntas(int dificultad, int categoria)
        {
            List<Pregunta> preguntas = new List<Pregunta>();
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
                    preguntas = connection.Query<Pregunta>(query, new { categoria }).ToList();
                    return preguntas;
                }
                else if (categoria == -1)
                {
                    query = "SELECT * FROM preguntas WHERE idDificultad = @dificultad";
                    preguntas = connection.Query<Pregunta>(query, new { dificultad }).ToList();
                    return preguntas;
                }
                else
                {
                    query = "SELECT * FROM preguntas WHERE idCategoria = @categoria AND idDificultad = @dificultad";
                    preguntas = connection.Query<Pregunta>(query, new { dificultad, categoria }).ToList();
                }

                preguntas = connection.Query<Pregunta>(query, new { dificultad, categoria }).ToList();
            }
            return preguntas;
        }

        public static List<Respuesta> ObtenerRespuestas(int idPregunta)
        {
            List<Respuesta> respuestas = new List<Respuesta>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM respuestas WHERE idPregunta = @idPregunta";
                respuestas = connection.Query<Respuesta>(query, new { idPregunta }).ToList();
            }
            return respuestas;
        }
        
        

}