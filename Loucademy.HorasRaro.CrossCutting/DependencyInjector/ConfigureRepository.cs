using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Repository.Context;
using Loucademy.HorasRaro.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Loucademy.HorasRaro.CrossCutting.DependencyInjector
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
            serviceCollection.AddScoped<IProjetoRepository, ProjetoRepository>();
            serviceCollection.AddScoped<ICodigoConfirmacaoRepository, CodigoConfirmacaoRepository>();
            serviceCollection.AddScoped<IProjetoUsuarioRepository, ProjetoUsuarioRepository>();
            serviceCollection.AddScoped<ITarefaRepository, TarefaRepository>();
            serviceCollection.AddScoped<ITogglRepository, TogglRepository>();
            serviceCollection.AddScoped<ILogRepository, LogRepository>();


            serviceCollection.AddDbContext<HorasRarasApiContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
