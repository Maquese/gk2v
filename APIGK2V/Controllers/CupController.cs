using APIGK2V.Contratos;
using Microsoft.AspNetCore.Mvc;

namespace APIGK2V.Controllers
{

    [ApiController]
    public class CupController : ControllerBase
    {
        private readonly ICupRepositorio _cupRepositorio;

        public CupController(ICupRepositorio cupRepositorio)
        {
            _cupRepositorio = cupRepositorio;
        }

        [Route("api/Cup/ListarCampeonatos")]
        [HttpGet]
        public JsonResult ListarCampeonatos()
        {
            return new JsonResult(_cupRepositorio.Listar());
        }
    }
}