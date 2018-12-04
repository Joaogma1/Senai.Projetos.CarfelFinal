using System;
using Microsoft.Extensions.Primitives;
using Projeto_Senai.Carfel.Final.Models;

namespace Senai.Aulas.ProjetoFinal.Models {
    [Serializable]
    public class ComentarioModel {

        public int ID { get; set; }

        public String Nome { get; set; }

        public string Comentario { get; set; }
        public int IDusuario { get; set; }

        public DateTime DataComentario { get; set; }
        public Boolean Aprovado { get; private set; }

        public ComentarioModel (int id, string nome, string comentario, DateTime data, Boolean aprovado, int idUsuario) {
            this.ID = id;
            this.Nome = nome;
            this.Comentario = comentario;
            this.DataComentario = data;
            this.Aprovado = aprovado;
            this.IDusuario = idUsuario;
        }
        

        public ComentarioModel(string nome, string comentario, bool aprovado, int idUsuario)
        {
            this.Nome = nome;
            this.Comentario = comentario;
            this.Aprovado = aprovado;
            this.DataComentario = DateTime.Now;
            this.IDusuario = idUsuario;
        }
    }
}