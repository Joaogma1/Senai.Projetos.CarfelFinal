using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Aulas.ProjetoFinal.Interfaces;
using Senai.Aulas.ProjetoFinal.Models;
using Senai.Projetos.CarfelFinal.Repositorio;
namespace Senai.Aulas.ProjetoFinal.Controllers {
    public class ComentarioController : Controller {
        public IComentario ComentarioRepositorio { get; set; }

        public ComentarioController () => ComentarioRepositorio = new ComentarioRepositorio ();

        [HttpGet]
        public ActionResult Comentar () {
            return View ();
        }

        [HttpPost]
        public ActionResult Comentar (IFormCollection form) {

            string nomeUsuarioLogado = HttpContext.Session.GetString ("NomeUsuario");
            int idusuario = HttpContext.Session.GetInt32("IdUsuario").Value;
            ComentarioModel comentarioModel = new ComentarioModel (
                nome: nomeUsuarioLogado,
                comentario: form["Comentario"],
                aprovado: false,
                idUsuario: idusuario);

            ComentarioRepositorio.Comentar (comentarioModel);
            
            return View ();
        }

        [HttpGet]
        public IActionResult Listar () {

            ViewData["Comentarios"] = ComentarioRepositorio.Listar ();

            return View ();
        }

        [HttpGet]
        public IActionResult Excluir (int id) {
            ComentarioRepositorio.Excluir (id);
 
            TempData["Mensagem"] = "Usuário excluído";

            return RedirectToAction ("Listar");
        }

        [HttpGet]
        public IActionResult Editar (int id) {

            if (id == 0) {
                TempData["Mensagem"] = "Informe um usuário para editar";
                return RedirectToAction ("Listar");
            }

            ComentarioRepositorio comentarioRepositorio = new ComentarioRepositorio ();
            ComentarioModel usuario = ComentarioRepositorio.BuscarPorId (id);

            if (usuario != null) {
                ViewBag.Usuario = usuario;
            } else {
                TempData["Mensagem"] = "Usuário não encontrado";
                return RedirectToAction ("Listar");
            }
            return View ();
        }

        [HttpPost]
        public IActionResult Editar (IFormCollection form) {
            //Declara um objeto ComentarioModel e atribui os valores do form
            ComentarioModel usuario = new ComentarioModel (
                id: int.Parse (form["id"]),
                nome: form["nome"],
                comentario: form["comentario"],
                data: DateTime.Parse (form["data"]),
                aprovado: Boolean.Parse (form["aprovado"]),
                idUsuario: int.Parse(form["idUsuario"])
            );

            //Cria um objeto ComentarioRepositorio e edita
            ComentarioRepositorio ComentarioRepositorio = new ComentarioRepositorio ();
            ComentarioRepositorio.Editar (usuario);

            TempData["Mensagem"] = "Usuário editado";

            return RedirectToAction ("Listar");
        }

    }
}