using Microsoft.AspNetCore.Mvc;

namespace Senai.Aulas.ProjetoFinal.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(){
            return View();
        }
    }
}