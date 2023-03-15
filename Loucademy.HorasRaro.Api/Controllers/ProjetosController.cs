using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts.ProjetoContracts;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Utils;
using Loucademy.HorasRaro.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace Loucademy.HorasRaro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjetosController : ControllerBase
    {
        private readonly IProjetoService _projetoService;
        

        public ProjetosController(IProjetoService service)
        {
            _projetoService = service;
           
        }

        /// <summary>
        /// Realiza Cadastro de projeto.
        /// </summary>
        /// <returns>Projeto cadastrado</returns>
        /// <response code="201">Retorna projeto cadastrado</response>
        /// <response code="400">Se o item não for criado</response> 
        [HttpPost]
        [ProducesResponseType(201)]
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [SwaggerOperation(Summary = "Cadastra um novo projeto na aplicação", Description = "Retorna dados do projeto")]
        public async Task<ActionResult<ProjetoResponse>> PostAsync([FromBody] ProjetoRequest projetoRequest)
        {
            var projetoCadastrado = await _projetoService.Post(projetoRequest);            
            return Ok(projetoCadastrado);

        }

        /// <summary>
        /// Busca todos os projetos cadastrados e ativos.
        /// </summary>
        /// <returns>Projetos cadastrados</returns>
        /// <response code="200">Retorna os projetos cadastrados</response>
        /// <response code="403">Se o acesso for negado</response>
        [HttpGet()]
        [ProducesResponseType(200)]
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [SwaggerOperation(Summary = "Busca uma de lista de projetos existentes", Description = "Retorna uma listagem de projetos cadastrados")]
        public async Task<ActionResult<IEnumerable<ProjetoResponse>>> GetAsync()
        {
            var projetoLista = await _projetoService.Get();
            return Ok(projetoLista);
        }

        /// <summary>
        /// Busca projeto cadastrado por id.
        /// </summary>
        /// <returns>Projeto cadastrado</returns>
        /// <response code="200">Retorna projeto caso ele exista e/ou esteja ativo</response>
        /// <response code="403">Se o acesso for negado</response>
        /// <response code="404">Se o objeto não existe</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [SwaggerOperation(Summary = "Busca um projeto existente a partir do id", Description = "Retorna um projeto cadastrado a partir do id")]
        public async Task<ActionResult<ProjetoResponse>> GetById([FromRoute] int id)
        {
            var projetoId = await _projetoService.GetById(id);
            return Ok(projetoId);
        }

        /// <summary>
        /// Atribui usuários a projeto já cadastrado.
        /// </summary>
        /// <returns>Lista de ids</returns>
        /// <response code="200">Retorna lista de id de usuários cadastrados</response>
        /// <response code="403">Se o acesso for negado</response>
        /// <response code="404">Se o objeto não existe</response>
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Busca projeto para atribuir novos usuários a ele.", Description = "Acesso só ao administrador.Retorna a lista de usuarios modificada.")]
        public async Task<ActionResult<ProjetoUsuariosResponse>> Patch([FromRoute] int id, [FromBody] ProjetoUsuarioRequest usuario)
        {
            var result = await _projetoService.PatchAdicionarUsuario(usuario, id);            
            return Ok(result);
        }

        /// <summary>
        /// Remove usuários de projeto.
        /// </summary>
        /// <returns>Return NoCotent em caso de sucesso</returns>
        /// <response code="200">Em caso de sucesso</response>
        /// <response code="403">Se o acesso for negado</response>        
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpPatch("remove-projeto-usuario/{projetoId}/{usuarioId}")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Busca projeto para remover usuários.", Description = "Acesso só ao administrador.")]
        public async Task<ActionResult> PatchRemove([FromRoute] int projetoId, int usuarioId)
        {
            await _projetoService.PatchRemoveUsuario(projetoId, usuarioId);           
            return NoContent();
        }

        /// <summary>
        /// Atualiza projeto após busca por Id.
        /// </summary>
        /// <returns>Retorna objeto atualizado</returns>
        /// <response code="200">Em caso de sucesso</response>
        /// <response code="403">Se o acesso for negado</response> 
        /// <response code="404">Se o objeto não existe</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [SwaggerOperation(Summary = "Atualiza um projeto existente a partir do id", Description = "Retorna um projeto editado a partir do id")]
        public async Task<ActionResult<ProjetoResponse>> Put([FromRoute] int id, [FromBody] ProjetoRequest projetoRequest)
        {
            var projetoAtualizado = await _projetoService.Put(projetoRequest, id);            
            return Ok(projetoAtualizado);
        }

        /// <summary>
        /// Exclui projeto logicamente.
        /// </summary>
        /// <returns>Retorna NoContent</returns>
        /// <response code="204">Em caso de sucesso</response>
        /// <response code="403">Se o acesso for negado</response>         
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [SwaggerOperation(Summary = "Exclui um projeto existente a partir do id", Description = "Exclui um projeto cadastrado a partir do id")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {            
            await _projetoService.Delete(id);
            return NoContent();
        }
    }
}