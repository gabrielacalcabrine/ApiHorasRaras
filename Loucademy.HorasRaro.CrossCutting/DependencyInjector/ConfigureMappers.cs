using Loucademy.HorasRaro.CrossCutting.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Loucademy.HorasRaro.CrossCutting.DependencyInjector
{
    public class ConfigureMappers
    {
        public static void ConfigureDependenciesMapper(IServiceCollection serviceCollection)
        {
            var config = new AutoMapper.MapperConfiguration(cnf =>
            {
                cnf.AddProfile(new UsuarioToContractMap());
                cnf.AddProfile(new ProjetoToContractMap());
                cnf.AddProfile(new TarefaToContractMap());
                cnf.AddProfile(new TarefaTogglToContractMap());
                cnf.AddProfile(new ProjetoUsuarioToContractMap());
               
            });

            var mapConfiguration = config.CreateMapper();
            serviceCollection.AddSingleton(mapConfiguration);
        }

    }
}