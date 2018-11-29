using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Senai.Aulas.ProjetoFinal.Interfaces;
using Senai.Aulas.ProjetoFinal.Models;
namespace Senai.Aulas.ProjetoFinal.Repositorio {
    public class ComentarioRepositorioSerializacao : IComentario {
        private List<ComentarioModel> ComentariosSalvos { get; set; }

        public ComentarioRepositorioSerializacao () { // O método contrutor é uma otima alternativa parar instanciar obj
            // verificando se ja existe um arquivo serealizado...
            if (File.Exists ("Comentarios.dat")) {
                //Ler o arquivo
                ComentariosSalvos = lerArquivoSerializado ();
            } else {

                ComentariosSalvos = new List<ComentarioModel> ();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ComentarioModel> lerArquivoSerializado () {
            //lê os bytes do arquivo
            byte[] bytesSerializados = File.ReadAllBytes ("Comentarios.dat");

            //Cria o fluxo de memoria com os bytes do arquivo serialiazos
            MemoryStream memoria = new MemoryStream (bytesSerializados);

            BinaryFormatter serialiazor = new BinaryFormatter ();

            return (List<ComentarioModel>) (serialiazor.Deserialize (memoria));
        }
        private void EscreverNoArquivo () {
            //serializando a lista com todos os usuários cadastrados
            MemoryStream memoria = new MemoryStream ();
            BinaryFormatter serializadora = new BinaryFormatter ();

            serializadora.Serialize (memoria, ComentariosSalvos);
            //Pegando os bytes salvos na memoria
            byte[] bytes = memoria.ToArray ();
            File.WriteAllBytes ("Comentarios.dat", bytes);
        }
        public void Aprovar (int id) {
            throw new NotImplementedException ();
        }

        public ComentarioModel Comentar (ComentarioModel comentario) {
            comentario.ID = ComentariosSalvos.Count +1;

            ComentariosSalvos.Add(comentario);
            EscreverNoArquivo();
            return comentario;
            
        }

        public List<ComentarioModel> Listar () {
            throw new NotImplementedException ();
        }

        public void Rejeitar (int id) {
        ComentarioModel comentarioBuscado = BuscarPorId(id);
        if (comentarioBuscado != null)
        {
            ComentariosSalvos.Remove(comentarioBuscado);
            // att do arquivo
            EscreverNoArquivo();
        }
        }
                public ComentarioModel BuscarPorId (int id) {
            // Percorre todos os usuarios buscando pelo id 
            foreach (ComentarioModel comentario in ComentariosSalvos) {
                if (id == comentario.ID) {
                    return comentario ;
                }
            }
            return null;
        }

    }
}