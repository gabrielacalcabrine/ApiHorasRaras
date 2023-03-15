using AutoFixture;
using AutoMapper;
using Bogus;
using Loucademy.HorasRaro.Api.Controllers;
using Loucademy.HorasRaro.Domain;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Settings;
using Loucademy.HorasRaro.Repository.Repositories;
using Loucademy.HorasRaro.Service;
using Loucademy.HorasRaro.Testes.CrossCutting;
using Loucademy.HorasRaro.Testes.Fakers;
using Microsoft.AspNetCore.Http;
using Moq;
using NSubstitute;
using System;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Loucademy.HorasRaro.Testes.Service
{
    public class UsuarioServiceTestes
    {
        private readonly Fixture _fixture = FixtureConfig.Get();
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        private readonly Mock<IAuthService> _mockAuthService = new Mock<IAuthService>();
        private readonly Mock<AppSettings> _mockAppSetting = new Mock<AppSettings>();
        private readonly Mock<ICodigoConfirmacaoRepository> _mockCodigoConfirmaRepository = new Mock<ICodigoConfirmacaoRepository>();
        private readonly Mock<ILogService> _mockLogService = new Mock<ILogService>();
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        private readonly Faker _faker = new Faker();
        public IMapper _mapper = new AutoMapperFixture().GetMapper();

        [Fact(DisplayName = "Cadastra um novo usuario")]
        public async Task Post()
        {
            var userRequest = _fixture.Create<UsuarioRequest>();
            var userEntity = _fixture.Create<Task<Usuario>>();
            userRequest.Nome = userEntity.Result.Nome;
            userRequest.Email = userEntity.Result.Email;
            userRequest.Senha = userEntity.Result.Senha;

            var service = new UsuarioService(_mockUsuarioRepository.Object, _mapper,
                _mockHttpContextAccessor.Object,
                _mockAuthService.Object,
                _mockCodigoConfirmaRepository.Object, _mockLogService.Object);
            _mockUsuarioRepository.Setup(mock => mock.AddAsync(It.IsAny<Usuario>())).Returns(userEntity);

            var result = await service.Post(userRequest);

            Assert.Equal(result.Nome, userRequest.Nome);
        }

        [Fact(DisplayName = "Teste altera usuario")]
        public async Task PutTeste()
        {
            var userRequest = _fixture.Create<UsuarioRequest>();
            var userEntity = _fixture.Create<Task<Usuario>>();
            userEntity.Result.Ativo = true;
            userRequest.Nome = userEntity.Result.Nome;
            userRequest.Email = userEntity.Result.Email;
            userRequest.Senha = userEntity.Result.Senha;

            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(userEntity);
            _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<Usuario>())).Returns(userEntity);
            var service = new UsuarioService(_mockUsuarioRepository.Object, _mapper,
                _mockHttpContextAccessor.Object,
                _mockAuthService.Object,
                _mockCodigoConfirmaRepository.Object, _mockLogService.Object);

            var result = await service.PutUsuario(userRequest, userEntity.Result.Id);

            Assert.Equal(result.Nome, userEntity.Result.Nome);
        }
        [Fact(DisplayName = "Teste busca usuario")]
        public async Task GetUsuariobyId()
        {
            int id = _fixture.Create<int>();
            var userEntity = _fixture.Create<Task<Usuario>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(userEntity);
            var service = new UsuarioService(_mockUsuarioRepository.Object, _mapper,
                _mockHttpContextAccessor.Object,
                _mockAuthService.Object,
                _mockCodigoConfirmaRepository.Object, _mockLogService.Object);
            var result = await service.GetById(userEntity.Result.Id);
            Assert.Equal(result.Id, userEntity.Result.Id);
        }

        [Fact(DisplayName = "Teste buscar todos")]
        public async Task GetTodos()
        {
            var userEntitys = ListaDeUsuariosFake.UsuarioEntitiesAsync();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());

            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockUsuarioRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(userEntitys);
            var service = new UsuarioService(_mockUsuarioRepository.Object, _mapper,
                _mockHttpContextAccessor.Object,
                _mockAuthService.Object,
                _mockCodigoConfirmaRepository.Object, _mockLogService.Object);
            var result = await service.Get();
            Assert.True(result.Count() > 0);
        }

        [Fact(DisplayName = "Teste Patch Role")]
        public async Task PatchRoleTeste()
        {
            var userRequest = _fixture.Create<UsuarioRoleRequest>();
            var userEntity = _fixture.Create<Task<Usuario>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            userEntity.Result.Ativo = true;            
            _mockUsuarioRepository.Setup(mock => mock.FindAsync(userEntity.Result.Id)).Returns(userEntity);
            _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<Usuario>())).Returns(userEntity);
            var service = new UsuarioService(_mockUsuarioRepository.Object, _mapper,
               _mockHttpContextAccessor.Object,
               _mockAuthService.Object,
               _mockCodigoConfirmaRepository.Object, _mockLogService.Object);
            var result = await service.PatchRole(userEntity.Result.Id, userRequest);
            Assert.Equal(result.Role, userEntity.Result.Role);
        }

        [Fact(DisplayName = "Teste Patch Senha")]
        public async Task PatchSenhaTeste()
        {
            var userRequest = _fixture.Create<UsuarioSenhaRequest>();
            var userEntity = _fixture.Create<Task<Usuario>>();    

            var codigoConfirmacao = _fixture.Create<CodigoConfirmacao>();
            codigoConfirmacao.Ativo = true;
            var codigoConfirmacao2 = _fixture.Create<Task<CodigoConfirmacao>>();
            

            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            userEntity.Result.Ativo = true;

            _mockCodigoConfirmaRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<CodigoConfirmacao, bool>>>())).Returns(codigoConfirmacao2);
            _mockCodigoConfirmaRepository.Setup(mock => mock.EditAsync(codigoConfirmacao)).Returns(codigoConfirmacao2);

            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(userEntity);
            _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<Usuario>())).Returns(userEntity);
            userRequest.CodigoConfirmacao = codigoConfirmacao.ToString();
            var service = new UsuarioService(_mockUsuarioRepository.Object, _mapper,
               _mockHttpContextAccessor.Object,
               _mockAuthService.Object,
               _mockCodigoConfirmaRepository.Object, _mockLogService.Object);

            var result = await service.Patch(userRequest);

            Assert.NotNull(result);
        }

    }
}