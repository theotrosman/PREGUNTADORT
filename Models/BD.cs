using Microsoft.Data.SqlClient;
using Dapper;


namespace TuProyecto.Models
{   
    public static class BD //Fijarse si los nombres de las tablas van con mayusculas o con en plural
    {
        private static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=PreguntadosDB;Trusted_Connection=True;";

        public List<categoria> ObtenerCategorias()
        {
            List<categoria> categorias = new List<categoria>();
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT nombre FROM categoria";
                categorias = connection.Query<categoria>(query).ToList();
            }
            return categorias; 
        }

        public List<dificultad> ObtenerDificultades()
        {
            List<dificultad> dificultades = new List<dificultad>();
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT nombre FROM dificultades";
                    dificultades = connection.Query<dificultad>(query).ToList();
                }
                return dificultades; 
        }

        public List<pregunta> ObtenerPreguntas(int dificultad, int categoria)
        {
            List<pregunta> preguntas = new List<pregunta>();
            using(SqlConnection connection = new SqlConnection(_connectionString))
                {   
                    //Arreglar este error (antes de traerlo tiene que tener los where) CORREGIR
                    string query = "SELECT enunciado FROM preguntas";
                    preguntas = connection.Query<pregunta>(query).ToList();

                    if(dificultad == -1 && categoria == -1)
                        string query = "SELECT * FROM preguntas"

                    else if(dificultad == -1){
                        string query = "SELECT * FROM preguntas WHERE idDificultades = @dificultad";
                        preguntas = connecition.Query<pregunta>(query, new {dificultad = idDificultad})
                    }

                    else if(categoria == -1){
                        string query = "SELECT * FROM preguntas WHERE idCategoria = @categoria"
                        preguntas = connecition.Query<pregunta>(query, new {categoria = idCategoria})
                    }

                    else if(categoria != -1 && dificultad != -1){
                        string query = "SELECT * FROM preguntas WHERE idCategoria = @categoria AND idDificultad = @dificultad"
                        preguntas = connecition.Query<pregunta>(query, new {dificultad = idDificultad});
                    }
                }

            return preguntas; 
        }

        public List<respuesta> ObtenerRespuestas(int idPregunta)
        {
             List<respuesta> resupestas = new List<respuesta>();
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query =  "SELECT * FROM respuestas WHERE idPregunta = @idPregunta";
                    respuestas = connection.Query<respuesta>(query, new {idpregunta = idpregunta}).ToList();
                }
                return respuestas; 
        }


     

    }

  
}
