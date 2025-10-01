using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PrimerProyecto.Models;

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
        ViewBag.Categorias = BD.ObtenerCategorias();
        ViewBag.Dificultades = BD.ObtenerDificultades();
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
        
        Pregunta? pregunta = juegoActual.ObtenerProximaPregunta();
        if (pregunta == null)
        {
            return View("Fin", new { puntaje = juegoActual.puntajeActual });
        }

        List<Respuesta> respuestas = juegoActual.ObtenerProximasRespuestas(pregunta.idPregunta);

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
    
        List<Respuesta> todas = juegoActual.ListaRespuestas;
        Respuesta? correcta = todas.FirstOrDefault(r => r.correcta);
        ViewBag.RespuestaCorrecta = correcta?.contenido;

        return View("Respuesta");
    }
}