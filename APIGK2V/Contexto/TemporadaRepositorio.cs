using APIGK2V.Contratos;
using APIGK2V.Entidades;

namespace APIGK2V.Contexto
{
    public class TemporadaRepositorio : RepositorioBase<Temporada>, ITemporadaRepositorio
    {
        public TemporadaRepositorio(IContextoMongo context) : base(context)
        {
        }
    }
}