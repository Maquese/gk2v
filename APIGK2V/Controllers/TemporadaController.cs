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
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public TemporadaController(ITemporadaRepositorio temporadaRepositorio, IMatchRepositorio matchRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _temporadaRepositorio = temporadaRepositorio;
            _matchRepositorio = matchRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }
        
        [HttpPost]
        [Route("api/Temporada/Inserir")]
        public Temporada InserirNovaTemporada(TemporadaViewModel temporada)
        {
            var lista = GerarListaAnosPossiveis((Fase)temporada.Fase);

                Temporada temporadaAdd = new Temporada();                
                temporadaAdd.FaseInicial = temporada.Fase;
                temporadaAdd.TimesMesmaEpoca = temporada.TimesMesmaEpoca;
                temporadaAdd.DataInicial = DateTime.Now;
                temporadaAdd.Nome = temporada.Nome;
            if(temporada.TimesMesmaEpoca)
            {               
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

        [HttpPost]
        [Route("api/Temporada/ListarJogosPorTemporadaFase")]
        public List<Jogo> ListarJogosPorTemporadaFase([FromBody]TemporadaViewModel temporada)
        {
            var jogos = new List<Jogo>();
            var onde = "{"+String.Format("_id : ObjectId('{0}')",temporada.Id)+"}";
            var temporadaBd = _temporadaRepositorio.Encontrar(onde);
            return temporadaBd.Jogos.Where(x => x.Fase == temporada.Fase).ToList();
        }

        [HttpPost]
        [Route("api/Temporada/ListarTemporadas")]
        public IList<Temporada> ListarTemporadas([FromBody] ApostasUsuarioTemporadaViewModel apostasUsuarioTemporadaViewModel)
        {
            var temporadas = _temporadaRepositorio.Listar();
            var retorno = new List<Temporada>();
            var ondeAposta = "{"+String.Format("_id : ObjectId('{0}')",apostasUsuarioTemporadaViewModel.IdUsuario) + "}" ;
            var usuario = _usuarioRepositorio.Encontrar(ondeAposta);

            foreach (var item in temporadas)
            {                
                var apostasTemporada = usuario.Apostas.Where(x => x.CodigoTemporada == item._id.ToString()).ToList();
                if(apostasTemporada.Where(x => x.Jogo.Fase == (int)Fase.Final).Count() == 0)
                {
                    retorno.Add(item);
                }
            }
            return retorno;
        }

        [HttpPost]
        [Route("api/Temporada/Apostar")]
        public void Apostar([FromBody]ApostasUsuarioTemporadaViewModel apostaViewModel)
        {
            var ondeAposta = "{"+String.Format("_id : ObjectId('{0}')",apostaViewModel.IdUsuario) + "}" ;
            var usuario = _usuarioRepositorio.Encontrar(ondeAposta);

            var onde = "{"+String.Format("_id : ObjectId('{0}')",apostaViewModel.IdTemporada)+"}";
            var temporadaBd = _temporadaRepositorio.Encontrar(onde);

            foreach (var item in apostaViewModel.Apostas)
            {
                var aposta = new Aposta
                {
                    CodigoTemporada = apostaViewModel.IdTemporada,
                    Pontos = item.Pontos,
                    //Jogo = temporadaBd.Jogos.Where(x => x.)  
                };
            }
            
        }
    }
}