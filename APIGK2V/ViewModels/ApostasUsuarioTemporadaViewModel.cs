using System.Collections.Generic;

namespace APIGK2V.ViewModels
{
    public class ApostasUsuarioTemporadaViewModel
    {
        public string IdUsuario { get; set; }
        public string IdTemporada { get; set; }
        public List<ApostaViewModel> Apostas { get; set; }
    }
}