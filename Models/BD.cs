using Microsoft.Data.SqlClient;
using Dapper;


namespace TuProyecto.Models
{
    public static class BD
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
                    //Arreglar este error (antes de traerlo tiene que tener los where)
                    string query = "SELECT enunciado FROM preguntas";
                    preguntas = connection.Query<pregunta>(query).ToList();
                    if(dificultad == -1){
                        string query = "SELECT preguntas.enunciado, dificultades.nombre FROM preguntas INNER JOIN dificultades ON preguntas.idDificultad = dificultades.idDificultad ";
                    }
                }
            return preguntas; 
        }


     

    }

  
}
