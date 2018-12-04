using System;
using System.Collections.Generic;
using System.IO;
using Projeto_Senai.Carfel.Final.Models;
using Senai.Aulas.ProjetoFinal.Interfaces;
using Senai.Aulas.ProjetoFinal.Models;
namespace Senai.Projetos.CarfelFinal.Repositorio {
    public class ComentarioRepositorio : IComentario {
        public ComentarioModel Comentar (ComentarioModel comentario) {

            if (File.Exists ("comentario.csv")) {

                comentario.ID = System.IO.File.ReadAllLines ("comentario.csv").Length + 1;
            } else {
                comentario.ID = 1;
            }

            using (StreamWriter sw = new StreamWriter ("comentario.csv", true)) {
                sw.WriteLine ($"{comentario.ID};{comentario.Nome};{comentario.DataComentario};{comentario.Comentario};{comentario.Aprovado};{comentario.IDusuario}");
            }

            return comentario;

        }

        public ComentarioModel Editar (ComentarioModel comentario) {
            string[] linhas = System.IO.File.ReadAllLines ("comentario.csv");
            for (int i = 0; i < linhas.Length; i++) {
                if (string.IsNullOrEmpty (linhas[i])) {
                    continue;
                }
                string[] dados = linhas[i].Split (";");
                if (dados[0] == comentario.ID.ToString ()) {
                    linhas[i] = $"{comentario.ID};{comentario.Nome};{comentario.DataComentario};{comentario.Comentario};{comentario.Aprovado};{comentario.IDusuario}";
                    break;
                }
            }
            System.IO.File.WriteAllLines ("comentario.csv", linhas);

            return comentario;
        }

        public void Excluir (int id) {
            //Abre o stream de leitura do arquivo
            string[] linhas = File.ReadAllLines ("comentario.csv");

            //Lê cada registro no CSV
            for (int i = 0; i < linhas.Length; i++) {
                //Separa os dados da linha
                string[] dadosDaLinha = linhas[i].Split (';');

                if (id.ToString () == dadosDaLinha[0]) {
                    linhas[i] = "";
                    break;
                }
            }

            File.WriteAllLines ("comentario.csv", linhas);
        }

        public List<ComentarioModel> Listar () => CarregarDoCSV ();

        private List<ComentarioModel> CarregarDoCSV () {
            List<ComentarioModel> lsComentario = new List<ComentarioModel> ();

            //Abre o stream de leitura do arquivo
            string[] linhas = File.ReadAllLines ("comentario.csv");

            //Lê cada registro no CSV
            foreach (string linha in linhas) {
                //Verificando se é uma linha vazia
                if (string.IsNullOrEmpty (linha)) {
                    continue; //Pula para o próximo registro do laço
                }

                //Separa os dados da linha
                string[] dadosDaLinha = linha.Split (';');

                //Cria o objeto com os dados da linha do CSV
                ComentarioModel comentario = new ComentarioModel (
                    id: int.Parse (dadosDaLinha[0]),
                    nome: dadosDaLinha[1],
                    data: DateTime.Parse (dadosDaLinha[2]),
                    comentario: dadosDaLinha[3],
                    aprovado: Boolean.Parse (dadosDaLinha[4]),
                    idUsuario: int.Parse(dadosDaLinha[5])
                );

                //Adicionando o usuário na lista
                lsComentario.Add (comentario);
            }
            return lsComentario;
        }
        public ComentarioModel BuscarPorId (int id) {
            string[] linhas = System.IO.File.ReadAllLines ("comentario.csv");

            for (int i = 0; i < linhas.Length; i++) {
                if (string.IsNullOrEmpty (linhas[i])) {
                    continue;
                }

                string[] dados = linhas[i].Split (';');

                if (dados[0] == id.ToString ()) {
                ComentarioModel comentario = new ComentarioModel (
                    id: int.Parse (dados[0]),
                    nome: dados[1],
                    data: DateTime.Parse (dados[2]),
                    comentario: dados[3],
                    aprovado: Boolean.Parse (dados[4]),
                    idUsuario: int.Parse(dados[5])
                    );

                    return comentario;
                }
            }
            return null;
        }

    }
}