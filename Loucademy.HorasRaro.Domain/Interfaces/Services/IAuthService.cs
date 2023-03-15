using Loucademy.HorasRaro.Domain.Contracts.AuthContracts;
using Loucademy.HorasRaro.Domain.Entities;

namespace Loucademy.HorasRaro.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> AutenticarAsync(string email, string senha);
        Task EnviaEmailConfirmacao(Usuario usuario);
        Task<string> ConfirmaEmailAsync(string token);
        Task<string> EnviaCodigoNovaSenhaAsync(EsqueciSenhaRequest email);
    }
}
