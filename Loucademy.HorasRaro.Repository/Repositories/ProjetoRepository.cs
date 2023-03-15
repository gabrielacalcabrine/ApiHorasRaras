using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Repository.Context;

namespace Loucademy.HorasRaro.Repository.Repositories
{
    public class ProjetoRepository : BaseRepository<Projeto>, IProjetoRepository
    {
        public ProjetoRepository(HorasRarasApiContext context) : base(context)
        {
        }
    }
}
