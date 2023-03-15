using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Loucademy.HorasRaro.CrossCutting.DependencyInjector
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
            serviceCollection.AddScoped<IProjetoService, ProjetoService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<ITarefaService, TarefaService>();            
            serviceCollection.AddScoped<IEmailService, EmailService>();
            serviceCollection.AddScoped<IRelatorioService, RelatorioService>();
            serviceCollection.AddScoped<ITogglService, TogglService>();
            serviceCollection.AddScoped<ILogService, LogService>();
        }
    }
}
