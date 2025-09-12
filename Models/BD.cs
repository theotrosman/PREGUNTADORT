using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;


namespace TuProyecto.Models
{
    public static class BD
    {
        private static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=PreguntadosDB;Trusted_Connection=True;";

        // Método para obtener preguntas con sus respuestas
        public static List<Pregunta> GetPreguntas(int cantidad = 5)
        {
            List<Pregunta> preguntas = new List<Pregunta>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = $@"
                    SELECT TOP {cantidad} p.IdPregunta, p.Texto, r.IdRespuesta, r.Texto AS RespuestaTexto, r.EsCorrecta
                    FROM Preguntas p
                    INNER JOIN Respuestas r ON p.IdPregunta = r.IdPregunta
                    ORDER BY NEWID()"; 

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    Dictionary<int, Pregunta> mapPreguntas = new Dictionary<int, Pregunta>();

                    while (dr.Read())
                    {
                        int idPregunta = Convert.ToInt32(dr["IdPregunta"]);

                        if (!mapPreguntas.ContainsKey(idPregunta))
                        {
                            mapPreguntas[idPregunta] = new Pregunta
                            {
                                IdPregunta = idPregunta,
                                Texto = dr["Texto"].ToString(),
                                Respuestas = new List<Respuesta>()
                            };
                        }

                        mapPreguntas[idPregunta].Respuestas.Add(new Respuesta
                        {
                            IdRespuesta = Convert.ToInt32(dr["IdRespuesta"]),
                            Texto = dr["RespuestaTexto"].ToString(),
                            EsCorrecta = Convert.ToBoolean(dr["EsCorrecta"])
                        });
                    }

                    preguntas.AddRange(mapPreguntas.Values);
                }
            }

            return preguntas;
        }
    }
    public class Pregunta
    {
        public int IdPregunta { get; set; }
        public string Texto { get; set; }
        public List<Respuesta> Respuestas { get; set; }
    }

    public class Respuesta
    {
        public int IdRespuesta { get; set; }
        public string Texto { get; set; }
        public bool EsCorrecta { get; set; }
    }
}
