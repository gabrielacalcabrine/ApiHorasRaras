using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Entities;
using System.Text.Json;
using Loucademy.HorasRaro.Domain.Interfaces.Services;

namespace Loucademy.HorasRaro.Service
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository) 
        {
            _logRepository = logRepository;
        }
        public async Task Log(string Contexto, int UsuarioId, Usuario? DadosAntigos , Usuario DadosNovos)
        {
            await _logRepository.AddAsync(new Log
            {
                Contexto = Contexto,
                UsuarioId = UsuarioId,
                DadosAntigos = DadosAntigos is not null ? JsonSerializer.Serialize(DadosAntigos) : null,
                DadosNovos = JsonSerializer.Serialize(DadosNovos),
                DataCriacao = DateTime.Now,
            });
        }

        public async Task Log(string Contexto, int UsuarioId, Projeto? DadosAntigos, Projeto DadosNovos)
        {
            await _logRepository.AddAsync(new Log
            {
                Contexto = Contexto,
                UsuarioId = UsuarioId,
                DadosAntigos = DadosAntigos is not null ? JsonSerializer.Serialize(DadosAntigos) : null,
                DadosNovos = JsonSerializer.Serialize(DadosNovos),
                DataCriacao = DateTime.Now,
            });
        }

        public async Task Log(string Contexto, int UsuarioId, Tarefa? DadosAntigos, Tarefa DadosNovos)
        {
            await _logRepository.AddAsync(new Log
            {
                Contexto = Contexto,
                UsuarioId = UsuarioId,
                DadosAntigos = DadosAntigos is not null ? JsonSerializer.Serialize(DadosAntigos) : null,
                DadosNovos = JsonSerializer.Serialize(DadosNovos),
                DataCriacao = DateTime.Now,
            });
        }
    }
}
