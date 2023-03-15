using AutoMapper;
using Loucademy.HorasRaro.CrossCutting.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.Configs
{
    public abstract class BaseAutoMapperFixture
    {
        public IMapper mapper { get; set; }

        public BaseAutoMapperFixture()
        {
            mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration(cnf =>
                {
                    cnf.AddProfile(new UsuarioToContractMap());
                    cnf.AddProfile(new ProjetoToContractMap());
                    cnf.AddProfile(new TarefaToContractMap());
                    cnf.AddProfile(new TarefaTogglToContractMap());
                    cnf.AddProfile(new ProjetoUsuarioToContractMap());
                });

                return config.CreateMapper();
            }

            public void Dispose() { }
        }
    }
}
