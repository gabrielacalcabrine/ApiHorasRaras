using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Repository.Context;


namespace Loucademy.HorasRaro.Repository.Repositories
{
    public class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(HorasRarasApiContext context) : base(context)
        {
        }
    }
}
