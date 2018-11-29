using System;
using System.Collections.Generic;
using Senai.Aulas.ProjetoFinal.Models;

namespace Senai.Aulas.ProjetoFinal.Interfaces {
    public interface IComentario {
        ComentarioModel Comentar(ComentarioModel comentario);
        List<ComentarioModel> Listar ();
        void Rejeitar(int id);
        void Aprovar(int id);
    }
}