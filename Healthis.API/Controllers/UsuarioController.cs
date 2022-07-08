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
        [Authorize]
        [HttpGet]
        [Route("api/user")]
        public List<Usuario> Get()
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [Authorize]
        [HttpGet]
        [Route("api/user/{id}")]
        public Usuario Get(int id)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        //[HttpPost]
        //[Route("api/user/create")]
        //public Usuario Create([FromBody] UsuarioRequest usuario)
        //{
        //    UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
        //    return service.Create(new Usuario().ConvertFromRequest(usuario));
        //}

        [Authorize]
        [HttpPost]
        [Route("api/user/update/{id}")]
        public IHttpActionResult Update(int id, [FromBody] UsuarioRequest usuario)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            EnderecoService enderecoService = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);

            if (enderecoService.Get(usuario.EnderecoID) == null)
                return BadRequest("Endereço não existe!");

            var getUsuario = service.Get(id);
            if (getUsuario != null)
            {
                return Ok(service.Update(new Usuario
                {
                    ID = getUsuario.ID,
                    Nome = usuario.Nome,
                    CPF = usuario.CPF,
                    Sexo = usuario.Sexo,
                    DataNascimento = usuario.DataNascimento,
                    Telefone = usuario.Telefone,
                    EnderecoID = usuario.EnderecoID
                }));
            }

            return Ok("Usuário não localizado!");
        }

        [Authorize]
        [HttpPost]
        [Route("api/user/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
                if (service.Delete(id))
                    return Ok("Usuario deletado com sucesso!");
                else
                {
                    return BadRequest("Usuario não localizado!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/user/userVaccination")]
        public IHttpActionResult VincularVacinacaoUsuario([FromBody] VacinacaoUsuarioRequest request)
        {
            UsuarioService service = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            VacinacaoService vacinacaoService = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);

            if (service.Get(request.UsuarioID) == null)
                return BadRequest("Usuário não localizado!");

            if (vacinacaoService.Get(request.VacinacaoID) == null)
                return BadRequest("Vacinação não localizada!");

            if (service.VincularVacinacaoUsuario(request.UsuarioID, request.VacinacaoID))
                return Ok(String.Format("Vacinação vinculada com sucesso ao usuário ID: " + request.UsuarioID));
            else
                return BadRequest("Erro ao vincular vacinação com usuário!");
        }
    }
}
