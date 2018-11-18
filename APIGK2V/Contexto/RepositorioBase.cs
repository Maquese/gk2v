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

        public T Encontrar(string onde)
        {
            //var r  = onde.ToBson();
            var result1 = _contexto.contextoAtual().GetCollection<T>(typeof(T).Name).Find(onde).FirstOrDefault();
            //var result =  _contexto.contextoAtual().GetCollection<T>(typeof(T).Name).Find("{'Email':'admin@gk2v.com.br','Senha' : 'Admin'}").FirstOrDefault();
            return result1;
            //return 
        }

        public void Insert(T document)
        {
           _contexto.contextoAtual().GetCollection<T>(typeof(T).Name).InsertOne(document);
        }

        public void InsertMany(IList<T> documents)
        {
            _contexto.contextoAtual().GetCollection<T>(typeof(T).Name).InsertMany(documents);
        }

        public virtual IList<T> Listar()
        {
           return _contexto.contextoAtual().GetCollection<T>(typeof(T).Name).Find("{}").ToList();
        }

        public void Update(string onde,T document)
        {            
            var doc = document.ToJson();
            _contexto.contextoAtual().GetCollection<T>(typeof(T).Name).ReplaceOne(onde,document);
        }
    }
}