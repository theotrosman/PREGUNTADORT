class Juego
{
    public string username { get; set; }
    public int puntajeActual { get; set; }
    public int CantidadPreguntasCorrectas { get; set; }
    public int ContadorNroPreguntaActual { get; set; }
    public pregunta preguntaActual { get; set; }
    public List<Pregunta> ListaPreguntas { get; set; }
    public List<Respuesta> ListaRespuestas { get; set; }

    public Juego(string username, int puntajeActual, int CantidadPreguntasCorrectas, int ContadorNroPreguntaActual)
    {
        this.username = username;
        this.puntajeActual = puntajeActual;
        this.CantidadPreguntasCorrectas = CantidadPreguntasCorrectas;
        this.ContadorNroPreguntaActual = ContadorNroPreguntaActual;
    }

    private void InicializarJuego(string username)
    {
        username = ""; 
        puntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        preguntaActual = null;
        List<pregunta> ListaPreguntas = null;
        List<respuesta> ListaRespuestas = null;
    }

    public List<Categoria>ObtenerCategorias()
    {
        List<Categoria> categorias = BD.ObtenerCategorias();
        return categorias;
    }
    
    public List<Dificultad>ObtenerDificultades()
    {
        List<Dificultad> dificultades = BD.ObtenerDificultades();
        return dificultades; 
    }

    public void CargarPartida(string username, int dificultad, int categoria)
    {
        Juego juego = new Juego(username, puntajeActual, CantidadPreguntasCorrectas, ContadorNroPreguntaActual);
        juego.InicializarJuego(username);
        List<Pregunta> preguntas = BD.ObtenerPreguntas(dificultad, categoria);
    }

    public Pregunta ObtenerProximaPregunta() //CORREGIR ESTA FUNCIÓN 
    {
        Pregunta pregunta = null;
        if(ListaPreguntas != null && ContadorNroPreguntaActual < ListaPreguntas.Count())
        {
            preguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
            pregunta = preguntaActual;
        }
        return pregunta;
    }

    List<Respuesta>ObtenerProximasRespuestas(int idPregunta)
    {
        ListaRespuestas = BD.ObtenerRespuestas(idPregunta);
        return ListaRespuestas;
    }

    public bool VerificarRespuesta(int idRespuesta)
    {
        bool esCorrecta = false;
        foreach(Respuesta respuesta in respuesta)
        {
            if(respuesta.idPregunta == idRespuesta)
            {
                esCorrecta = true;
                puntajeActual+=2;
                CantidadPreguntasCorrectas+=1;
            }
        } 
        ContadorNroPreguntaActual+=1;
        preguntaActual = ObtenerProximaPregunta;
        return esCorrecta;
    }


}