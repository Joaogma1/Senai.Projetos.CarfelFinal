using System;
using System.Collections.Generic;
using Senai.Aulas.ProjetoFinal.Models;

namespace Senai.Aulas.ProjetoFinal.Interfaces {
    public interface IUsuario {
        UsuarioModel Cadastrar (UsuarioModel usuario);
        List<UsuarioModel> Listar ();
        void Excluir (int id);
        UsuarioModel Editar (UsuarioModel usuario);
        UsuarioModel ValidarLogin (string email, string senha);
        UsuarioModel ProcurarID (int id);
    }
}