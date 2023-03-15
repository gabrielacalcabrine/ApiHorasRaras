using Loucademy.HorasRaro.Domain.Contracts.TarefaContracts;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Interfaces.Services
{
    public interface ITarefaService : IBaseService<TarefaRequest, TarefaResponse>
    {
        Task<IEnumerable<TarefaResponse>> GetTarefaByIDProjeto(int id);
        Task<IEnumerable<TarefaResponse>> GetTarefaPorIdUsuario(int id);
        Task PatchTerminarTarefa(int id);
        Task<TarefaResponse> Post(TarefaCadastroRequest request);
        Task<TarefaResponse> Post(TarefaCompletaRequest request);
        Task<bool> UsuarioPertenceAProjeto(int UsuarioId, int ProjetoId);
    }
}
