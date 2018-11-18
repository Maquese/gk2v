using System;

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
   
        
    }
}