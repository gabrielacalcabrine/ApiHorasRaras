using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts.ProjetoContracts;
using Loucademy.HorasRaro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.CrossCutting.Mappers
{
    public class ProjetoUsuarioToContractMap : Profile
    {
        public ProjetoUsuarioToContractMap()
        {
            CreateMap<ProjetoUsuario, ProjetoUsuarioRequest>().ReverseMap();
            CreateMap<ProjetoUsuario, ProjetoUsuariosResponse>().ReverseMap();
            CreateMap<ProjetoUsuarioRequest, ProjetoUsuariosResponse>().ReverseMap();
        }
    }
}
