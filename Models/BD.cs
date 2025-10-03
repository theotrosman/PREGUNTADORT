using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

public static class BD
{
    // Ajustá el nombre del server si no usás LocalDB.
    private const string connectionString =
        @"Server=(localdb)\MSSQLLocalDB;Database=PreguntadosDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

    private static IDbConnection GetConnection() => new SqlConnection(connectionString);

    public static List<Categoria> ObtenerCategorias()
    {
        using var connection = GetConnection();
        connection.Open();
        const string sql = "SELECT * FROM categoria";
        return connection.Query<Categoria>(sql).ToList();
    }

    public static List<Dificultad> ObtenerDificultades()
    {
        using var connection = GetConnection();
        connection.Open();
        const string sql = "SELECT * FROM dificultades";
        return connection.Query<Dificultad>(sql).ToList();
    }

    public static List<Pregunta> ObtenerPreguntas(int dificultad, int categoria)
    {
        using var connection = GetConnection();
        connection.Open();

        // Construcción dinámica de WHERE
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

        return connection.Query<Pregunta>(sql, parametros).ToList();
    }

    public static List<Respuesta> ObtenerRespuestas(int idPregunta)
    {
        using var connection = GetConnection();
        connection.Open();
        const string sql = "SELECT * FROM respuestas WHERE idPregunta = @idPregunta";
        return connection.Query<Respuesta>(sql, new { idPregunta }).ToList();
    }
}
