using System;

namespace APIGK2V.ViewModels
{
    public class UsuarioViewModel
    {
        public string id { get; set; }     
        public string nome { get; set; }
            public string email { get; set; }
            public string senha { get; set; }
            public DateTime dataNascimento { get; set; }
            public int TipoUsuario { get; set; }
    }
    
}