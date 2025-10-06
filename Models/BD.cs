using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

public static class BD
{
private static string _connectionString = @"Server=localhost;DataBase=PreguntadOrt;Integrated Security=True;TrustServerCertificate=True;";

    private static IDbConnection GetConnection() => new SqlConnection(_connectionString);

    public static List<Categorias> ObtenerCategorias()
    {
        using var connection = GetConnection();
        connection.Open();
        const string sql = "SELECT * FROM categoria";
        return connection.Query<Categorias>(sql).ToList();
    }

    public static List<Dificultades> ObtenerDificultades()
    {
        using var connection = GetConnection();
        connection.Open();
        const string sql = "SELECT * FROM dificultades";
        return connection.Query<Dificultades>(sql).ToList();
    }

    public static List<Pregunta> ObtenerPreguntas(int dificultad, int categoria)
    {
        using var connection = GetConnection();
        connection.Open();
        var condiciones = new List<string>();
        var parametros = new DynamicParameters();
        if (categoria != -1)
        {
            condiciones.Add("idCategoria = @categoria");
            parametros.Add("categoria", categoria);
        }
        if (dificultad != -1)
        {
            condiciones.Add("idDificultad = @dificultad");
            parametros.Add("dificultad", dificultad);
        }
        var sql = "SELECT * FROM preguntas";
        if (condiciones.Any())
            sql += " WHERE " + string.Join(" AND ", condiciones);
        return connection.Query<Preguntas>(sql, parametros).ToList();
    }
    
    public static List<Respuestas> ObtenerRespuestas(int idPregunta)
    {
        using var connection = GetConnection();
        connection.Open();
        const string sql = "SELECT * FROM respuestas WHERE idPregunta = @idPregunta";
        return connection.Query<Respuestas>(sql, new { idPregunta }).ToList();
    }
}
