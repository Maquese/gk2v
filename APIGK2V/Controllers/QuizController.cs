using System.Collections.Generic;
using APIGK2V.Contratos;
using APIGK2V.Entidades;
using APIGK2V.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MongoDB.Bson;
using System;

namespace APIGK2V.Controllers
{
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepositorio _quizRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;

        public QuizController(IQuizRepositorio quizRepositorio,IUsuarioRepositorio usuarioRepositorio)
        {
            _quizRepositorio = quizRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost]
        [Route("api/Quiz/QuizDiario")]
        public Quiz QuizDiario()
        {
                    Quiz retorno = null;
            try
            {
                IList<Quiz> lista = _quizRepositorio.Listar();
                Random r = new Random();


                
                var value = r.Next(lista.Count());

                retorno = lista[value]; 
                                                   
            }
            catch (System.Exception)
            {
                
                throw;
            }
            return retorno;
        }

        [HttpPost]
        [Route("api/Quiz/RespostaQuiz")]
        public void RespostaQuiz([FromBody]RespostaQuizViewModel resposta)
        {
            try
            {
                var onde = "{" + string.Format("_id : ObjectId('{0}')",resposta.IdUsuario)+ "}";
                var usuario = _usuarioRepositorio.Encontrar(onde);
                usuario.RespostasQuiz.Add(new RespostaQuiz{Acertou = resposta.Acertou,DataResposta = DateTime.Now, IdQuiz = resposta.IdQuiz});
                _usuarioRepositorio.Update(onde,usuario);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}