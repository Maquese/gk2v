using APIGK2V.Contratos;
using APIGK2V.Entidades;

namespace APIGK2V.Contexto
{
    public class PlayerRepositorio : RepositorioBase<Player>, IPlayerRepositorio
    {
        public PlayerRepositorio(IContextoMongo context) : base(context)
        {
        }
    }
}