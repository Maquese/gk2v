using System.Collections.Generic;

namespace APIGK2V.Entidades
{
    public class Quiz : EntidadeBase
    {
        public string Pergunta { get; set; }
        public string  OpcaoCerta { get; set; }
        public IList<OpcaoQuiz> Opcoes { get; set; }

        public Quiz()
        {
            Opcoes = new List<OpcaoQuiz>();
        }
    }
}