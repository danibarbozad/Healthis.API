using Healthis.API.Authentication;
using Healthis.Entities;
using Healthis.Entities.ApiEntities;
using Healthis.Service;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Healthis.API.Controllers
{
    public class AuthController : ApiController
    {
        private AuthRepository _repo = null;

        public AuthController()
        {
            _repo = new AuthRepository();
        }

        // POST api/auth/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            UsuarioService usuarioService = new UsuarioService(ConfigurationManager.ConnectionStrings["HealthisDB"].ConnectionString);
            Usuario usuario = usuarioService.Create(new Usuario()
            {
                UserName = userModel.UserName,
                Email = userModel.Email
            });

            return Ok(new 
            {
                Message = "Usuário criado com sucesso",
                UsuarioID = usuario.ID,
                usuario.Email,
                usuario.UserName
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
