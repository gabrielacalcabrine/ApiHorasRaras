using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts.TarefaContracts;
using Loucademy.HorasRaro.Domain.Entities;


namespace Loucademy.HorasRaro.CrossCutting.Mappers
{
    public class TarefaToContractMap : Profile
    {
        public TarefaToContractMap()
        {
            CreateMap<Tarefa, TarefaRequest>().ReverseMap();
            CreateMap<Tarefa, TarefaResponse>().ReverseMap();
            CreateMap<Tarefa, TarefaCadastroRequest>().ReverseMap();
            CreateMap<Tarefa, TarefaCompletaRequest>().ReverseMap();
        }
    }
}
 