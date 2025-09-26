using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PrimerProyecto.Models;

namespace PrimerProyecto.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("Index.html");
    }
    public IActionResult ConfigurarJuego()
    {
        ViewBag.categorias;
        ViewBag.dificultades;
        return View("ConfigurarJuego"); //PASAR LAS VIEWBAG DE CATEGORIAS Y DIFICULTADES EN LA VIEW Y ESO
    }

    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        /*
        Recibe el username, dificultad y categoría elegidas por el usuario, invoca al método CargarPartida de la clase Juego y se dirige a la acción Jugar.
Ayuda: return RedirectToAction(“Jugar”);
*/
    }
    public IActionResult Jugar()
    {
        /*
        Carga en ViewBag todo lo necesario para mostrar la pregunta actual con sus respectivas respuestas (que proviene del método ObtenerProximaPregunta. 
        Si ya no hay más preguntas disponibles, retorna la view Fin. 
        Si el método retorna una pregunta, invoca a ObtenerProximasRespuestas de la clase Juego guardando estos datos en ViewBag y retorna la view Juego.
        */
    }
    [HttpPost] public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta)
    {
        /*
        Recibe el id de la respuesta elegida, invoca al método VerificarRespuesta de la clase Juego y retorna la view Respuesta, enviando por ViewBag si fue correcta o no. 
        (Como opcional, podés enviar también cuál era la respuesta correcta).
        */
    }
}
