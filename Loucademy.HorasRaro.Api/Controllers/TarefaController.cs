using Loucademy.HorasRaro.Domain.Contracts.TarefaContracts;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Utils;
using Loucademy.HorasRaro.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.CompilerServices;

namespace Loucademy.HorasRaro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;
        

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
            
        }

        /// <summary>
        /// Realiza Cadastro de tarefa.
        /// </summary>
        /// <returns>Tarefa cadastrada</returns>
        /// <response code="201">Retorna Tarefa cadastrada</response>
        /// <response code="400">Se o item não for criado</response> 
        [HttpPost]
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [SwaggerOperation(Summary = "Cadastra uma nova tarefa no banco.", Description = "Retorna dados da tarefa.")]
        [ProducesResponseType(201)]
        public async Task<ActionResult<TarefaResponse>> Post([FromBody] TarefaCadastroRequest tarefa)
        {
            var result = await _tarefaService.Post(tarefa);
            return Ok(result);
        }

        /// <summary>
        /// Realiza busca de todas as tarefa.
        /// </summary>
        /// <returns>Todas tarefas cadastradas ativas</returns>
        /// <response code="200">Retorna todas as tarefas</response>
        /// <response code="403">Se o acesso for negado</response> 
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpGet]
        [SwaggerOperation(Summary = "Busca Todos as tarefas ativas.", Description = "Retorna todas as tarefas Ativas.")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<TarefaResponse>>> Get()
        {
            var result = await _tarefaService.Get();
            return Ok(result);
        }
        /// <summary>
        /// Realiza busca de tarefa por Id.
        /// </summary>
        /// <returns>Tarefa</returns>
        /// <response code="200">Retorna tarefa buscada por Id</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca nova tarefa por id.", Description = "Retorna a tarefa se ela estiver ativa.")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<TarefaResponse>> Get(int id)
        {
            var result = await _tarefaService.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Modifica hora fim de tarefa adicionando hora atual.
        /// </summary>        
        /// <response code="200">Se o objeto existe e foi alterado</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Termina tarefa.")]
        public async Task <ActionResult> TerminaTarefa([FromRoute] int id)
        {
            await _tarefaService.PatchTerminarTarefa(id);
            return NoContent();
        }

        /// <summary>
        /// Modifica tarefa buscada.
        /// </summary>   
        ///<returns>Tarefa modificada</returns>
        /// <response code="200">Se o objeto existe e foi alterado</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Busca tarefa para mudança de dados.", Description = "Retorna o tarefa modificada.")]
        public async Task<ActionResult<TarefaResponse>> Put([FromBody] TarefaRequest tarefaAlteracao, [FromRoute] int id)
        {
            var result = await _tarefaService.Put(tarefaAlteracao, (int)id);
            return Ok(result);
        }

        /// <summary>
        /// Busca tarefas de determinado Usuário.
        /// </summary>     
        /// <returns>Tarefas por usuário</returns>
        /// <response code="200">Se o objeto existe</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpGet("buscar-tarefas-por-Usuario/{id}")]
        [SwaggerOperation(Summary = "Busca tarefas por Usuario.", Description = "Retorna as tarefas se elas estiverem ativas.")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<TarefaResponse>>> GetTarefaIdUsuario(int id)
        {
            var result = await _tarefaService.GetTarefaPorIdUsuario(id);
            return Ok(result);
        }

        /// <summary>
        /// Busca tarefas por projeto.
        /// </summary>     
        /// <returns>Tarefas por projeto</returns>
        /// <response code="200">Se o objeto existe</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpGet("buscar-tarefas-por-projeto/{id}")]
        [SwaggerOperation(Summary = "Busca tarefas por projeto.", Description = "Retorna as tarefas se elas estiverem ativas.")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<TarefaResponse>>> GetTarefaIdProjeto(int id)
        { 
            var result = await _tarefaService.GetTarefaByIDProjeto(id);
            return Ok(result);
        }

        /// <summary>
        /// Deleta tarefa.
        /// </summary>            
        /// <response code="200">Se o objeto existe</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {            
            await _tarefaService.Delete(id);
            return NoContent();
        }
    }
}
