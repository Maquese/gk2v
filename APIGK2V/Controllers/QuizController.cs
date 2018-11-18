using System.Collections.Generic;
using APIGK2V.Contratos;
using APIGK2V.Entidades;
using APIGK2V.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APIGK2V.Controllers
{
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepositorio _quizRepositorio;

        public QuizController(IQuizRepositorio quizRepositorio)
        {
            _quizRepositorio = quizRepositorio;
        }

        [HttpPost]
        [Route("api/Quiz/QuizDiario")]
        public Quiz QuizDiario()
        {
                    Quiz retorno = null;
            try
            {
                IList<Quiz> lista = _quizRepositorio.Listar();

                retorno = lista.FirstOrDefault();
                                                   
            }
            catch (System.Exception)
            {
                
                throw;
            }
            return retorno;
        }
    }
}