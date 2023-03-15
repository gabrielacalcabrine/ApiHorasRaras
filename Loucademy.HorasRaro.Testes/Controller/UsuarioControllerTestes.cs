using AutoFixture;
using AutoMapper;
using Bogus;
using Castle.Components.DictionaryAdapter;
using Loucademy.HorasRaro.Api.Controllers;
using Loucademy.HorasRaro.CrossCutting.DependencyInjector;
using Loucademy.HorasRaro.CrossCutting.Mappers;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Testes.Fakers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;

namespace Loucademy.HorasRaro.Testes.Controller
{
    [Trait("Controller", "Controller de Usuarios")]
    public class UsuarioControllerTestes
    {
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly Fixture _fixture;

        public UsuarioControllerTestes()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Cadastra um novo usuario")]
        public async Task Post()
        {
            var userRequest = _fixture.Create<UsuarioRequest>();
            var userResult = _fixture.Create<Task<UsuarioResponse>>();

            _mockUsuarioService.Setup(mock => mock.Post(userRequest)).Returns(userResult);
            var controller = new UsuarioController(_mockUsuarioService.Object);
            var result = await controller.Post(userRequest);
            var objectReult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectReult.StatusCode);

        }

        [Fact(DisplayName = "Alteração usuário")]
        public async Task Put()
        {
            var userRequest = _fixture.Create<UsuarioAlteracaoRequest>();
            var userResult = _fixture.Create<Task<UsuarioResponse>>();
            _mockUsuarioService.Setup(mock => mock.PutUsuario(userRequest, userResult.Result.Id)).Returns(userResult);
            var controller = new UsuarioController(_mockUsuarioService.Object);
            var result = await controller.Put(userRequest, userResult.Result.Id);
            var objectReult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(StatusCodes.Status200OK, objectReult.StatusCode);

        }
        [Fact(DisplayName = "Busca um usuario por ID")]
        public async Task GetById()
        {
            var userResponse = _fixture.Create<Task<UsuarioResponse>>();
            var id = userResponse.Id;
            _mockUsuarioService.Setup(mock => mock.GetById(id)).Returns(userResponse);
            var controller = new UsuarioController(_mockUsuarioService.Object);
            var result = await controller.GetById(id);
            var objectReult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(StatusCodes.Status200OK, objectReult.StatusCode);
        }

        [Fact(DisplayName = "Buscar todos os Usuários")]
        public async Task Get()
        {
            var people = _fixture.Create<Task<IEnumerable<UsuarioResponse>>>();

            _mockUsuarioService.Setup(mock => mock.Get()).Returns(people);
            var controller = new UsuarioController(_mockUsuarioService.Object);
            var result = await controller.Get();
            var objectReult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectReult.StatusCode);
        }

        [Fact(DisplayName = "Busca usuários para edição de Role")]
        public async Task Patch()
        {
            var userRequest = _fixture.Create<UsuarioRoleRequest>();
            var userResult = _fixture.Create<Task<UsuarioResponse>>();
            _mockUsuarioService.Setup(mock => mock.PatchRole(userResult.Id, userRequest)).Returns(userResult);
            var controller = new UsuarioController(_mockUsuarioService.Object);
            var result = await controller.Patch(userResult.Result.Id, userRequest);
            var objectReult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectReult.StatusCode);
        }

        [Fact(DisplayName = "Busca usuários para edição de Senha")]
        public async Task PatchSenha()
        {
            var userRequest = _fixture.Create<UsuarioSenhaRequest>();
            var userResult = _fixture.Create<Task<UsuarioResponse>>();
            _mockUsuarioService.Setup(mock => mock.Patch(userRequest)).Returns(userResult);
            var controller = new UsuarioController(_mockUsuarioService.Object);
            var result = await controller.PatchSenha(userRequest);
            var objectReult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectReult.StatusCode);
        }

        [Fact(DisplayName = "Exclusão de usuários")]
        public async Task RemoveUsuario()
        {
            int id = 1;
            _mockUsuarioService.Setup(mock => mock.Delete(id)).Returns(Task.CompletedTask);
            var controller = new UsuarioController(_mockUsuarioService.Object);
            var result = await controller.Delete(id);
            var objectReult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, objectReult.StatusCode);

        }
    }
}
