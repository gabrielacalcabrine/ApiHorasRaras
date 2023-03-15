using AutoMapper;
using Loucademy.HorasRaro.CrossCutting.Mappers;
using Loucademy.HorasRaro.Domain;
using Loucademy.HorasRaro.Domain.Contracts;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace Loucademy.HorasRaro.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;
       

        public UsuarioController(IUsuarioService service)
        {
            _usuarioService = service;           
        }

        /// <summary>
        /// Realiza cadastro de novo usuário.
        /// </summary>
        /// <returns>Usuário cadastrado</returns>
        /// <response code="201">Retorna usuário cadastrado</response>
        /// <response code="400">Se o item não for criado</response> 
        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra um novo usuario no banco.", Description = "Retorna dados do usuario.")]
        [ProducesResponseType(201)]
        public async Task<ActionResult<UsuarioResponse>> Post([FromBody] UsuarioRequest usuario)
        {
            var result = await _usuarioService.Post(usuario);            
            return Ok(result);
        }

        /// <summary>
        /// Realiza busca de usuário por Id.
        /// </summary>
        /// <returns>Usuário</returns>
        /// <response code="200">Retorna usuário</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca um usuário. Por id", Description = "Retorna o usuario se ele for encontrado, e se não, retorna exception.")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UsuarioResponse>> GetById(int id)
        {
            var result = await _usuarioService.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Realiza busca de todos os usuários.
        /// </summary>
        /// <returns>Todos os usuários cadastrados e ativos</returns>
        /// <response code="200">Retorna todos os usuários</response>
        /// <response code="403">Se o acesso for negado</response> 
        
        [HttpGet]
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [SwaggerOperation(Summary = "Busca Todos os Usuarios ativos.", Description = "Retorna todos os usuarios Ativos.")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> Get()
        {
            var result = await _usuarioService.Get();
            return Ok(result);
        }

        /// <summary>
        /// Busca usuário e realiza mudança de dados.
        /// </summary>   
        ///<returns>Usuário modificado</returns>
        /// <response code="200">Se o objeto existe e foi alterado</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Busca usuário para mudança de dados.", Description = "Retorna o usuario modificado.")]
        public async Task<ActionResult<UsuarioResponse>> Put([FromBody] UsuarioAlteracaoRequest usuarioAlteracao, [FromRoute] int id)
        {
            var result = await _usuarioService.PutUsuario(usuarioAlteracao, id);            
            return Ok(result);
        }

        /// <summary>
        /// Busca usuário e realiza mudança de role
        /// </summary>   
        ///<returns>Usuário modificado</returns>
        /// <response code="200">Se o objeto existe e foi alterado</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Busca usuário para mudança da Role.", Description = "Acesso só ao administrador.Retorna o usuario modificado.")]
        public async Task<ActionResult<UsuarioResponse>> Patch([FromRoute] int id, [FromBody] UsuarioRoleRequest userRole)
        {
            var result = await _usuarioService.PatchRole(id, userRole);            
            return Ok(result);
        }

        /// <summary>
        /// Busca usuário e realiza mudança de Senha.
        /// </summary>   
        ///<returns>Usuário modificado</returns>
        /// <response code="200">Se o objeto existe e foi alterado</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [HttpPatch("alterar-senha")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Busca usuário para mudança da Senha.", Description = "Acesso só ao administrador.Retorna o usuario modificado.")]
        public async Task<ActionResult<UsuarioResponse>> PatchSenha([FromBody] UsuarioSenhaRequest request)
        {
            var result = await _usuarioService.Patch(request);            
            return Ok(result);
        }

        /// <summary>
        /// Deleta usuário.
        /// </summary>            
        /// <response code="200">Se o objeto existe</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            
            await _usuarioService.Delete(id);
            return NoContent();
        }
    }
}
