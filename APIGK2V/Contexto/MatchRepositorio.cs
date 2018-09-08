using APIGK2V.Contratos;
using APIGK2V.Entidades;

namespace APIGK2V.Contexto
{
    public class MatchRepositorio : RepositorioBase<Match>, IMatchRepositorio
    {
        public MatchRepositorio(IContextoMongo context) : base(context)
        {
        }
    }
}