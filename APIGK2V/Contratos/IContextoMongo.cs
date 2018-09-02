using APIGK2V.Contexto;
using MongoDB.Driver;

namespace APIGK2V.Contratos
{
    public interface IContextoMongo
    {
        IMongoDatabase contextoAtual();

          
    }
}