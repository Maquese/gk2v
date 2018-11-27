using System;
using System.Collections.Generic;
using APIGK2V.Contratos;
using APIGK2V.Entidades;
using APIGK2V.Enum;
using APIGK2V.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace APIGK2V.Controllers
{
    public class TemporadaController : ControllerBase
    {
        private readonly ITemporadaRepositorio _temporadaRepositorio;
        private readonly IMatchRepositorio _matchRepositorio;

        public TemporadaController(ITemporadaRepositorio temporadaRepositorio, IMatchRepositorio matchRepositorio)
        {
            _temporadaRepositorio = temporadaRepositorio;
            _matchRepositorio = matchRepositorio;
        }

        
        [HttpPost]
        [Route("api/Temporada/Inserir")]
        public void InserirNovaTemporada([FromBody]TemporadaViewModel temporada)
        {
            var lista = GerarListaAnosPossiveis();

            if(temporada.TimesMesmaEpoca)
            {               
                ///randomiza o ano 
                // pega as selecoes
                Random r = new Random();
                var ano = lista[r.Next(0,19)];

                var onde = String.Format("{'Year' : {0}}",ano);
                IList<Match> matches = _matchRepositorio.ListarOnde(onde);

                var quantidade = QuantidadeTimes((Fase)temporada.Fase);
                var times = PegaSelecoesPorFaseRandomicamente(matches,quantidade);
            }
        }

        private  void PegaSelecoesMesmoAnoRandomicamente()
        {

        }

        private List<string> PegaSelecoesPorFaseRandomicamente(IList<Match> matches,int quantidade)
        {
            var lista = matches.Select(x => x.HomeTeamName).Distinct().ToList();
            lista.Union(matches.Select(x => x.AwayTeamName).Distinct()).ToList();

             Random r = new Random();
            var listaRandomica = new List<string>();
            while(listaRandomica.Count < quantidade)
            {
                 var nome = lista[r.Next(0,lista.Count - 1)];
                 if(!listaRandomica.Contains(nome))
                 {
                     listaRandomica.Add(nome);
                 }
            }
            return listaRandomica;

        }

        private void GeraJogosPorFase()
        {

        }

         private List<string> GerarListaAnosPossiveis()
         {
                var lista = new List<string>();
                lista.Add("1930");
                lista.Add("1934");
                lista.Add("1938");
                lista.Add("1942");
                lista.Add("1946");
                lista.Add("1950");
                lista.Add("1954");
                lista.Add("1958");
                lista.Add("1962");
                lista.Add("1966");
                lista.Add("1970");
                lista.Add("1974");
                lista.Add("1978");
                lista.Add("1982");
                lista.Add("1986");
                lista.Add("1990");
                lista.Add("1994");                
                lista.Add("1998");
                lista.Add("2002");                
                lista.Add("2006");
                lista.Add("2010");
                lista.Add("2014");

                return lista;
         }

         private int QuantidadeTimes(Fase fase)
         {
             var quantidade = 0;
             switch (fase)
             {
                 case Fase.Final : 
                 {
                     quantidade = 2;
                     break;
                 }
                 case Fase.SemiFinais : 
                 {
                     quantidade = 4;
                     break;
                 }
                 case Fase.Quartas : 
                 {
                     quantidade = 8;
                     break;
                 }
                 ///tem que ver essa questão não existe oitavas de final
                 case Fase.Oitavas : 
                 {
                     quantidade = 16;
                     break;
                 }
                 default :
                 {
                     break;
                 }
             }
             return quantidade;
         }
    }
}