using System;

namespace APIGK2V.Entidades
{
    public class Temporada : EntidadeBase
    {
        public int CodigoAdministrador { get; set; }
        public int Fase { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DatalimiteAposta { get; set; }
        public DateTime DataFinal { get; set; } 
        public int NumeroTemporada { get; set; }
        public int UsuarioVencedor { get; set; }
        
    }
}