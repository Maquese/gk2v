using System;
using System.Collections.Generic;
using APIGK2V.Enum;

namespace APIGK2V.Entidades
{
    public class Usuario : EntidadeBase
    {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public DateTime DataNascimento { get; set; }
            public TipoUsuario TipoUsuario { get; set; }
            public IList<RespostaQuiz> RespostasQuiz { get; set; }
            public IList<Aposta> Apostas { get; set; }

            public int Pontuacao { get; set; }

            public Usuario()
            {
                RespostasQuiz = new List<RespostaQuiz>();
                Apostas = new List<Aposta>();
            }
    }
    
}