using System;

namespace Senai.Aulas.ProjetoFinal.Models {
    [Serializable]
    public class ComentarioModel {
        public int ID { get; set; }

        public UsuarioModel Nome { get; set; }

        public string Comentario { get; set; }

        public DateTime DataComentario { get; set; }
        public Boolean Aprovado { get; private set; }

        public ComentarioModel (int id, UsuarioModel nome, string comentario, DateTime data) {
            this.ID = id;
            this.Nome = nome;
            this.Comentario = comentario;
            this.DataComentario = data;
        }
        public ComentarioModel(UsuarioModel nome, string comentario){
            this.Nome = nome;
            this.Comentario = comentario;
        }
        // public ComentarioModel (int id, UsuarioModel nome, string comentario, DateTime data) {
        //     this.ID = id;
        //     this.Nome = nome;
        //     this.Comentario = comentario;
        //     this.DataComentario = data;
        //     this.Aprovado = 
        // }
    }
}