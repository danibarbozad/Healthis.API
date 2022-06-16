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
    public class UsuarioController : ApiController
    {
        [HttpGet]
        public List<Usuario> Get()
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [HttpGet]
        public Usuario Get(int id)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [HttpPost]
        [Route("api/usuario/create")]
        public Usuario Create([FromBody] UsuarioRequest usuario)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(new Usuario().ConvertFromRequest(usuario));
        }

        [HttpPost]
        [Route("api/usuario/update")]
        public Usuario Update([FromBody] Usuario usuario)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Update(usuario);
        }

        [HttpPost]
        [Route("api/usuario/delete/{id}")]
        public bool Delete(int id)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Delete(id);
        }
    }
}
