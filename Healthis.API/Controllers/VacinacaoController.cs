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
        [Authorize]
        [HttpGet]
        [Route("api/vaccination")]
        public List<Vacinacao> Get()
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            return service.GetAll();
        }

        [Authorize]
        [HttpGet]
        [Route("api/vaccination/{id}")]
        public IHttpActionResult Get(int id)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            Vacinacao vacinacao = service.Get(id);
            if (vacinacao == null)
                return BadRequest("Vacinação não encontrada");
            else
            {
                return Ok(vacinacao);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/vaccination/create")]
        public IHttpActionResult Create([FromBody] VacinacaoRequest vacinacao)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            EnderecoService enderecoService = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            UnidadeSaudeService unidadeSaudeService = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);

            if (enderecoService.Get(vacinacao.EnderecoID) == null)
                return BadRequest("Endereço não localizado!");

            if (unidadeSaudeService.Get(vacinacao.UnidadeSaudeID) == null)
                return BadRequest("Unidade de saúde não localizada!");

            return Ok(service.Create(new Vacinacao().ConvertFromRequest(vacinacao)));
        }

        [Authorize]
        [HttpPost]
        [Route("api/vaccination/update/{id}")]
        public IHttpActionResult Update(int id, [FromBody] VacinacaoRequest vacinacao)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            UnidadeSaudeService unidadeSaudeService = new UnidadeSaudeService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            EnderecoService enderecoService = new EnderecoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);

            if (enderecoService.Get(vacinacao.EnderecoID) == null)
                return BadRequest("Endereço não localizado!");

            if (unidadeSaudeService.Get(vacinacao.UnidadeSaudeID) == null)
                return BadRequest("Unidade de saúde não localizada!");

            var getVacinacao = service.Get(id);
            if (getVacinacao != null)
            {
                return Ok(service.Update(new Vacinacao
                {
                    ID = getVacinacao.ID,
                    DataVacinacao = vacinacao.DataVacinacao,
                    DataProximaDose = vacinacao.DataProximaDose,
                    Reacao = vacinacao.Reacao,
                    DescricaoReacao = vacinacao.DescricaoReacao,
                    UnidadeSaudeID = vacinacao.UnidadeSaudeID,
                    EnderecoID = vacinacao.EnderecoID
                }));
            }

            return Ok("Vacinação não localizada!");
        }

        [Authorize]
        [HttpPost]
        [Route("api/vaccination/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
                if (service.Delete(id))
                    return Ok("Vacinação deletada com sucesso!");
                else
                {
                    return BadRequest("Vacinação não localizada!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/vaccination/vaccinationVacines")]
        public IHttpActionResult VincularVacinaVacinacao([FromBody] VacinaVacinacaoRequest request)
        {
            VacinacaoService service = new VacinacaoService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            VacinaService vacinaService = new VacinaService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);

            if (service.Get(request.VacinacaoID) == null)
                return BadRequest("Vacinação não localizada!");

            if (vacinaService.Get(request.VacinaID) == null)
                return BadRequest("Vacina não localizada!");

            if (service.AssociarVacinaVacinacao(request.VacinaID, request.VacinacaoID))
                return Ok(String.Format("Vacina associada com sucesso à vacinação ID: " + request.VacinacaoID));
            else
                return BadRequest("Erro ao associar vacina com vacinação!");
        }
    }
}
