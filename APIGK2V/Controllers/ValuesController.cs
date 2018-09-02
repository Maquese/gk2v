using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGK2V.Contratos;
using APIGK2V.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace APIGK2V.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;

        public ValuesController(IPessoaRepositorio pessoaRepositorio)
        {
            _pessoaRepositorio = pessoaRepositorio;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
           var r = _pessoaRepositorio.Encontrar(new Pessoa{nome = "Kenney"});
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
