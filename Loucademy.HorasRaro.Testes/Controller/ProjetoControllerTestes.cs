using AutoFixture;
using Loucademy.HorasRaro.Api.Controllers;
using Loucademy.HorasRaro.Domain.Contracts.ProjetoContracts;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Testes.Fakers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Moq;
using NSubstitute;
using Org.BouncyCastle.Crypto.Modes.Gcm;

namespace Loucademy.HorasRaro.Testes.Controller
{
    [Trait("Controller", "Controller de Projetos")]
    public class ProjetoControllerTestes
    {
        private readonly Mock<IProjetoService> _mockProjetoService;
        private readonly Fixture _fixture;
        private readonly Mock<IProjetoUsuarioRepository> _mockProjetoUsuarioRepository;

        public ProjetoControllerTestes()
        {
            _mockProjetoService = new Mock<IProjetoService>();
            _mockProjetoUsuarioRepository = new Mock<IProjetoUsuarioRepository>();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Cadastra um novo projeto e retorna true")]
        public async Task Post()
        {
            var projetoRequest = _fixture.Create<ProjetoRequest>();
            var projetoResponse = _fixture.Create<Task<ProjetoResponse>>();
            _mockProjetoService.Setup(x => x.Post(projetoRequest)).Returns(projetoResponse);
            var controller = new ProjetosController(_mockProjetoService.Object);
            var result = await controller.PostAsync(projetoRequest);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        }

        [Fact(DisplayName = "Atualiza um projeto existente a partir do id")]
        public async Task Put()
        {
            var projetoRequest = _fixture.Create<ProjetoRequest>();
            var projetoResponse = _fixture.Create<Task<ProjetoResponse>>();
            _mockProjetoService.Setup(x => x.Put(projetoRequest, projetoResponse.Result.Id)).Returns(projetoResponse);
            var controller = new ProjetosController(_mockProjetoService.Object);
            var result = await controller.Put(projetoResponse.Result.Id, projetoRequest);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Busca projeto por Id")]
        public async Task GetById()
        {
            var projetoResponse = _fixture.Create<Task<ProjetoResponse>>();
            var id = projetoResponse.Id;
            _mockProjetoService.Setup(mock => mock.GetById(id)).Returns(projetoResponse);
            var controller = new ProjetosController(_mockProjetoService.Object);
            var result = await controller.GetById(id);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Busca todos os usuários")]
        public async Task Get()
        {
            var projetos = _fixture.Create<Task<IEnumerable<ProjetoResponse>>>();
            _mockProjetoService.Setup(mock => mock.Get()).Returns(projetos);
            var controller = new ProjetosController(_mockProjetoService.Object);
            var result = await controller.GetAsync();
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Exclui usuario de projeto")]
        public async Task DeleteUsuarioProjeto()
        {
            int idProj = 1;
            int idUser = 1;
            _mockProjetoService.Setup(mock => mock.PatchRemoveUsuario(idProj, idUser)).Returns(Task.CompletedTask);
            var controller = new ProjetosController(_mockProjetoService.Object);
            var result = await controller.PatchRemove(idProj, idUser);
            var objectResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
        [Fact(DisplayName = "Adiciona Usuario a projeto")]
        public async Task PacthAdicionarUsuario()
        {
            int id = 1;
            var projetoUsuarioRequest = _fixture.Create<ProjetoUsuarioRequest>();
            var projetoUsuarioRequestReturn = _fixture.Create<Task<ProjetoUsuarioRequest>>();
            var projetoUsuarioEntity = _fixture.Create<ProjetoUsuario>();

            _mockProjetoService.Setup(mock => mock.PatchAdicionarUsuario(projetoUsuarioRequest, projetoUsuarioEntity.ProjetoId))
            .Returns(projetoUsuarioRequestReturn);

            var controller = new ProjetosController(_mockProjetoService.Object);
            var result = await controller.Patch(id, projetoUsuarioRequest);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);


        }
        [Fact(DisplayName = "Teste delete projeto")]
        public async Task DeleteTeste()
        {
            int id = 1;
            _mockProjetoService.Setup(mock => mock.Delete(id)).Returns(Task.CompletedTask);
            var controller = new ProjetosController(_mockProjetoService.Object);
            var result = await controller.Delete(id);
            var objectResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

    }
}