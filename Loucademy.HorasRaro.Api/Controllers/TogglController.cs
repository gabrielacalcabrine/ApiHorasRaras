using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Service;
using Microsoft.AspNetCore.Mvc;

namespace Loucademy.HorasRaro.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TogglController : ControllerBase
    {
        private readonly ITogglService _togglService;

        public TogglController(ITogglService togglService)
        {

            _togglService = togglService;

        }

        [HttpGet]
        [Route("Toggl")]
        public async Task<ActionResult<IEnumerable<TooglResponse>>> GetTarefas([FromQuery] string? userAgent, string? since, string? workspace, string? until, string? token, int usuarioId)
        {
            var tarefas = await _togglService.GetTarefa(userAgent, since, workspace, until, token, usuarioId);
            return Ok(tarefas);
        }
    }
}
