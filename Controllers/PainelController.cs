using Microsoft.AspNetCore.Mvc;

namespace Senai.Projetos.CarfelFinal.Controllers
{
    public class PainelController: Controller
    {
        [HttpGet]
        public IActionResult Painel(){
            return View();
        }
        
    }
}