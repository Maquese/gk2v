using APIGK2V.Contratos;
using APIGK2V.Entidades;

namespace APIGK2V.Contexto
{
    public class CupRepositorio : RepositorioBase<Cup>, ICupRepositorio
    {
        public CupRepositorio(IContextoMongo context) : base(context)
        {
            //teste commit git
        }
    }
}