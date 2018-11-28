using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Senai.Aulas.ProjetoFinal.Interfaces;
using Senai.Aulas.ProjetoFinal.Models;

namespace Senai.Aulas.ProjetoFinal.Repositorio
{
        public class UsuarioRepositorioSerializacao : IUsuario {

        /// <summary>
        /// Lista que armazena todos os usuários cadastrados no sistema
        /// </summary>
        /// <value></value>
        private List<UsuarioModel> UsuarioSalvos { get; set; }

        public UsuarioRepositorioSerializacao () { // O método contrutor é uma otima alternativa parar instanciar obj
            // verificando se ja existe um arquivo serealizado...
            if (File.Exists ("usuarios.dat")) {
                //Ler o arquivo
                UsuarioSalvos = lerArquivoSerializado ();
            } else {

                UsuarioSalvos = new List<UsuarioModel> ();
            }
        }

        public UsuarioModel BuscarPorEmailESenha (string email, string senha) {
            throw new System.NotImplementedException ();
        }

        public UsuarioModel BuscarPorId (int id) {
            // Percorre todos os usuarios buscando pelo id 
            foreach (UsuarioModel usuario in UsuarioSalvos) {
                if (id == usuario.ID) {
                    return usuario;
                }
            }
            return null;
        }

        public UsuarioModel Cadastrar (UsuarioModel usuario) {
            usuario.ID = UsuarioSalvos.Count + 1;
            // Adiciona o usuário na lista
            UsuarioSalvos.Add (usuario);

            EscreverNoArquivo ();

            return usuario;
        }

        private void EscreverNoArquivo () {
            //serializando a lista com todos os usuários cadastrados
            MemoryStream memoria = new MemoryStream ();
            BinaryFormatter serializadora = new BinaryFormatter ();

            serializadora.Serialize (memoria, UsuarioSalvos);
            //Pegando os bytes salvos na memoria
            byte[] bytes = memoria.ToArray ();
            File.WriteAllBytes ("usuarios.dat", bytes);
        }

        public UsuarioModel Editar (UsuarioModel usuario) {
            throw new System.NotImplementedException ();
        }

        public void Excluir (int id) {
            // Buscando o usuario pelo id
            UsuarioModel usuarioBuscado = BuscarPorId (id);

            if (usuarioBuscado != null) {
                UsuarioSalvos.Remove (usuarioBuscado);
                //temos de atualizar o arquivo com a lista sem objeto
                EscreverNoArquivo ();
            }
        }
        public List<UsuarioModel> Listar () {
            return UsuarioSalvos;
        }
        public List<UsuarioModel> lerArquivoSerializado () {
            //lê os bytes do arquivo
            byte[] bytesSerializados = File.ReadAllBytes ("usuarios.dat");

            //Cria o fluxo de memoria com os bytes do arquivo serialiazos
            MemoryStream memoria = new MemoryStream (bytesSerializados);

            BinaryFormatter serialiazor = new BinaryFormatter ();

            return (List<UsuarioModel>) (serialiazor.Deserialize (memoria));
        }

        public UsuarioModel ValidarLogin(string email, string senha)
        {
            throw new NotImplementedException();
        }

        public UsuarioModel ProcurarID(int id)
        {
            throw new NotImplementedException();
        }
    }
}