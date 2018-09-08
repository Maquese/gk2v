using APIGK2V.Contratos;
using Microsoft.AspNetCore.Mvc;

namespace APIGK2V.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepositorio _playerRepositorio;

        public PlayerController(IPlayerRepositorio playerRepositorio)
        {
            _playerRepositorio = playerRepositorio;
        }

        [Route("api/Player/ListarJogadores")]
        [HttpGet]
        public JsonResult ListarJogadores()
        {
             return new JsonResult(_playerRepositorio.Listar());
        }
    }
}