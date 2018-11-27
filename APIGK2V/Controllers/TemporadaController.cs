using APIGK2V.Contratos;
using APIGK2V.Entidades;
using APIGK2V.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace APIGK2V.Controllers
{
    public class TemporadaController : ControllerBase
    {
        private readonly ITemporadaRepositorio _temporadaRepositorio;

        public TemporadaController(ITemporadaRepositorio temporadaRepositorio)
        {
            _temporadaRepositorio = temporadaRepositorio;
        }

        
        [HttpPost]
        [Route("api/Temporada/Inserir")]
        public void InserirNovaTemporada([FromBody]TemporadaViewModel temporada)
        {
            if(temporada.TimesMesmaEpoca)
            {
                ///randomiza o ano 
                // pega as selecoes

            }
        }

        private  void PegaSelecoesMesmoAnoRandomicamente()
        {

        }

        private void PegaSelecoesPorFaseRandomicamente()
        {
            
        }
    }
}