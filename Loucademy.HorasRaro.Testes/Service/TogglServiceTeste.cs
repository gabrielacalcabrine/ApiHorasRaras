using AutoFixture;
using AutoMapper;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Repository.Context;
using Loucademy.HorasRaro.Service;
using Loucademy.HorasRaro.Testes.CrossCutting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.Service
{
    
    public class TogglServiceTeste
    {
        private readonly Mock<ITogglRepository> _mockTogglRepository = new Mock<ITogglRepository>();
        public IMapper _mapper = new AutoMapperFixture().GetMapper();
        private readonly Mock<HorasRarasApiContext> _mockHorasRarasContext = new Mock<HorasRarasApiContext>();
        private readonly Mock<ITarefaService> _mockTarefaService = new Mock<ITarefaService>();
        private readonly Mock<IProjetoRepository> _mockProjetoRepository = new Mock<IProjetoRepository>();
        private readonly Fixture _fixture = new Fixture();       

        [Fact(DisplayName = "Toggl Service Get")]
        public async Task TogglGet()
        {
            var datum = _fixture.Create<Task<IEnumerable<Datum>>>();
            var userAgent = _fixture.Create<string>();
            var since = _fixture.Create<string>();
            var workspace = _fixture.Create<string>();
            var until = _fixture.Create<string>();
            var token = _fixture.Create<string>();
            var usuarioId = _fixture.Create<int>();
            _mockTogglRepository.Setup(mock => mock.GetTarefa(userAgent, since, workspace, until, token)).Returns(datum);
            var service = new TogglService(_mockTogglRepository.Object, 
                                            _mapper, _mockHorasRarasContext.Object, 
                                            _mockTarefaService.Object, 
                                            _mockProjetoRepository.Object);
            var result = service.GetTarefa(userAgent, since, workspace, until, token, usuarioId);
            Assert.NotNull(result);             
        }
    }
}
