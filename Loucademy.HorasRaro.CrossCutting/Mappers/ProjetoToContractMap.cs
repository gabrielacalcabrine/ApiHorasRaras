using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts.ProjetoContracts;
using Loucademy.HorasRaro.Domain.Entities;

namespace Loucademy.HorasRaro.CrossCutting.Mappers
{
    public class ProjetoToContractMap : Profile
    {
        public ProjetoToContractMap()
        {
            CreateMap<Projeto, ProjetoRequest>().ReverseMap();
            CreateMap<Projeto, ProjetoResponse>().ReverseMap();
            CreateMap<Projeto, ProjetoUsuariosResponse>().ReverseMap();
        }
    }
}
