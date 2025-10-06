public class Juego
{
    public string username { get; set; }
    public int puntajeActual { get; set; }
    public int CantidadPreguntasCorrectas { get; set; }
    public int ContadorNroPreguntaActual { get; set; }
    public Preguntas? preguntaActual { get; set; }
    public List<Preguntas> ListaPreguntas { get; set; }
    public List<Respuestas> ListaRespuestas { get; set; }

    public Juego(string username, int puntajeActual, int CantidadPreguntasCorrectas, int ContadorNroPreguntaActual)
    {
        this.username = username;
        this.puntajeActual = puntajeActual;
        this.CantidadPreguntasCorrectas = CantidadPreguntasCorrectas;
        this.ContadorNroPreguntaActual = ContadorNroPreguntaActual;
        this.ListaPreguntas = new List<Preguntas>();
        this.ListaRespuestas = new List<Respuestas>();
    }

    private void InicializarJuego()
    {
        username = ""; 
        puntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        preguntaActual = null;
        ListaPreguntas = new List<Preguntas>();
        ListaRespuestas = new List<Respuestas>();
    }

    public List<Categorias> ObtenerCategorias()
    {
        List<Categorias> categorias = BD.ObtenerCategorias();
        return categorias;
    }
    
    public List<Dificultades> ObtenerDificultades()
    {
        List<Dificultades> dificultades = BD.ObtenerDificultades();
        return dificultades; 
    }

    public void CargarPartida(string username, int dificultad, int categoria)
    {
        InicializarJuego();
        this.username = username;
        ListaPreguntas = BD.ObtenerPreguntas(dificultad, categoria);
    }

    public Preguntas? ObtenerProximaPregunta()
    {
        if(ListaPreguntas != null && ContadorNroPreguntaActual < ListaPreguntas.Count)
        {
            preguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
            ContadorNroPreguntaActual++;
            return preguntaActual;
        }
        return null;
    }

    public List<Respuestas> ObtenerProximasRespuestas(int idPregunta)
    {
        ListaRespuestas = BD.ObtenerRespuestas(idPregunta);
        return ListaRespuestas;
    }

    public bool VerificarRespuesta(int idRespuesta)
    {
        bool esCorrecta = false;
        foreach(Respuestas respuesta in ListaRespuestas)
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