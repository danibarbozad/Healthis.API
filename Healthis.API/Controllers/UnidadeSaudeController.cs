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

namespace Healthis.API.Controllers
{
    public class UnidadeSaudeController : ApiController
    {
        [HttpGet]
        [Route("api/healthUnity")]
        public List<UnidadeSaude> Get()
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [HttpGet]
        [Route("api/healthUnity/{id}")]
        public UnidadeSaude Get(int id)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [HttpPost]
        [Route("api/healthUnity/create")]
        public UnidadeSaude Create([FromBody] UnidadeSaudeRequest unidadeSaude)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(new UnidadeSaude().ConvertFromRequest(unidadeSaude));
        }

        [HttpPost]
        [Route("api/healthUnity/update")]
        public UnidadeSaude Update([FromBody] UnidadeSaude unidadeSaude)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Update(unidadeSaude);
        }

        [HttpPost]
        [Route("api/healthUnity/delete/{id}")]
        public bool Delete(int id)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Delete(id);
        }
    }
}
