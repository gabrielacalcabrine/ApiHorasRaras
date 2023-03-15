using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Loucademy.HorasRaro.Domain.Contracts.AuthContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Loucademy.HorasRaro.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        

        public AuthController(IAuthService service)
        {
            _authService = service;
        }

        /// <summary>
        /// Loga Usuário e retorna Jwt.
        /// </summary>
        /// <returns>Jwt</returns>
        /// <response code="200">Retorna Jwt</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Loga usuario e retorna jwt")]
        public async Task<ActionResult<LoginResponse>> LogaUsuario([FromBody] LoginResquest loginRequest)
        {
            var jwt = await _authService.AutenticarAsync(loginRequest.Email, loginRequest.Senha);         
            return Ok(jwt);
        }

        /// <summary>
        /// Confirma email do usuário cadastrado.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [HttpGet("confirma-email")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Confirma email do usuario cadastrado")]
        public async Task<ActionResult<string>> LogaUsuario([FromQuery] string token)
        {
            var Token = await _authService.ConfirmaEmailAsync(token);            
            return Ok();
        }

        /// <summary>
        /// Realiza envio Código de confirmação do email.
        /// </summary>
        /// <returns>Código</returns>
        /// <response code="200">Retorna código de confirmação</response>
        [HttpPost("esqueci-senha")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Envia código para email para alterar senha")]
        public async Task<ActionResult<string>> EsqueciSenha([FromBody] EsqueciSenhaRequest email)
        {
            var envio = await _authService.EnviaCodigoNovaSenhaAsync(email);            
            return Ok(envio);
        }
    }
}
