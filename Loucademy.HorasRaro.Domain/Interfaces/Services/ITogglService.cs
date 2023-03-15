using Loucademy.HorasRaro.Domain.Contracts;
using Loucademy.HorasRaro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Interfaces.Services
{
    public interface ITogglService
    {
        // Task<Projeto> GetProjetoToggl(string email, string senha);
        Task<IEnumerable<TooglResponse>> GetTarefa(string? userAgent, string? since, string? workspace, string? until, string? token, int? usuarioId);
    }
}
