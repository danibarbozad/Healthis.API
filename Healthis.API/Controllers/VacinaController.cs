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
        [Authorize]
        [HttpGet]
        [Route("api/vaccine")]
        public List<Vacina> Get()
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [Authorize]
        [HttpGet]
        [Route("api/vaccine/{id}")]
        public Vacina Get(int id)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [Authorize]
        [HttpPost]
        [Route("api/vaccine/create")]
        public Vacina Create([FromBody] VacinaRequest vacina)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(new Vacina().ConvertFromRequest(vacina));
        }

        [Authorize]
        [HttpPost]
        [Route("api/vaccine/update")]
        public IHttpActionResult Update([FromBody] Vacina vacina)
        {
            VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);

            if (service.Get(vacina.ID) == null)
                return BadRequest("Vacina não localizada!");

            return Ok(service.Update(vacina));
        }

        [Authorize]
        [HttpPost]
        [Route("api/vaccine/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                VacinaService service = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
                if (service.Delete(id))
                    return Ok("Vacina deletada com sucesso!");
                else
                {
                    return BadRequest("Vacina não localizada!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
