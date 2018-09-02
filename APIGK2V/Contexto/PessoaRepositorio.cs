using APIGK2V.Contratos;
using APIGK2V.Entidades;

namespace APIGK2V.Contexto
{
    public class PessoaRepositorio : RepositorioBase<Pessoa> , IPessoaRepositorio
    {
        public PessoaRepositorio(IContextoMongo context) : base(context)
        {
            
        }
        
    }
}