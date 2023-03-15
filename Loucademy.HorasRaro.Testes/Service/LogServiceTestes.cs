using AutoFixture;
using AutoMapper;
using Bogus;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Service;
using Loucademy.HorasRaro.Testes.CrossCutting;
using Loucademy.HorasRaro.Testes.Fakers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.Service
{
    public class LogServiceTestes
    {
        private readonly Mock<ILogRepository> _mockLogRepository = new Mock<ILogRepository>();
        private readonly Faker _faker = new Faker();
        public IMapper _mapper = new AutoMapperFixture().GetMapper();
        private readonly Fixture _fixture = FixtureConfig.Get();

        [Fact(DisplayName = "Teste log service")]
        public async Task Log()
        {
            var contexto = _fixture.Create<string>();
            var DadosAntigos = _fixture.Create<Usuario>();
            var DadosNovos = _fixture.Create<Usuario>();
            int IdUsuario = _fixture.Create<int>();
            var log = _fixture.Create<Log>();
            var logTask = _fixture.Create<Task<Log>>();
            _mockLogRepository.Setup(mock => mock.AddAsync(log)).Returns(logTask);
            var service = new LogService(_mockLogRepository.Object);
            var result = service.Log(contexto, IdUsuario, DadosAntigos, DadosNovos);
            Assert.Equal(result.Status, logTask.Status);
        }
    }
}
