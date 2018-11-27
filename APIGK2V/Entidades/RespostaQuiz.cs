using System;

namespace APIGK2V.Entidades
{
    public class RespostaQuiz
    {
        public string IdQuiz { get; set; }
        public DateTime DataResposta { get; set; }
        public bool Acertou { get; set; }
    }
}