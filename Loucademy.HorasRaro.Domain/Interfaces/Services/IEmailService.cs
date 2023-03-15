using Loucademy.HorasRaro.Domain.Entities;

namespace Loucademy.HorasRaro.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task ConfiguraEnvio(Usuario usuario);
        Task DefineAssuntoEmail(string assunto);
        Task DefineCorpoEmail(string corpo);
        Task EnviaEmail();
    }
}
