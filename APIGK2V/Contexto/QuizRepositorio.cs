using APIGK2V.Contratos;
using APIGK2V.Entidades;

namespace APIGK2V.Contexto
{
    public class QuizRepositorio : RepositorioBase<Quiz>, IQuizRepositorio
    {
        public QuizRepositorio(IContextoMongo context) : base(context)
        {
        }
    }
}