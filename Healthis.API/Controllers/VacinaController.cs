using Healthis.Entities;
using Healthis.Service;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Healthis.Entities.ApiEntities;

namespace Healthis.API.Controllers
{
    public class VacinaController : ApiController
    {
        [HttpGet]
        [Route("api/vaccine")]
        public List<Vacina> Get()
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [HttpGet]
        [Route("api/vaccine/{id}")]
        public Vacina Get(int id)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [HttpPost]
        [Route("api/vaccine/create")]
        public Vacina Create([FromBody] VacinaRequest vacina)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(new Vacina().ConvertFromRequest(vacina));
        }

        [HttpPost]
        [Route("api/vaccine/update")]
        public Vacina Update([FromBody] Vacina vacina)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Update(vacina);
        }

        [HttpPost]
        [Route("api/vaccine/delete/{id}")]
        public bool Delete(int id)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Delete(id);
        }
    }
}
