using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Aulas.ProjetoFinal.Interfaces;
using Senai.Aulas.ProjetoFinal.Models;
using Senai.Aulas.ProjetoFinal.Repositorio;

namespace Senai.Aulas.ProjetoFinal.Controllers {
    public class UsuarioController : Controller {

        public IUsuario UsuarioRepositorio { get; set; }

        public UsuarioController () {
            UsuarioRepositorio = new UsuarioRepositorioSerializacao ();

        }

        [HttpGet]
        public ActionResult Cadastro () {
            return View ();
        }

        [HttpPost]
        public ActionResult Cadastro (IFormCollection form) {

            UsuarioModel usuarioModel = new UsuarioModel (
                nome: form["nome"],
                email: form["email"],
                senha: form["senha"],
                tipo: false);

            // UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

            UsuarioRepositorio.Cadastrar (usuarioModel);

            ViewBag.Mensagem = "Usuário Cadastrado";

            return RedirectToAction ("login", "Usuario");
        }

        [HttpGet]
        public IActionResult Login () => View ();

        [HttpPost]
        public IActionResult Login (IFormCollection form) {

            //Pega os dados do POST
            UsuarioModel usuario = new UsuarioModel (
                email: form["email"],
                senha: form["senha"]
            )

            ;

            //Verificar se o usuário possuí acesso para realizazr login

            UsuarioModel usuarioModel = UsuarioRepositorio.ValidarLogin (usuario.Email, usuario.Senha);

            if (usuarioModel != null) {
                HttpContext.Session.SetString ("idUsuario", usuarioModel.ID.ToString ());
                 HttpContext.Session.SetString ("NomeUsuario", usuarioModel.Nome.ToString ());
                ViewBag.Mensagem = "Login realizado com sucesso!";

                return RedirectToAction ("Usuario", "Logado"); // implemntar direcionamento para home logada
            } else {
                ViewBag.Mensagem = "Acesso negado!";
            }

            return View ();
        }

        /// <summary>
        /// Lista todos os usuários cadastrados no sistema
        /// </summary>
        /// <returns>A view da listagem de usuário</returns>
        [HttpGet]
        public IActionResult Listar () {
            // UsuarioRepositorio rep = new UsuarioRepositorio();

            //Buscando os dados do rep. e aplicando no view bag
            //ViewBag.Usuarios = rep.Listar();
            ViewData["Usuarios"] = UsuarioRepositorio.Listar ();

            return View ();
        }

        [HttpGet]
        public IActionResult Excluir (int id) {
            UsuarioRepositorio.Excluir (id);

            TempData["Mensagem"] = "Usuário excluído";

            return RedirectToAction ("Listar");
        }

        [HttpGet]
        public IActionResult Editar (int id) {

            if (id == 0) {
                TempData["Mensagem"] = "Informe um id";
                return RedirectToAction ("Listar");
            }

            UsuarioModel usuario = UsuarioRepositorio.ProcurarID (id);

            if (usuario != null) {
                ViewBag.Usuario = usuario;
                return View ();
            }

            TempData["Mensagem"] = "Usuário não encontrado";
            return RedirectToAction ("Listar");
        }

        [HttpPost]
        public IActionResult Editar (IFormCollection form) {

            UsuarioModel usuario = new UsuarioModel (
                id: int.Parse (form["id"]),
                nome: form["nome"],
                email: form["email"],
                senha: form["senha"],
                dataNascimento: DateTime.Parse (form["dataNascimento"])
            );

            UsuarioRepositorio.Editar (usuario);

            TempData["Mensagem"] = "Usuário editado";

            return RedirectToAction ("Listar");
        }
    }
}