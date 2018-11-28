using System;
using System.Collections.Generic;

namespace APIGK2V.Entidades
{
    public class Temporada : EntidadeBase
    {
        public int CodigoAdministrador { get; set; }
        public int FaseInicial { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; } 
        public int UsuarioVencedor { get; set; }
        public bool TimesMesmaEpoca {get;set;}
        public string Nome { get; set; }
        public List<Jogo> Jogos { get; set; }
   
        public Temporada()
        {
            Jogos = new List<Jogo>();
        }
    }
}