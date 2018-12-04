using System;
using System.Collections.Generic;
using Projeto_Senai.Carfel.Final.Models;
using Senai.Aulas.ProjetoFinal.Models;

namespace Senai.Aulas.ProjetoFinal.Interfaces {
    public interface IUsuario {
        UsuarioModel Cadastrar (UsuarioModel usuario);
         List<UsuarioModel> Listar();
         void Excluir(int id);
         UsuarioModel Editar(UsuarioModel usuario);
         UsuarioModel BuscarPorEmailESenha(string email, string senha);
         UsuarioModel BuscarPorId(int id);
         UsuarioModel Login (string email, string senha);
    }
}