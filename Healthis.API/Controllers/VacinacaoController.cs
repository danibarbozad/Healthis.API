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
    public class VacinacaoController : ApiController
    {
        [HttpGet]
        public List<Vacinacao> Get()
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [HttpGet]
        public Vacinacao Get(int id)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [HttpPost]
        [Route("api/vacinacao/create")]
        public Vacinacao Create([FromBody] Vacinacao vacinacao)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(vacinacao);
        }

        [HttpPost]
        [Route("api/vacinacao/update")]
        public Vacinacao Update([FromBody] Vacinacao vacinacao)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Update(vacinacao);
        }

        [HttpPost]
        [Route("api/vacinacao/delete/{id}")]
        public bool Delete(int id)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Delete(id);
        }
    }
}
