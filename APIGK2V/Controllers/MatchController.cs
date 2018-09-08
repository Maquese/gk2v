
using APIGK2V.Contratos;
using Microsoft.AspNetCore.Mvc;

namespace APIGK2V.Controllers
{
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchRepositorio _matchRepositorio;

        public MatchController(IMatchRepositorio matchRepositorio)
        {
            _matchRepositorio = matchRepositorio;
        }

        [Route("api/Match/ListarPartidas")]
        [HttpGet]
        public JsonResult ListarPartidas()
        {
            return new JsonResult(_matchRepositorio.Listar());
        }
    }
}