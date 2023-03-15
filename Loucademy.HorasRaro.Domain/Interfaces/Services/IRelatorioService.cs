using Loucademy.HorasRaro.Domain.Contracts.RelatorioContracts;

namespace Loucademy.HorasRaro.Domain.Interfaces.Services
{
    public interface IRelatorioService
    {
        Task<IEnumerable<RelatorioResponse>> MeuRelatorioHoje();
        Task<IEnumerable<RelatorioResponse>> MeuRelatorioSemana();
        Task<IEnumerable<RelatorioResponse>> MeuRelatorioMes();
        Task<IEnumerable<RelatorioResponse>> RelatorioAdminHoje(int? UsuarioId, int? ProjetoId);
        Task<IEnumerable<RelatorioResponse>> RelatorioAdminSemana(int? UsuarioId, int? ProjetoId);
        Task<IEnumerable<RelatorioResponse>> RelatorioAdminMes(int? UsuarioId, int? ProjetoId);
        Task<RelatorioHorasProjetoResponse> TotalDeHorasPorProjeto(RelatorioHorasProjetoRequest Request);
    }
}
