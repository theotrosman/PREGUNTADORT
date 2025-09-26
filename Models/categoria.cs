public class Categoria 
{
    public int idCategoria { get; set; }
    public string nombre { get; set; }
    public string foto { get; set; }

    public Categoria(int idCategoria, string nombre, string foto)
    {
        this.idCategoria = idCategoria;
        this.nombre = nombre;
        this.foto = foto;
    }
}