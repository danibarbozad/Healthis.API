﻿using Healthis.Entities;
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
        [Authorize]
        [HttpGet]
        [Route("api/address")]
        public List<Endereco> Get()
        {
            EnderecoService service = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [Authorize]
        [HttpGet]
        [Route("api/address/{id}")]
        public Endereco Get(int id)
        {
            EnderecoService service = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Get(id);
        }

        [Authorize]
        [HttpPost]
        [Route("api/address/create")]
        public Endereco Create([FromBody] EnderecoRequest endereco)
        {
            EnderecoService service = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.Create(new Endereco().ConvertFromRequest(endereco));
        }

        [Authorize]
        [HttpPost]
        [Route("api/address/update")]
        public IHttpActionResult Update([FromBody] Endereco endereco)
        {
            EnderecoService service = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);

            if (service.Get(endereco.ID) == null)
                return BadRequest("Endereço não localizado!");

            return Ok(service.Update(endereco));
        }

        [Authorize]
        [HttpPost]
        [Route("api/address/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                EnderecoService service = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
                if (service.Delete(id))
                    return Ok("Endereço deletado com sucesso!");
                else
                {
                    return BadRequest("Endereço não localizado!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
