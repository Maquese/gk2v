using System;
using APIGK2V.Contratos;
using APIGK2V.Entidades;
using APIGK2V.Enum;
using APIGK2V.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace APIGK2V.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly ITemporadaRepositorio _temporadaRepositorio;
        private readonly IUsuarioRepositorio _UsuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, ITemporadaRepositorio temporadaRepositorio)
        {
            _temporadaRepositorio = temporadaRepositorio;
            _UsuarioRepositorio = usuarioRepositorio;
        }

        [Route("api/Usuario/Cadastrar")]
        [HttpPost]
        public Usuario Cadastrar([FromBody] UsuarioViewModel usuario)
        {
            
                var usuarioAdd = new Usuario();
            try
            {                
                if(string.IsNullOrEmpty(usuario._id))
                {
                usuarioAdd.Nome = usuario.nome;
                usuarioAdd.Senha = usuario.senha;
                usuarioAdd.Email  = usuario.email;      
                usuarioAdd.TipoUsuario = (TipoUsuario)usuario.TipoUsuario;
                
                _UsuarioRepositorio.Insert(usuarioAdd);
                }else{
                    
                    var onde = "{"+String.Format("_id : ObjectId('{0}')",usuario._id)+"}";
                    usuarioAdd = _UsuarioRepositorio.Encontrar(onde);
                    usuarioAdd.Nome = usuario.nome;
                    usuarioAdd.Senha = usuario.senha;
                    usuarioAdd.Email  = usuario.email;    
                    _UsuarioRepositorio.Update(onde,usuarioAdd);
                }
            }
            catch (System.Exception e)
            {                
                throw;
            }
            return usuarioAdd;
        }

        [Route("api/Usuario/Login")]
        [HttpPost]      
        public JsonResult Login([FromBody] UsuarioViewModel usuario)
        {
            try
            {
                var encontrarPor = "{"+string.Format("'Email':'{0}','Senha':'{1}'",usuario.email,usuario.senha)+"}";
                return new JsonResult(_UsuarioRepositorio.Encontrar(encontrarPor));
            }
            catch (System.Exception e)
            {                
                throw;
            }
        }

        /*[HttpPost]
        public IList<Aposta> ListarApostas([FromBody] ApostasUsuarioTemporadaViewModel apostasUsuarioTemporadaViewModel)
        {
            
            var onde = "{"+String.Format("_id : ObjectId('{0}')",apostasUsuarioTemporadaViewModel.IdTemporada)+"}";
            var temporadaBd = _temporadaRepositorio.Encontrar(onde);

            var ondeAposta = "{"+String.Format("_id : ObjectId('{0}')",apostasUsuarioTemporadaViewModel.IdUsuario) + "}" ;
            var usuario = _UsuarioRepositorio.Encontrar(ondeAposta);

            var apostasTemporada = usuario.Apostas.Where(x => x.CodigoTemporada == temporadaBd._id.ToString()).ToList();

            if(apostasTemporada.Where(x => x.Jogo.Fase == (int)Fase.Final).Count() > 0)
            {

            }else if(apostasTemporada.Where(x => x.Jogo.Fase == (int)Fase.SemiFinais).Count() > 0)
            {

            }else if(apostasTemporada.Where(x => x.Jogo.Fase == (int)Fase.Quartas).Count() > 0)
            {

            }else if(apostasTemporada.Where(x => x.Jogo.Fase == (int)Fase.Oitavas).Count() > 0)
            {

            }
        }*/

        [HttpPost]
        [Route("api/Usuario/RankingUsuarioPorPontos")]
        public IList<RetornoRankingViewModel> RankingUsuarioPorPontos()
        {
            var retorno =  _UsuarioRepositorio.Listar().Where(x => x.TipoUsuario != TipoUsuario.Admin).Select(x => new RetornoRankingViewModel { Nome = x.Nome, Pontuacao = x.Pontuacao }).OrderByDescending(x => x.Pontuacao).ToList();

            return retorno;
        }

    }
}