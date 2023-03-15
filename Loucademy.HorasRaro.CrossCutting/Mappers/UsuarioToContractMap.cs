using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Entities;

namespace Loucademy.HorasRaro.CrossCutting.Mappers
{
    public class UsuarioToContractMap : Profile
    {
        public UsuarioToContractMap()
        {
            CreateMap<Usuario, UsuarioRequest>().ReverseMap();
            CreateMap<Usuario, UsuarioResponse>().ReverseMap();
            CreateMap<Usuario, UsuarioAlteracaoRequest >().ReverseMap();
            CreateMap<Usuario, UsuarioRoleRequest>().ReverseMap();
            CreateMap<Usuario, UsuarioSenhaRequest>().ReverseMap();
            
        }
    }
}
