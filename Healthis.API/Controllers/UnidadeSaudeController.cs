using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Healthis.Entities;
using Healthis.Service;
using System.Configuration;
using Healthis.Entities.ApiEntities;
using Newtonsoft.Json;

namespace Healthis.API.Controllers
{
    public class UnidadeSaudeController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/healthUnity")]
        public List<UnidadeSaude> Get()
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [Authorize]
        [HttpGet]
        [Route("api/healthUnity/{id}")]
        public UnidadeSaude Get(int id)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [Authorize]
        [HttpPost]
        [Route("api/healthUnity/create")]
        public UnidadeSaude Create([FromBody] UnidadeSaudeRequest unidadeSaude)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(new UnidadeSaude().ConvertFromRequest(unidadeSaude));
        }

        [Authorize]
        [HttpPost]
        [Route("api/healthUnity/update/{id}")]
        public IHttpActionResult Update(int id, [FromBody] UnidadeSaudeRequest unidadeSaude)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);

            var getUnidadeSaude = service.Get(id);
            if (getUnidadeSaude != null)
            {
                getUnidadeSaude.NomeUnidade = unidadeSaude.NomeUnidade;
                getUnidadeSaude.EnderecoID = unidadeSaude.EnderecoID;
                return Ok(service.Update(getUnidadeSaude));
            }

            return Ok("Unidade de saúde não localizada!");
        }

        [Authorize]
        [HttpPost]
        [Route("api/healthUnity/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Delete(id) ? Ok("Unidade de saúde excluída com sucesso!") : Ok("Unidade de saúde não localizada!");
        }
    }
}
