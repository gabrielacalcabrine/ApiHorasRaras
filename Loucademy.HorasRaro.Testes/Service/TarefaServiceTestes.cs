using AutoFixture;
using AutoMapper;
using Bogus;
using Loucademy.HorasRaro.Domain;
using Loucademy.HorasRaro.Domain.Contracts.TarefaContracts;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Settings;
using Loucademy.HorasRaro.Service;
using Loucademy.HorasRaro.Testes.CrossCutting;
using Loucademy.HorasRaro.Testes.Fakers;
using Microsoft.AspNetCore.Http;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.Service
{
    public class TarefaServiceTestes
    {
        private readonly Fixture _fixture = FixtureConfig.Get();
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        private readonly Mock<AppSettings> _mockAppSetting = new Mock<AppSettings>();
        private readonly Mock<ILogService> _mockLogService = new Mock<ILogService>();
        private readonly Mock<ITarefaRepository> _mockTarefaRepository = new Mock<ITarefaRepository>();
        private readonly Mock<IProjetoRepository> _mockProjetoRepository = new Mock<IProjetoRepository>();
        private readonly Mock<IProjetoUsuarioRepository> _mockProjetoUsuarioRepository = new Mock<IProjetoUsuarioRepository>();
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        private readonly Faker _faker = new Faker();
        public IMapper _mapper = new AutoMapperFixture().GetMapper();

        [Fact(DisplayName = "Cadastra nova tarefa usuario")]
        public async Task Post()
        {
            var tarefaRequest = _fixture.Create<TarefaCadastroRequest>();
            var tarefaResponse = _fixture.Create<Task<TarefaResponse>>();
            var projetoEntity = _fixture.Create<Task<Projeto>>();
            var usuarioEntity = _fixture.Create<Task<Usuario>>();
            var tarefaEntity = _fixture.Create<Task<Tarefa>>();

            tarefaRequest.UsuarioId = usuarioEntity.Id;
            tarefaRequest.ProjetoId = projetoEntity.Id;

            var projetoUsuario = _fixture.Create<Task<ProjetoUsuario>>();
            projetoUsuario.Result.ProjetoId = tarefaRequest.ProjetoId;

            _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Projeto, bool>>>())).Returns(projetoEntity);
            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(usuarioEntity);
            _mockProjetoUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoUsuario, bool>>>())).Returns(projetoUsuario);
            _mockTarefaRepository.Setup(mock => mock.AddAsync(It.IsAny<Tarefa>())).Returns(tarefaEntity);
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new TarefaService(_mockTarefaRepository.Object,
                                        _mockUsuarioRepository.Object,
                                        _mapper,
                                        _mockHttpContextAccessor.Object,
                                        _mockProjetoRepository.Object,
                                        _mockProjetoUsuarioRepository.Object,
                                        _mockLogService.Object);
            var result = await service.Post(tarefaRequest);

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Busca todas as tarefas")]
        public async Task Get()
        {
            var tarefas = _fixture.Create<Task<IEnumerable<Tarefa>>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());

            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockTarefaRepository.Setup(mock => mock.ListAsync((It.IsAny<Expression<Func<Tarefa, bool>>>()))).Returns(tarefas);
            var service = new TarefaService(_mockTarefaRepository.Object,
                                        _mockUsuarioRepository.Object,
                                        _mapper,
                                        _mockHttpContextAccessor.Object,
                                        _mockProjetoRepository.Object,
                                        _mockProjetoUsuarioRepository.Object,
                                        _mockLogService.Object);
            var result = service.Get();
            Assert.NotNull(result);
        }
        [Fact(DisplayName = "Busca tarefas por id")]
        public async Task GetId()
        {
            int id = _fixture.Create<int>();
            var TarefaEntity = _fixture.Create<Task<Tarefa>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockTarefaRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Tarefa, bool>>>())).Returns(TarefaEntity);
            var service = new TarefaService(_mockTarefaRepository.Object,
                                        _mockUsuarioRepository.Object,
                                        _mapper,
                                        _mockHttpContextAccessor.Object,
                                        _mockProjetoRepository.Object,
                                        _mockProjetoUsuarioRepository.Object,
                                        _mockLogService.Object);

            var result = await service.GetById(id);
            Assert.Equal(result.Descricao, TarefaEntity.Result.Descricao);
        }

        [Fact(DisplayName = "Busca tarefas por id de projeto")]
        public async Task GetIdProjeto()
        {
            int id = _fixture.Create<int>();
            var TarefaEntity = _fixture.Create<Task<IEnumerable<Tarefa>>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockTarefaRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Tarefa, bool>>>())).Returns(TarefaEntity);
            var service = new TarefaService(_mockTarefaRepository.Object,
                                        _mockUsuarioRepository.Object,
                                        _mapper,
                                        _mockHttpContextAccessor.Object,
                                        _mockProjetoRepository.Object,
                                        _mockProjetoUsuarioRepository.Object,
                                        _mockLogService.Object);

            var result = await service.GetTarefaByIDProjeto(id);
            Assert.True(result.Count() > 0);
        }

        [Fact(DisplayName = "Busca tarefas por id de usuario")]
        public async Task GetIdUsuario()
        {
            int id = _fixture.Create<int>();
            var TarefaEntity = _fixture.Create<Task<IEnumerable<Tarefa>>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockTarefaRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Tarefa, bool>>>())).Returns(TarefaEntity);
            var service = new TarefaService(_mockTarefaRepository.Object,
                                        _mockUsuarioRepository.Object,
                                        _mapper,
                                        _mockHttpContextAccessor.Object,
                                        _mockProjetoRepository.Object,
                                        _mockProjetoUsuarioRepository.Object,
                                        _mockLogService.Object);

            var result = await service.GetTarefaPorIdUsuario(id);
            Assert.True(result.Count() > 0);
        }
        [Fact(DisplayName = "Put tarefa")]
        public async Task Put()
        {
            var tarefaRequest = _fixture.Create<TarefaRequest>();
            var tarefaEntity = _fixture.Create<Task<Tarefa>>();
            tarefaEntity.Result.Ativo = true;
            tarefaEntity.Result.HoraInicio = tarefaRequest.HoraInicio;
            _mockTarefaRepository.Setup(mock => mock.FindAsync(tarefaEntity.Result.Id)).Returns(tarefaEntity);
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);          

            _mockTarefaRepository.Setup(mock => mock.EditAsync(It.IsAny<Tarefa>())).Returns(tarefaEntity);

            var service = new TarefaService(_mockTarefaRepository.Object,
                                        _mockUsuarioRepository.Object,
                                        _mapper,
                                        _mockHttpContextAccessor.Object,
                                        _mockProjetoRepository.Object,
                                        _mockProjetoUsuarioRepository.Object,
                                        _mockLogService.Object);
            var result = await service.Put(tarefaRequest, tarefaEntity.Result.Id);
            Assert.Equal(result.HoraInicio, tarefaRequest.HoraInicio);
        }

        [Fact(DisplayName = "Patch terminar tarefa")]
        public async Task Patch ()
        {
            var tarefaEntity = _fixture.Create<Task<Tarefa>>();
            _mockTarefaRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Tarefa, bool>>>())).Returns(tarefaEntity);
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new TarefaService(_mockTarefaRepository.Object,
                                        _mockUsuarioRepository.Object,
                                        _mapper,
                                        _mockHttpContextAccessor.Object,
                                        _mockProjetoRepository.Object,
                                        _mockProjetoUsuarioRepository.Object,
                                        _mockLogService.Object);
            var result = service.PatchTerminarTarefa(tarefaEntity.Result.Id);
            Assert.True(result.IsCompleted);
        }

        [Fact(DisplayName = "Cadastra nova tarefa")]
        public async Task PostTarefaCompleta()
        {
            var tarefaRequest = _fixture.Create<TarefaCompletaRequest>();
            var tarefaResponse = _fixture.Create<Task<TarefaResponse>>();
            var projetoEntity = _fixture.Create<Task<Projeto>>();
            var usuarioEntity = _fixture.Create<Task<Usuario>>();
            var tarefaEntity = _fixture.Create<Task<Tarefa>>();

            tarefaRequest.UsuarioId = usuarioEntity.Id;
            tarefaRequest.ProjetoId = projetoEntity.Id;

            var projetoUsuario = _fixture.Create<Task<ProjetoUsuario>>();
            projetoUsuario.Result.ProjetoId = tarefaRequest.ProjetoId;

            _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Projeto, bool>>>())).Returns(projetoEntity);
            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(usuarioEntity);
            _mockProjetoUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoUsuario, bool>>>())).Returns(projetoUsuario);
            _mockTarefaRepository.Setup(mock => mock.AddAsync(It.IsAny<Tarefa>())).Returns(tarefaEntity);
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new TarefaService(_mockTarefaRepository.Object,
                                        _mockUsuarioRepository.Object,
                                        _mapper,
                                        _mockHttpContextAccessor.Object,
                                        _mockProjetoRepository.Object,
                                        _mockProjetoUsuarioRepository.Object,
                                        _mockLogService.Object);
            var result = await service.Post(tarefaRequest);

            Assert.NotNull(result);
        }
    }
}
