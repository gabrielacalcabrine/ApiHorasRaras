using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts.RelatorioContracts;
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
    public class RelatorioController : ControllerBase
    {        
        private readonly IRelatorioService _relatorioService;
        

        public RelatorioController(IRelatorioService service)
        {
            _relatorioService = service;
            
            
        }

        /// <summary>
        /// Busca todas as atividades do dia.
        /// </summary>
        /// <returns>Atividades do dia</returns>
        /// <response code="200">Retorna as atividades ativas do dia</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpGet("meu-relatorio-hoje")]
        [SwaggerOperation(Summary = "Busca suas atividades do dia", Description = "Retorna atividades do dia")]
        public async Task<ActionResult<IEnumerable<RelatorioResponse>>> MeuRelatorioHoje()
        {
            var response = await _relatorioService.MeuRelatorioHoje();
            return Ok(response);
        }

        /// <summary>
        /// Busca todas as atividades da semana.
        /// </summary>
        /// <returns>Atividades da semana</returns>
        /// <response code="200">Retorna as atividades ativas da semana</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpGet("meu-relatorio-semana")]
        [SwaggerOperation(Summary = "Busca suas atividades da semana", Description = "Retorna atividades da semana")]
        public async Task<ActionResult<IEnumerable<RelatorioResponse>>> MeuRelatorioSemana()
        {
            var response = await _relatorioService.MeuRelatorioSemana();
            return Ok(response);
        }

        /// <summary>
        /// Busca todas as atividades do mês.
        /// </summary>
        /// <returns>Atividades do mês</returns>
        /// <response code="200">Retorna as atividades ativas do mês</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpGet("meu-relatorio-mes")]
        [SwaggerOperation(Summary = "Busca suas atividades do mês", Description = "Retorna atividades do mês")]
        public async Task<ActionResult<IEnumerable<RelatorioResponse>>> MeuRelatorioMes()
        {
            var response = await _relatorioService.MeuRelatorioMes();
            return Ok(response);
        }

        /// <summary>
        /// Busca todas as atividades do dia. Acesso só permitido ao administrador.
        /// </summary>
        /// <returns>Atividades do dia</returns>
        /// <response code="200">Retorna as atividades ativas do dia</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpGet("relatorio-admin-hoje")]
        [SwaggerOperation(Summary = "Busca atividades do dia", Description = "Retorna atividades do dia")]
        public async Task<ActionResult<IEnumerable<RelatorioResponse>>> RelatorioAdminHoje(int? UsuarioId, int? ProjetoId)
        {
            var response = await _relatorioService.RelatorioAdminHoje(UsuarioId, ProjetoId);
            return Ok(response);
        }

        /// <summary>
        /// Busca todas as atividades da semana. Acesso só permitido ao administrador.
        /// </summary>
        /// <returns>Atividades da semana</returns>
        /// <response code="200">Retorna as atividades ativas da semana</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpGet("relatorio-admin-semana")]
        [SwaggerOperation(Summary = "Busca atividades da semana", Description = "Retorna atividades da semana")]
        public async Task<ActionResult<IEnumerable<RelatorioResponse>>> RelatorioAdminSemana(int? UsuarioId, int? ProjetoId)
        {
            var response = await _relatorioService.RelatorioAdminSemana(UsuarioId, ProjetoId);
            return Ok(response);
        }

        /// <summary>
        /// Busca todas as atividades do mês. Acesso só permitido ao administrador.
        /// </summary>
        /// <returns>Atividades do mês</returns>
        /// <response code="200">Retorna as atividades ativas do mês</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpGet("relatorio-admin-mes")]
        [SwaggerOperation(Summary = "Busca atividades do mês", Description = "Retorna atividades do mês")]
        public async Task<ActionResult<IEnumerable<RelatorioResponse>>> RelatorioAdminMes(int? UsuarioId, int? ProjetoId)
        {
            var response = await _relatorioService.RelatorioAdminMes(UsuarioId, ProjetoId);
            return Ok(response);
        }

        /// <summary>
        /// Busca todas as atividades do mês. Acesso só permitido ao administrador.
        /// </summary>
        /// <returns>Atividades do mês</returns>
        /// <response code="200">Retorna as atividades ativas do mês</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpGet("total-horas-projeto")]
        [SwaggerOperation(Summary = "Busca atividades do mês", Description = "Retorna atividades do mês")]
        public async Task<ActionResult<RelatorioHorasProjetoResponse>> TotalHorasProjeto([FromQuery] RelatorioHorasProjetoRequest Request)
        {
            var response = await _relatorioService.TotalDeHorasPorProjeto(Request);
            return Ok(response);
        }
    }
}
