using System;
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
    }
    
}