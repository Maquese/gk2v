using APIGK2V.Contratos;
using MongoDB.Driver;

namespace APIGK2V.Contexto
{
    public class mongoContext  : IContextoMongo
    {
        private readonly IMongoDatabase mongo;

        public mongoContext()
        {
            mongo = new MongoClient().GetDatabase("test");
            var x = mongo.ListCollections();
        }

        public IMongoDatabase contextoAtual()
        {
            return mongo;
        }
    }
}