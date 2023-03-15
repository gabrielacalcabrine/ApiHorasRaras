using Loucademy.HorasRaro.CrossCutting.DependencyInjector;
using Microsoft.Extensions.DependencyInjection;

namespace Loucademy.HorasRaro.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterAppDependencies(this IServiceCollection services)
        {
            ConfigureService.ConfigureDependenciesService(services);
            ConfigureMappers.ConfigureDependenciesMapper(services);
        }

        public static void RegisterAppDependenciesContext(this IServiceCollection services, string connectionString)
        {
            ConfigureRepository.ConfigureDependenciesRepository(services, connectionString);
        }
    }
}
