using Loucademy.HorasRaro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Interfaces.Repositorys
{
    public interface ITogglRepository
    {
        Task<IEnumerable<Datum>> GetTarefa(string? userAgent, string? since, string? workspace, string? until, string? token);
    }
}
