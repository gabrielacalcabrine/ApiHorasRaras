using Loucademy.HorasRaro.Domain.Contracts.ProjetoContracts;

namespace Loucademy.HorasRaro.Domain.Interfaces.Services
{
    public interface IProjetoService : IBaseService<ProjetoRequest, ProjetoResponse>
    {
        Task<ProjetoUsuarioRequest> PatchAdicionarUsuario(ProjetoUsuarioRequest projetoRequest, int? id);
        Task PatchRemoveUsuario(int IdProjeto, int IdUsuario);
    }
}