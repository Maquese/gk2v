using APIGK2V.Contratos;
using MongoDB.Bson;
using APIGK2V.Entidades;
using System.Collections.Generic;
using MongoDB.Driver;

namespace APIGK2V.Contexto
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        private readonly IContextoMongo _contexto;

        public RepositorioBase(IContextoMongo context)
        {
            _contexto = context;
        }

        public T Encontrar(T onde)
        {
            var where = onde.ToJson();
            return _contexto.contextoAtual().GetCollection<T>(typeof(T).Name).Find(where).FirstOrDefault();
        }

        public void Insert(T document)
        {
           _contexto.contextoAtual().GetCollection<T>(typeof(T).Name).InsertOne(document);
        }

        public virtual IList<T> Listar()
        {
            var x  = _contexto.contextoAtual().GetCollection<T>(typeof(T).Name).Find("{}");
         
           return x.ToList();
        }

        
    }
}