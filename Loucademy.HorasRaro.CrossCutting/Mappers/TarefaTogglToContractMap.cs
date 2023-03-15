using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts;
using Loucademy.HorasRaro.Domain.Contracts.TarefaContracts;
using Loucademy.HorasRaro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.CrossCutting.Mappers
{
    public class TarefaTogglToContractMap : Profile
    {
        public TarefaTogglToContractMap()
        {
            CreateMap<Datum, TooglResponse>().ReverseMap();
            CreateMap<Toggl, TooglResponse>().ReverseMap();
            CreateMap<TooglResponse, Tarefa>()
                 .ForMember(p => p.Descricao, map => map.MapFrom(s => s.Description))
                 .ForMember(p => p.HoraInicio, map => map.MapFrom(s => s.Start))
                 .ForMember(p => p.HoraFim, map => map.MapFrom(s => s.End));


        }
    }
}
