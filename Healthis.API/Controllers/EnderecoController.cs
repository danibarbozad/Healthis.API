using Healthis.Entities;
using Healthis.Entities.ApiEntities;
using Healthis.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Healthis.API.Controllers
{
    public class EnderecoController : ApiController
    {
        [HttpGet]
        public List<Endereco> Get()
        {
            EnderecoService service = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [HttpGet]
        public Endereco Get(int id)
        {
            EnderecoService service = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [HttpPost]
        [Route("api/endereco/create")]
        public Endereco Create([FromBody] EnderecoRequest endereco)
        {
            EnderecoService service = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(new Endereco().ConvertFromRequest(endereco));
        }

        [HttpPost]
        [Route("api/endereco/update")]
        public Endereco Update([FromBody] Endereco endereco)
        {
            EnderecoService service = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Update(endereco);
        }

        [HttpPost]
        [Route("api/endereco/delete/{id}")]
        public bool Delete(int id)
        {
            EnderecoService service = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Delete(id);
        }
    }
}
