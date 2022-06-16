using Healthis.Entities;
using Healthis.Service;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Healthis.API.Controllers
{
    public class VacinaController : ApiController
    {
        [HttpGet]
        public List<Vacina> Get()
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [HttpGet]
        public Vacina Get(int id)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [HttpPost]
        [Route("api/vacina/create")]
        public Vacina Create([FromBody] Vacina vacina)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(vacina);
        }

        [HttpPost]
        [Route("api/vacina/update")]
        public Vacina Update([FromBody] Vacina vacina)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Update(vacina);
        }

        [HttpPost]
        [Route("api/vacina/delete/{id}")]
        public bool Delete(int id)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Delete(id);
        }
    }
}
