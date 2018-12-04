using System;
using System.Collections.Generic;
using Senai.Aulas.ProjetoFinal.Models;

namespace Senai.Aulas.ProjetoFinal.Interfaces {
    public interface IComentario {
        ComentarioModel Comentar (ComentarioModel comentario);
        List<ComentarioModel> Listar ();
        ComentarioModel Editar (ComentarioModel id);
        void Excluir (int id);
        ComentarioModel BuscarPorId (int id);
    }
}