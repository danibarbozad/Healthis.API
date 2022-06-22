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
using System.Web.Http.Results;

namespace Healthis.API.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("api/user")]
        public List<Usuario> Get()
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [HttpGet]
        [Route("api/user/{id}")]
        public Usuario Get(int id)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [HttpPost]
        [Route("api/user/create")]
        public Usuario Create([FromBody] UsuarioRequest usuario)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(new Usuario().ConvertFromRequest(usuario));
        }

        [HttpPost]
        [Route("api/user/update")]
        public Usuario Update([FromBody] Usuario usuario)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Update(usuario);
        }

        [HttpPost]
        [Route("api/user/delete/{id}")]
        public bool Delete(int id)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Delete(id);
        }

        [HttpPost]
        [Route("api/user/userVaccination")]
        public IHttpActionResult VincularVacinacaoUsuario([FromBody] VacinacaoUsuarioRequest request)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            if (service.VincularVacinacaoUsuario(request.UsuarioID, request.VacinacaoID))
                return Ok(String.Format("Vacinação vinculada com sucesso ao usuário ID: " + request.UsuarioID));
            else
                return BadRequest("Erro ao vincular vacinação com usuário!");
        }
    }
}
