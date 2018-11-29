using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Aulas.ProjetoFinal.Interfaces;
using Senai.Aulas.ProjetoFinal.Models;
using Senai.Aulas.ProjetoFinal.Repositorio;
namespace Senai.Aulas.ProjetoFinal.Controllers {
    public class ComentarioController : Controller {
        public IComentario ComentarioRepositorio { get; set; }

        public ComentarioController () {
            ComentarioRepositorio = new ComentarioRepositorioSerializacao ();
        }

        [HttpGet]
        public ActionResult Comentar () {
            return View ();
        }

        [HttpPost]
        public ActionResult Comentar (IFormCollection form) {
           string nome =  HttpContext.Session.GetString ("NomeUsuario");

            ComentarioModel comentarioModel = new ComentarioModel (
                nome: nome,
                comentario: form["Comentario"],
                data: DateTime.Now
            );
            ComentarioRepositorio.Comentar (comentarioModel);
            return RedirectToAction ("Index", "Home");
        }
        [HttpGet]
        public IActionResult Listar(){
            ViewData["Comentarios"] = ComentarioRepositorio.Listar();
            return View();
        }
        [HttpGet]
        public IActionResult Editar(int id){
            if (id==0)
            {
                TempData["Mensagem"] = "informe um id";
                return RedirectToAction("Listar");
            }
            // ComentarioModel comentario = ComentarioRepositorioSerializacao.BuscarPorId Parei Aqui
        }
    }
}