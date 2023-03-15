using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Repository.Context;

namespace Loucademy.HorasRaro.Repository.Repositories
{
    public class CodigoConfirmacaoRepository : BaseRepository<CodigoConfirmacao>, ICodigoConfirmacaoRepository
    {
        public CodigoConfirmacaoRepository(HorasRarasApiContext context) : base(context)
        {
        }
    }
}

