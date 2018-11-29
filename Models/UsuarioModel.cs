using System;

namespace Senai.Aulas.ProjetoFinal.Models {

    [Serializable]
    public class UsuarioModel {
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DataNascimento { get; set; }
        private Boolean Admin { get; set; }

        public UsuarioModel () {

        }
        public UsuarioModel (string email, string senha) {
            this.Email = email;
            this.Senha = senha;
        }
        public UsuarioModel (string nome, string email, string senha, Boolean tipo) {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Admin = tipo;
        }

        public UsuarioModel (string nome, string email, string senha, DateTime dataNascimento) {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.DataNascimento = dataNascimento;
        }

        public UsuarioModel (int id, string nome, string email, string senha, DateTime dataNascimento) {
            this.ID = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.DataNascimento = dataNascimento;
        }

    }
}