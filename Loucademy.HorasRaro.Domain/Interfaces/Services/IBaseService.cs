using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;

namespace Loucademy.HorasRaro.Domain.Interfaces.Services
{
    public interface IBaseService<Request, Response>
    {

        Task<IEnumerable<Response>> Get();

        Task<Response> GetById(int id);

        Task<Response> Post(Request request);

        Task<Response> Put(Request request, int? id);       

        Task Delete(int request);

    
    }
}