using PrimerProyecto.Models;

public class Juego
{
    public string username { get; set; }
    public int puntajeActual { get; set; }
    public int CantidadPreguntasCorrectas { get; set; }
    public int ContadorNroPreguntaActual { get; set; }
    public Pregunta? preguntaActual { get; set; }
    public List<Pregunta> ListaPreguntas { get; set; }
    public List<Respuesta> ListaRespuestas { get; set; }

    public Juego(string username, int puntajeActual, int CantidadPreguntasCorrectas, int ContadorNroPreguntaActual)
    {
        this.username = username;
        this.puntajeActual = puntajeActual;
        this.CantidadPreguntasCorrectas = CantidadPreguntasCorrectas;
        this.ContadorNroPreguntaActual = ContadorNroPreguntaActual;
        this.ListaPreguntas = new List<Pregunta>();
        this.ListaRespuestas = new List<Respuesta>();
    }

    private void InicializarJuego()
    {
        username = ""; 
        puntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        preguntaActual = null;
        ListaPreguntas = new List<Pregunta>();
        ListaRespuestas = new List<Respuesta>();
    }

    public List<Categoria> ObtenerCategorias()
    {
        List<Categoria> categorias = BD.ObtenerCategorias();
        return categorias;
    }
    
    public List<Dificultad> ObtenerDificultades()
    {
        List<Dificultad> dificultades = BD.ObtenerDificultades();
        return dificultades; 
    }

    public void CargarPartida(string username, int dificultad, int categoria)
    {
        InicializarJuego();
        this.username = username;
        ListaPreguntas = BD.ObtenerPreguntas(dificultad, categoria);
    }

    public Pregunta? ObtenerProximaPregunta()
    {
        if(ListaPreguntas != null && ContadorNroPreguntaActual < ListaPreguntas.Count)
        {
            preguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
            ContadorNroPreguntaActual++;
            return preguntaActual;
        }
        return null;
    }

    public List<Respuesta> ObtenerProximasRespuestas(int idPregunta)
    {
        ListaRespuestas = BD.ObtenerRespuestas(idPregunta);
        return ListaRespuestas;
    }

    public bool VerificarRespuesta(int idRespuesta)
    {
        bool esCorrecta = false;
        foreach(Respuesta respuesta in ListaRespuestas)
        {
            if(respuesta.idRespuesta == idRespuesta && respuesta.correcta)
            {
                esCorrecta = true;
                puntajeActual += 10;
                CantidadPreguntasCorrectas++;
                break;
            }
        } 
        return esCorrecta;
    }
}