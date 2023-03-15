using Bogus;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain;
using Loucademy.HorasRaro.Service;
using Loucademy.HorasRaro.Testes.Fakers;
using AutoFixture;
using AutoMapper;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Moq;
using Loucademy.HorasRaro.Testes.CrossCutting;
using Loucademy.HorasRaro.Domain.Contracts.RelatorioContracts;
using Loucademy.HorasRaro.Repository.Context;

namespace Loucademy.HorasRaro.Testes.Service
{
    public class RelatorioServiceTestes
    {
        private readonly Fixture _fixture = FixtureConfig.Get();
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        private readonly Mock<AppSettings> _mockAppSetting = new Mock<AppSettings>();
        private readonly Mock<ILogService> _mockLogService = new Mock<ILogService>();
        private readonly Mock<IRelatorioService> _mockRelatorioRepository = new Mock<IRelatorioService>();
        private readonly Mock<HorasRarasApiContext> _mockHorasRarasContext = new Mock<HorasRarasApiContext>();
      
        private readonly Faker _faker = new Faker();
        public IMapper _mapper = new AutoMapperFixture().GetMapper();

        [Fact(DisplayName = "RelatorioDia")]
        public async Task RelatorioDia()
        {
            var relatotio = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Colaborador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new RelatorioService(_mockHttpContextAccessor.Object, _mockHorasRarasContext.Object);
            var result = service.MeuRelatorioHoje();
            Assert.NotNull(result);
        }
        [Fact(DisplayName = "RelatorioSemana")]
        public async Task RelatorioSemana()
        {
            var relatotio = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Colaborador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new RelatorioService(_mockHttpContextAccessor.Object, _mockHorasRarasContext.Object);
            var result = service.MeuRelatorioSemana();
            Assert.NotNull(result);
        }
        [Fact(DisplayName = "RelatorioMes")]
        public async Task RelatorioMes()
        {
            var relatotio = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Colaborador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new RelatorioService(_mockHttpContextAccessor.Object, _mockHorasRarasContext.Object);
            var result = service.MeuRelatorioMes();
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "RelatorioAdmnDia")]
        public async Task RelatorioDiaAdm()
        {
            var relatotio = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            var usuario = _fixture.Create<Usuario>();
            var projeto = _fixture.Create<Projeto>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new RelatorioService(_mockHttpContextAccessor.Object, _mockHorasRarasContext.Object);
            var result = service.RelatorioAdminHoje(usuario.Id, projeto.Id);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Relatorio Admn Semana")]
        public async Task RelatorioSemanaAdm()
        {
            var relatotio = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            var usuario = _fixture.Create<Usuario>();
            var projeto = _fixture.Create<Projeto>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new RelatorioService(_mockHttpContextAccessor.Object, _mockHorasRarasContext.Object);
            var result = service.RelatorioAdminSemana(usuario.Id, projeto.Id);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Relatorio Admn Mes")]
        public async Task RelatorioSemanaMes()
        {
            var relatotio = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            var usuario = _fixture.Create<Usuario>();
            var projeto = _fixture.Create<Projeto>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new RelatorioService(_mockHttpContextAccessor.Object, _mockHorasRarasContext.Object);
            var result = service.RelatorioAdminMes(usuario.Id, projeto.Id);
            Assert.NotNull(result);
        }

    }
}
