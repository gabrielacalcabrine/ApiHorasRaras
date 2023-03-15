using Loucademy.HorasRaro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Interfaces.Services
{
    public interface ILogService
    {
        Task Log(string Contexto, int UsuarioId, Usuario? DadosAntigos, Usuario DadosNovos);
        Task Log(string Contexto, int UsuarioId, Projeto? DadosAntigos, Projeto DadosNovos);
        Task Log(string Contexto, int UsuarioId, Tarefa? DadosAntigos, Tarefa DadosNovos);
    }
}
