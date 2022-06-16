using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Healthis.Entities;
using Healthis.Service;
using System.Configuration;

namespace Healthis.API.Controllers
{
    public class UnidadeSaudeController : ApiController
    {
        [HttpGet]
        public List<UnidadeSaude> Get()
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [HttpGet]
        public UnidadeSaude Get(int id)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [HttpPost]
        [Route("api/unidadeSaude/create")]
        public UnidadeSaude Create([FromBody] UnidadeSaude unidadeSaude)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(unidadeSaude);
        }

        [HttpPost]
        [Route("api/unidadeSaude/update")]
        public UnidadeSaude Update([FromBody] UnidadeSaude unidadeSaude)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Update(unidadeSaude);
        }

        [HttpPost]
        [Route("api/unidadeSaude/delete/{id}")]
        public bool Delete(int id)
        {
            UnidadeSaudeService service = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Delete(id);
        }
    }
}
