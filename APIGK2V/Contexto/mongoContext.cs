using APIGK2V.Contratos;
using APIGK2V.Entidades;
using MongoDB.Driver;

namespace APIGK2V.Contexto
{
    public class mongoContext  : IContextoMongo
    {
        private readonly IMongoDatabase mongo;

        public mongoContext()
        {
            //jvarb.gcp.mongodb.net/test cluster vinicius
           mongo  = new MongoClient("mongodb://admin:admin@cluster0-shard-00-00-xhygg.mongodb.net:27017,cluster0-shard-00-01-xhygg.mongodb.net:27017,cluster0-shard-00-02-xhygg.mongodb.net:27017?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true").GetDatabase("GK2V");

            //mongo = new MongoClient().GetDatabase("test");
           //var x = mongo.GetCollection<Cup>("Cup").Find("{}").ToList();
        }

        public IMongoDatabase contextoAtual()
        {
            return mongo;
        }
    }
}