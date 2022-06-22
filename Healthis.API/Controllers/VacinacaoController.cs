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
    public class VacinacaoController : ApiController
    {
        [HttpGet]
        [Route("api/vaccination")]
        public List<Vacinacao> Get()
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [HttpGet]
        [Route("api/vaccination/{id}")]
        public Vacinacao Get(int id)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [HttpPost]
        [Route("api/vaccination/create")]
        public Vacinacao Create([FromBody] VacinacaoRequest vacinacao)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(new Vacinacao().ConvertFromRequest(vacinacao));
        }

        [HttpPost]
        [Route("api/vaccination/update")]
        public Vacinacao Update([FromBody] Vacinacao vacinacao)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Update(vacinacao);
        }

        [HttpPost]
        [Route("api/vaccination/delete/{id}")]
        public bool Delete(int id)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Delete(id);
        }

        [HttpPost]
        [Route("api/vaccination/vaccinationVacines")]
        public IHttpActionResult VincularVacinaVacinacao([FromBody] VacinaVacinacaoRequest request)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            if (service.AssociarVacinaVacinacao(request.VacinaID, request.VacinacaoID))
                return Ok(String.Format("Vacina associada com sucesso à vacinação ID: " + request.VacinacaoID));
            else
                return BadRequest("Erro ao associar vacina com vacinação!");
        }
    }
}
