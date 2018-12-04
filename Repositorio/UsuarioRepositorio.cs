using System;
using System.Collections.Generic;
using System.IO;
using Projeto_Senai.Carfel.Final.Models;
using Senai.Aulas.ProjetoFinal.Interfaces;
using Senai.Aulas.ProjetoFinal.Models;

namespace Projeto_Senai.Carfel.Final.Repositorios {
    public class UsuarioRepositorio : IUsuario {

        public UsuarioModel Login (string email, string senha) {
            {
            using (StreamReader sr = new StreamReader ("usuarios.csv")) {
                while (!sr.EndOfStream) {
                    var linha = sr.ReadLine ();

                    if (string.IsNullOrEmpty(linha))
                    {
                        continue;
                    }

                    string[] dados = linha.Split (";");

                    if (dados[2] == email && dados[3] == senha) {
                        UsuarioModel usuario = new UsuarioModel(
                            id: int.Parse (dados[0]),
                            nome: dados[1],
                            email: dados[2],
                            senha: dados[3],
                            admin: Boolean.Parse (dados[4])
                        );

                        return usuario;
                    }
                }
            }

            return null;
        }
        }

        /// <summary>
        /// Excluir um registro do CSV
        /// </summary>
        /// <param name="id">O ID do usuário cadastrado</param>
        public void Excluir (int id) {
            //Abre o stream de leitura do arquivo
            string[] linhas = File.ReadAllLines ("usuarios.csv");

            //Lê cada registro no CSV
            for (int i = 0; i < linhas.Length; i++) {
                //Separa os dados da linha
                string[] dadosDaLinha = linhas[i].Split (';');

                if (id.ToString () == dadosDaLinha[0]) {
                    linhas[i] = "";
                    break;
                }
            }

            File.WriteAllLines ("usuarios.csv", linhas);
        }

        /// <summary>
        /// Carrega a lista de usuários com os dados do CSV
        /// </summary>
        private List<UsuarioModel> CarregarDoCSV () {
            List<UsuarioModel> lsUsuarios = new List<UsuarioModel> ();

            //Abre o stream de leitura do arquivo
            string[] linhas = File.ReadAllLines ("usuarios.csv");

            //Lê cada registro no CSV
            foreach (string linha in linhas) {
                //Verificando se é uma linha vazia
                if (string.IsNullOrEmpty (linha)) {
                    continue; //Pula para o próximo registro do laço
                }

                //Separa os dados da linha
                string[] dadosDaLinha = linha.Split (';');

                //Cria o objeto com os dados da linha do CSV
                UsuarioModel usuario = new UsuarioModel (
                    id: int.Parse (dadosDaLinha[0]),
                    nome: dadosDaLinha[1],
                    email: dadosDaLinha[2],
                    senha: dadosDaLinha[3],
                    admin: Boolean.Parse (dadosDaLinha[4])
                );

                //Adicionando o usuário na lista
                lsUsuarios.Add (usuario);
            }
            return lsUsuarios;
        }

        public UsuarioModel Cadastrar (UsuarioModel usuario) {
            if (File.Exists ("usuarios.csv")) {

                usuario.Id = System.IO.File.ReadAllLines ("usuarios.csv").Length + 1;
            } else {
                usuario.Id = 1;
            }

            using (StreamWriter sw = new StreamWriter ("usuarios.csv", true)) {
                sw.WriteLine ($"{usuario.Id};{usuario.Nome};{usuario.Email};{usuario.Senha};{usuario.Administrador}");
            }

            return usuario;
        }

        List<UsuarioModel> IUsuario.Listar () => CarregarDoCSV ();

        public UsuarioModel Editar (UsuarioModel usuario) {
            string[] linhas = System.IO.File.ReadAllLines ("usuarios.csv");

            for (int i = 0; i < linhas.Length; i++) {
                if (string.IsNullOrEmpty (linhas[i])) {
                    continue;
                }

                string[] dados = linhas[i].Split (";");

                if (dados[0] == usuario.Id.ToString()) {
                    linhas[i] = $"{usuario.Id};{usuario.Nome};{usuario.Email};{usuario.Senha};{usuario.Administrador}";
                    break;
                }
            }

            System.IO.File.WriteAllLines ("usuarios.csv", linhas);

            return usuario;
        }

        UsuarioModel IUsuario.BuscarPorEmailESenha (string email, string senha) {
            List<UsuarioModel> usuariosCadastrados = CarregarDoCSV ();

            //Percorro cada usuário da lista do CSV...
            foreach (UsuarioModel usuario in usuariosCadastrados) {
                if (usuario.Email == email && usuario.Senha == senha) {
                    return usuario;
                }
            }

            //Caso  sistema não encontre nenhuma combinação de email e senha retorna nulls
            return null;
        }

         public UsuarioModel BuscarPorId (int id) {
            string[] linhas = System.IO.File.ReadAllLines ("usuarios.csv");

            for (int i = 0; i < linhas.Length; i++) {
                if (string.IsNullOrEmpty (linhas[i])) {
                    continue;
                }

                string[] dados = linhas[i].Split (';');

                if (dados[0] == id.ToString ()) {
                    UsuarioModel usuario = new UsuarioModel (
                        id: int.Parse (dados[0]),
                        nome: dados[1],
                        email: dados[2],
                        senha: dados[3],
                        admin: Boolean.Parse ((dados[4]))
                    );

                    return usuario;
                }
            }

            return null;
        }
    }
}