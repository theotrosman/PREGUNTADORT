using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PrimerProyecto.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private Juego? juegoActual;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("Index");
    }
    
    public IActionResult ConfigurarJuego()
    {
        ViewBag.categorias = BD.ObtenerCategorias();
        ViewBag.dificultades = BD.ObtenerDificultades();
        return View("ConfigurarJuego");
    }
    
    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        juegoActual = new Juego(username, 0, 0, 0);
        juegoActual.CargarPartida(username, dificultad, categoria);
        return RedirectToAction("Jugar");
    }
    
    public IActionResult Jugar()
    {
        if (juegoActual == null)
        {
            return RedirectToAction("ConfigurarJuego");
        }
        
        Preguntas? pregunta = juegoActual.ObtenerProximaPregunta();
        if (pregunta == null)
        {
            return View("Fin", new { puntaje = juegoActual.puntajeActual });
        }

        List<Respuestas> respuestas = juegoActual.ObtenerProximasRespuestas(pregunta.idPregunta);

        ViewBag.Pregunta = pregunta;
        ViewBag.Respuestas = respuestas;
        return View("Jugar");
    }

    [HttpPost] 
    public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta)
    {
        if (juegoActual == null)
            return RedirectToAction("ConfigurarJuego");

        bool esCorrecta = juegoActual.VerificarRespuesta(idRespuesta);

        ViewBag.EsCorrecta = esCorrecta;
    
        List<Respuestas> todas = juegoActual.ListaRespuestas;
        Respuestas? correcta = todas.FirstOrDefault(r => r.correcta);
        ViewBag.RespuestaCorrecta = correcta?.contenido;

        return View("Respuesta");
    }
}