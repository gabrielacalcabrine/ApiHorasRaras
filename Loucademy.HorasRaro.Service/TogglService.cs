using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts;
using Loucademy.HorasRaro.Domain.Contracts.TarefaContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Repository.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Service
{
    public class TogglService : ITogglService
    {
        private readonly ITogglRepository _togglRepository;
        private readonly IMapper _mapper;
        private readonly ITarefaService _tarefaService;
        private readonly HorasRarasApiContext _context;
        private readonly IProjetoRepository _projetoRepository;

        public TogglService(ITogglRepository togglRepository, IMapper mapper, HorasRarasApiContext context, ITarefaService tarefaService, IProjetoRepository projetoRepository)
        {
            _togglRepository = togglRepository;
            _mapper = mapper;
            _context = context;
            _tarefaService = tarefaService;
            _projetoRepository = projetoRepository;
        }

       public async Task<IEnumerable<TooglResponse>> GetTarefa(string? userAgent, string? since, string? workspace, string? until, string? token, int? usuarioId)
       {
            var tarefaToggl = await _togglRepository.GetTarefa(userAgent, since, workspace, until, token);
            var tarefasToggl = _mapper.Map<IEnumerable<TooglResponse>>(tarefaToggl);

            foreach (TooglResponse tarefa in tarefasToggl)
            {
                var projeto = await _projetoRepository.FindAsync(p => p.Nome.Contains(tarefa.Project));

                if (projeto is null)
                {
                    continue;
                }
                if (! await _tarefaService.UsuarioPertenceAProjeto((int) usuarioId, projeto.Id))
                {
                    continue;
                }

                var tarefaContract = new TarefaCompletaRequest()
                {
                    ProjetoId = projeto.Id,
                    UsuarioId = (int)usuarioId,
                    Descricao = tarefa.Description,
                    HoraInicio = DateTime.Parse(tarefa.Start.ToString()),
                    HoraFim = DateTime.Parse(tarefa.End.ToString())
                };

                await _tarefaService.Post(tarefaContract);
            }
   
            return tarefasToggl;
       }
    }
}
