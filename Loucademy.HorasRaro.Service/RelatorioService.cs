using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts.RelatorioContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Repository.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Service
{
    public class RelatorioService : BaseService, IRelatorioService
    {
        private readonly HorasRarasApiContext _context;

        public RelatorioService(IHttpContextAccessor httpContextAccessor, HorasRarasApiContext context)
            : base(httpContextAccessor)
        {
            _context = context;
        }

        public async Task<IEnumerable<RelatorioResponse>> MeuRelatorioHoje()
        {
            return _context.Tarefas
                .Where(x => x.UsuarioId.Equals(UsuarioId) &&
                x.HoraInicio.Value.Date == DateTime.Today &&
                x.HoraFim.Value.Date == DateTime.Today &&
                x.Ativo)
                .Select(x => new RelatorioResponse
                {
                    Id = x.Id,
                    NomeProjeto = x.Projeto.Nome,
                    Data = x.HoraInicio.Value.Date,
                    HoraInicio = x.HoraInicio.Value.TimeOfDay,
                    HoraFim = x.HoraFim.Value.TimeOfDay,
                    TotalHoras = x.HoraFim.Value.TimeOfDay.TotalMinutes != null ?
                                 TimeSpan.FromMinutes(x.HoraFim.Value.TimeOfDay.TotalMinutes - x.HoraInicio.Value.TimeOfDay.TotalMinutes) :
                                 null,
                    PodeReajuste = x.HoraFim.Value.AddDays(2) > DateTime.Now
                })
                .ToList();
        }

        public async Task<IEnumerable<RelatorioResponse>> MeuRelatorioSemana()
        {
            return _context.Tarefas
                .Where(x => x.UsuarioId.Equals(UsuarioId) &&
                x.HoraInicio.Value.Date >= DateTime.Today.AddDays(-7) &&
                x.HoraFim.Value.Date <= DateTime.Today &&
                x.Ativo)
                .Select(x => new RelatorioResponse
                {
                    Id = x.Id,
                    NomeProjeto = x.Projeto.Nome,
                    Data = x.HoraInicio.Value.Date,
                    HoraInicio = x.HoraInicio.Value.TimeOfDay,
                    HoraFim = x.HoraFim.Value.TimeOfDay,
                    TotalHoras = x.HoraFim.Value.TimeOfDay.TotalMinutes != null ?
                                 TimeSpan.FromMinutes(x.HoraFim.Value.TimeOfDay.TotalMinutes - x.HoraInicio.Value.TimeOfDay.TotalMinutes) :
                                 null,
                    PodeReajuste = x.HoraFim.Value.AddDays(2) > DateTime.Now
                })
                .ToList();
        }

        public async Task<IEnumerable<RelatorioResponse>> MeuRelatorioMes()
        {
            return _context.Tarefas
                .Where(x => x.UsuarioId.Equals(UsuarioId) &&
                x.HoraInicio.Value.Month == DateTime.Today.Month &&
                x.HoraFim.Value.Month == DateTime.Today.Month &&
                x.Ativo)
                .Select(x => new RelatorioResponse
                {
                    Id = x.Id,
                    NomeProjeto = x.Projeto.Nome,
                    Data = x.HoraInicio.Value.Date,
                    HoraInicio = x.HoraInicio.Value.TimeOfDay,
                    HoraFim = x.HoraFim.Value.TimeOfDay,
                    TotalHoras = x.HoraFim.Value.TimeOfDay.TotalMinutes != null ?
                                 TimeSpan.FromMinutes(x.HoraFim.Value.TimeOfDay.TotalMinutes - x.HoraInicio.Value.TimeOfDay.TotalMinutes) :
                                 null,
                    PodeReajuste = x.HoraFim.Value.AddDays(2) > DateTime.Now
                })
                .ToList();
        }

        public async Task<IEnumerable<RelatorioResponse>> RelatorioAdminHoje(int? UsuarioId, int? ProjetoId)
        {
            return _context.Tarefas
                .Where(x =>
                UsuarioId != null ? x.UsuarioId.Equals(UsuarioId) : 1 == 1 &&
                ProjetoId != null ? x.ProjetoId.Equals(ProjetoId) : 1 == 1 &&
                x.HoraInicio.Value.Date == DateTime.Today &&
                x.HoraFim.Value.Date == DateTime.Today &&
                x.Ativo)
                .Select(x => new RelatorioResponse
                {
                    Id = x.Id,
                    NomeProjeto = x.Projeto.Nome,
                    Data = x.HoraInicio.Value.Date,
                    HoraInicio = x.HoraInicio.Value.TimeOfDay,
                    HoraFim = x.HoraFim.Value.TimeOfDay,
                    TotalHoras = x.HoraFim.Value.TimeOfDay.TotalMinutes != null ?
                                 TimeSpan.FromMinutes(x.HoraFim.Value.TimeOfDay.TotalMinutes - x.HoraInicio.Value.TimeOfDay.TotalMinutes) :
                                 null,
                    PodeReajuste = true
                })
                .ToList();
        }

        public async Task<IEnumerable<RelatorioResponse>> RelatorioAdminSemana(int? UsuarioId, int? ProjetoId)
        {
            return _context.Tarefas
                .Where(x =>
                UsuarioId != null ? x.UsuarioId.Equals(UsuarioId) : 1 == 1 &&
                ProjetoId != null ? x.ProjetoId.Equals(ProjetoId) : 1 == 1 &&
                x.HoraInicio.Value.Date >= DateTime.Today.AddDays(-7) &&
                x.HoraFim.Value.Date <= DateTime.Today &&
                x.Ativo)
                .Select(x => new RelatorioResponse
                {
                    Id = x.Id,
                    NomeProjeto = x.Projeto.Nome,
                    Data = x.HoraInicio.Value.Date,
                    HoraInicio = x.HoraInicio.Value.TimeOfDay,
                    HoraFim = x.HoraFim.Value.TimeOfDay,
                    TotalHoras = x.HoraFim.Value.TimeOfDay.TotalMinutes != null ?
                                 TimeSpan.FromMinutes(x.HoraFim.Value.TimeOfDay.TotalMinutes - x.HoraInicio.Value.TimeOfDay.TotalMinutes) :
                                 null,
                    PodeReajuste = true
                })
                .ToList();
        }

        public async Task<IEnumerable<RelatorioResponse>> RelatorioAdminMes(int? UsuarioId, int? ProjetoId)
        {
            return _context.Tarefas
                .Where(x =>
                UsuarioId != null ? x.UsuarioId.Equals(UsuarioId) : 1 == 1 &&
                ProjetoId != null ? x.ProjetoId.Equals(ProjetoId) : 1 == 1 &&
                x.HoraInicio.Value.Month == DateTime.Today.Month &&
                x.HoraFim.Value.Month == DateTime.Today.Month &&
                x.Ativo)
                .Select(x => new RelatorioResponse
                {
                    Id = x.Id,
                    NomeProjeto = x.Projeto.Nome,
                    Data = x.HoraInicio.Value.Date,
                    HoraInicio = x.HoraInicio.Value.TimeOfDay,
                    HoraFim = x.HoraFim.Value.TimeOfDay,
                    TotalHoras = x.HoraFim.Value.TimeOfDay.TotalMinutes != null ?
                                 TimeSpan.FromMinutes(x.HoraFim.Value.TimeOfDay.TotalMinutes - x.HoraInicio.Value.TimeOfDay.TotalMinutes) :
                                 null,
                    PodeReajuste = true
                })
                .ToList();
        }

        public async Task<RelatorioHorasProjetoResponse> TotalDeHorasPorProjeto(RelatorioHorasProjetoRequest Request)
        {
            if (Request.ProjetoId == 0 || Request.DataInicio == new DateTime() { } || Request.DataFim == new DateTime() { })
            {
                throw new ArgumentNullException($"Todos os dados deverão ser informados");
            }
            var TotalHoras = _context.Tarefas
                .Where(x =>
                x.ProjetoId.Equals(Request.ProjetoId) &&
                x.HoraInicio.Value >= Request.DataInicio &&
                x.HoraFim.Value <= Request.DataFim &&
                x.Ativo)
                .Select(x => x.HoraFim.Value.TimeOfDay.TotalMinutes != null ?
                                x.HoraFim.Value.TimeOfDay.TotalMinutes - x.HoraInicio.Value.TimeOfDay.TotalMinutes :
                                0
                ).ToList().Sum();
            return new RelatorioHorasProjetoResponse()
            {
                ProjetoId  = Request.ProjetoId,
                TotalHoras = TimeSpan.FromMinutes(TotalHoras)
            };
        }
    }
}
