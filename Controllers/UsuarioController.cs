using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_Senai.Carfel.Final.Models;
using Projeto_Senai.Carfel.Final.Repositorios;
using Senai.Aulas.ProjetoFinal.Interfaces;
using Senai.Aulas.ProjetoFinal.Models;

namespace Senai.Aulas.ProjetoFinal.Controllers {
    public class UsuarioController : Controller {

        public IUsuario UsuarioRepositorio { get; set; }

        public UsuarioController () {
            UsuarioRepositorio = new UsuarioRepositorio ();
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
                admin: false);

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

            UsuarioModel usuarioModel = UsuarioRepositorio.Login (usuario.Email, usuario.Senha);

            if (usuarioModel != null) {
                HttpContext.Session.SetString ("NomeUsuario", usuarioModel.Nome);
                HttpContext.Session.SetString("TipoUsuario", usuarioModel.Administrador.ToString());
                HttpContext.Session.SetInt32("IdUsuario", usuarioModel.Id);
                ViewBag.Mensagem = "Login realizado com sucesso!";
                   
                return RedirectToAction ("Index", "Home"); // implemntar direcionamento para home logada
                
            } 
                ViewBag.Mensagem = "Acesso negado!";

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
        public IActionResult Editar(int id){

            if(id == 0){
                TempData["Mensagem"] = "Informe um usuário para editar";
                return RedirectToAction("Listar");
            }

            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            UsuarioModel usuario = usuarioRepositorio.BuscarPorId(id);

            if(usuario != null){
                ViewBag.Usuario = usuario;
            } else {
                TempData["Mensagem"] = "Usuário não encontrado";
                return RedirectToAction("Listar");
            }
            
            return View();
        }
    
        [HttpPost]
        public IActionResult Editar(IFormCollection form){
            //Declara um objeto UsuarioModel e atribui os valores do form
            UsuarioModel usuario = new UsuarioModel(
                id: int.Parse(form["id"]),
                nome: form["nome"],
                email: form["email"],
                senha: form["senha"],
                admin: Boolean.Parse(form["admin"])
            );

            //Cria um objeto UsuarioRepositorio e edita
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            usuarioRepositorio.Editar(usuario);

            TempData["Mensagem"] = "Usuário editado";

            return RedirectToAction("Listar");
        }
        [HttpGet]
        public IActionResult Deslogar(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}