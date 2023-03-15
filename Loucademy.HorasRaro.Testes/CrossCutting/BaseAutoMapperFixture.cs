using AutoMapper;
using Loucademy.HorasRaro.CrossCutting.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.CrossCutting
{
    public abstract class BaseAutoMapperFixture
    {
        public IMapper mapper { get; set; }
        public BaseAutoMapperFixture()
        {
            mapper = new AutoMapperFixture().GetMapper();
        }
    }
    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UsuarioToContractMap());
                cfg.AddProfile(new TarefaToContractMap());
                cfg.AddProfile(new TarefaTogglToContractMap());
                cfg.AddProfile(new ProjetoToContractMap());
                cfg.AddProfile(new ProjetoUsuarioToContractMap());
            });

            return config.CreateMapper();
        }

        public void Dispose() { }
    }
}
