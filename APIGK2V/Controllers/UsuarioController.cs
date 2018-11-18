 using APIGK2V.Contratos;
using APIGK2V.Entidades;
using APIGK2V.Enum;
using APIGK2V.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace APIGK2V.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _UsuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)

        {
            _UsuarioRepositorio = usuarioRepositorio;
        }

        [Route("api/Usuario/Cadastrar")]
        [HttpPost]
        public void Cadastrar([FromBody] UsuarioViewModel usuario)
        {
            try
            {                
                var usuarioAdd = new Usuario();
                usuarioAdd.Nome = usuario.nome;
                usuarioAdd.Senha = usuario.senha;
                usuarioAdd.Email  = usuario.email;      
                usuarioAdd.TipoUsuario = (TipoUsuario)usuario.TipoUsuario;
                _UsuarioRepositorio.Insert(usuarioAdd);
            }
            catch (System.Exception e)
            {                
                throw;
            }
        }

        [Route("api/Usuario/Login")]
        [HttpPost]
        
        public JsonResult Login([FromBody] UsuarioViewModel usuario)
        {
            try
            {
                var encontrarPor = "{"+string.Format("'Email':'{0}','Senha':'{1}'",usuario.email,usuario.senha)+"}";
                return new JsonResult(_UsuarioRepositorio.Encontrar(encontrarPor));
            }
            catch (System.Exception e)
            {                
                throw;
            }
        }
    }
}