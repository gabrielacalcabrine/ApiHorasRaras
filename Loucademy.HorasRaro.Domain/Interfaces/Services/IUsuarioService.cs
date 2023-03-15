using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;

namespace Loucademy.HorasRaro.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<UsuarioRequest, UsuarioResponse>
    {
        Task<UsuarioResponse> PutUsuario(UsuarioAlteracaoRequest request, int? id);
        Task<UsuarioResponse> PatchRole(int id, UsuarioRoleRequest usuarioRole);
        Task<UsuarioResponse> Patch(UsuarioSenhaRequest request);
    }
}