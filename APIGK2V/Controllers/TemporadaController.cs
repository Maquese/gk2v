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
        public Temporada InserirNovaTemporada(TemporadaViewModel temporada)
        {
            var lista = GerarListaAnosPossiveis((Fase)temporada.Fase);

                Temporada temporadaAdd = new Temporada();
            if(temporada.TimesMesmaEpoca)
            {               
                ///randomiza o ano 
                // pega as selecoes

                temporadaAdd.FaseInicial = temporada.Fase;
                temporadaAdd.TimesMesmaEpoca = temporada.TimesMesmaEpoca;
                temporadaAdd.DataInicial = DateTime.Now;
                

                Random r = new Random();
                var ano = lista[r.Next(0,lista.Count)];

                var onde = "{" +String.Format(" 'Year' : {0}" ,"1938") + "}";
                IList<Match> matches = _matchRepositorio.ListarOnde(onde);

                var quantidade = QuantidadeTimes((Fase)temporada.Fase);
                var times = PegaSelecoesPorFaseRandomicamente(matches,quantidade);
                
                switch ((Fase)temporada.Fase)
                {
                    case Fase.Oitavas:
                    {
                        temporadaAdd.Jogos = GeraJogosPorFaseOitavas(times);
                        break;
                    }
                    case Fase.Quartas:
                    {
                        temporadaAdd.Jogos = GeraJogosPorFaseQuarta(times);
                        break;
                    }
                    case Fase.SemiFinais:
                    {
                        temporadaAdd.Jogos = GeraJogosPorFaseSemifinal(times);
                        break;
                    }
                    case Fase.Final:
                    {
                        temporadaAdd.Jogos = GeraJogosPorFaseFinal(times);
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
            }
            _temporadaRepositorio.Insert(temporadaAdd);
            return temporadaAdd;
        }

        public List<TimesmGolsViewModel> PegarVencedores(List<Jogo> jogos, List<TimesmGolsViewModel> times)
        {
            var retorno = new List<TimesmGolsViewModel>();
            foreach (var item in jogos.Select(x => x.Vencedor).ToList())
            {
                retorno.Add(times.Where(x => x.Nome == item).FirstOrDefault());
            }
            return retorno;
        }

        private List<TimesmGolsViewModel> PegaSelecoesPorFaseRandomicamente(IList<Match> matches,int quantidade)
        {
            var lista = matches.Select(x => x.HomeTeamName).Distinct().ToList();
            lista.Union(matches.Select(x => x.AwayTeamName).Distinct()).ToList();

             Random r = new Random();
            var listaRandomica = new List<TimesmGolsViewModel>();
            while(listaRandomica.Count < quantidade)
            {
                 var nome = lista[r.Next(0,lista.Count - 1)];
                 if(!listaRandomica.Select(x => x.Nome).Contains(nome))
                 {
                     listaRandomica.Add(new  TimesmGolsViewModel
                     {
                         Nome = nome,
                         Gols = (matches.Where(x => x.HomeTeamName == nome).Sum(x => x.HomeTeamGoals) + matches.Where(x => x.HomeTeamName == nome).Sum(x => x.AwayTeamGoals))
                     } );
                     lista.Remove(nome);
                 }
            }
            return listaRandomica;

        }

        private List<Jogo> GeraJogosPorFaseOitavas(List<TimesmGolsViewModel> times)
        {
            var jogos = new List<Jogo>();
            for (int i = 0; i < times.Count; i+=2)
            {
                jogos.Add(new Jogo{
                    PrimeiraSelecao = times[i].Nome,
                    SegundaSelecao = times[i+1].Nome,
                    Vencedor = times[i].Gols > times[i+1].Gols ? times[i].Nome : times[i + 1].Nome,
                    Fase = (int)Fase.Oitavas
                });
            }
            times = PegarVencedores(jogos,times);
            jogos.AddRange(GeraJogosPorFaseQuarta(times));   
            return jogos;
        }

        private List<Jogo> GeraJogosPorFaseQuarta(List<TimesmGolsViewModel> timesVencedoresOitavas)
        {
            var jogos = new List<Jogo>();
            for (int i = 0; i < timesVencedoresOitavas.Count; i+=2)
            {
                jogos.Add(new Jogo{
                    PrimeiraSelecao = timesVencedoresOitavas[i].Nome,
                    SegundaSelecao = timesVencedoresOitavas[i+1].Nome,
                    Vencedor = timesVencedoresOitavas[i].Gols > timesVencedoresOitavas[i+1].Gols ? timesVencedoresOitavas[i].Nome : timesVencedoresOitavas[i + 1].Nome,
                    Fase = (int)Fase.Quartas
                });
            }
            timesVencedoresOitavas = PegarVencedores(jogos,timesVencedoresOitavas);
            jogos.AddRange(GeraJogosPorFaseSemifinal(timesVencedoresOitavas));   
            return jogos;
        }

        private List<Jogo> GeraJogosPorFaseSemifinal(List<TimesmGolsViewModel> timesVencedoresQuartas)
        {
            var jogos = new List<Jogo>();
            for (int i = 0; i < timesVencedoresQuartas.Count; i+=2)
            {
                jogos.Add(new Jogo{
                    PrimeiraSelecao = timesVencedoresQuartas[i].Nome,
                    SegundaSelecao = timesVencedoresQuartas[i+1].Nome,
                    Vencedor = timesVencedoresQuartas[i].Gols > timesVencedoresQuartas[i+1].Gols ? timesVencedoresQuartas[i].Nome : timesVencedoresQuartas[i + 1].Nome,
                    Fase = (int)Fase.SemiFinais
                });
            }
            timesVencedoresQuartas = PegarVencedores(jogos,timesVencedoresQuartas);
            jogos.AddRange(GeraJogosPorFaseFinal(timesVencedoresQuartas));   
            return jogos;
        }

        private List<Jogo> GeraJogosPorFaseFinal(List<TimesmGolsViewModel> timesVencedoresSemi)
        {
            var jogos = new List<Jogo>();
            for (int i = 0; i < timesVencedoresSemi.Count; i+=2)
            {
                jogos.Add(new Jogo{
                    PrimeiraSelecao = timesVencedoresSemi[i].Nome,
                    SegundaSelecao = timesVencedoresSemi[i+1].Nome,
                    Vencedor = timesVencedoresSemi[i].Gols > timesVencedoresSemi[i+1].Gols ? timesVencedoresSemi[i].Nome : timesVencedoresSemi[i + 1].Nome,
                    Fase = (int)Fase.Final
                });
            }
            return jogos;
        }

         private List<string> GerarListaAnosPossiveis(Fase fase)
         {
                var lista = new List<string>();
                if(fase != Fase.Oitavas){
                lista.Add("1930");
                lista.Add("1934");
                lista.Add("1938");
                //lista.Add("1942");
                //lista.Add("1946");
                lista.Add("1950");
                lista.Add("1954");
                lista.Add("1958");
                lista.Add("1962");
                lista.Add("1966");
                lista.Add("1970");                
                lista.Add("1978");
                }
                lista.Add("1974");
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