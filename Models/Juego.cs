class Juego{
    public string username {get;set;}
    public int puntuajeActual {get;set;}
    public int CantidadPreguntasCorrectas {get;set;}
    public int ContadorNroPreguntaActual {get;set;}
    public pregunta preguntaActual {get;set;}
    public List<pregunta> ListaPreguntas {get;set;}
    public List<respuesta> ListaRespuestas {get;set;}

    public Juego(string username, int puntuajeActual, int CantidadPreguntasCorrectas, int ContadorNroPreguntaActual){
        this.username = username;
        this.puntuajeActual = puntuajeActual;
        this.CantidadPreguntasCorrectas = CantidadPreguntasCorrectas; 
        this.ContadorNroPreguntaActual = ContadorNroPreguntaActual;
    }

    private void InicializarJuego(){
        username = "";
        puntuajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        preguntaActual = null;
        List<pregunta> ListaPreguntas = null;
        List<respuesta> ListaRespuestas = null;
    }

}