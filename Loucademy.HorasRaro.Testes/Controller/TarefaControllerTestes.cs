using AutoFixture;
using Loucademy.HorasRaro.Api.Controllers;
using Loucademy.HorasRaro.Domain.Contracts.TarefaContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Service;
using Loucademy.HorasRaro.Testes.Fakers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.Controller
{
    [Trait("Controller", "Tarefa")]
    public class TarefaControllerTestes
    {
        private readonly Mock<ITarefaService> _mockTarefaService;
        private readonly Fixture _fixture;

        public TarefaControllerTestes()
        {
            _fixture = FixtureConfig.Get();
            _mockTarefaService = new Mock<ITarefaService>();
        }


        [Fact(DisplayName = "Cadastra um nova Tarefa")]
        public async Task Post()
        {
            var tarefaRequeste = _fixture.Create<TarefaCadastroRequest>();
            var tarefaResponse = _fixture.Create<Task<TarefaResponse>>();
            _mockTarefaService.Setup(mock => mock.Post(tarefaRequeste)).Returns(tarefaResponse);
            var controller = new TarefaController(_mockTarefaService.Object);
            var result = await controller.Post(tarefaRequeste);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Modifica tarefa")]
        public async Task Put()
        {
            var tarefaRequeste = _fixture.Create<TarefaRequest>();
            var tarefaResponse = _fixture.Create<Task<TarefaResponse>>();
            _mockTarefaService.Setup(mock => mock.Put(tarefaRequeste, tarefaResponse.Id)).Returns(tarefaResponse);
            var controller = new TarefaController(_mockTarefaService.Object);
            var result = await controller.Put(tarefaRequeste, tarefaResponse.Id);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Busca tarefa")]
        public async Task GetById()
        {
            var tarefaResponse = _fixture.Create<Task<TarefaResponse>>();
            _mockTarefaService.Setup(mock => mock.GetById(tarefaResponse.Id)).Returns(tarefaResponse);
            var controller = new TarefaController(_mockTarefaService.Object);
            var result = await controller.Get(tarefaResponse.Id);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Busca tarefa")]
        public async Task Get()
        {
            var tarefaResponse = _fixture.Create<Task<IEnumerable<TarefaResponse>>>();
            _mockTarefaService.Setup(mock => mock.Get()).Returns(tarefaResponse);
            var controller = new TarefaController(_mockTarefaService.Object);
            var result = await controller.Get();
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Busca tarefa")]
        public async Task GetTarefaPorUsuario()
        {
            var usuario = _fixture.Create<Usuario>();
            var listaDeTarefas = _fixture.Create<Task<IEnumerable<TarefaResponse>>>();            
            _mockTarefaService.Setup(mock => mock.GetTarefaPorIdUsuario(usuario.Id)).Returns(listaDeTarefas);
            var controller = new TarefaController(_mockTarefaService.Object);            
            var result = await controller.GetTarefaIdUsuario(usuario.Id);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);            
        }

        [Fact(DisplayName = "Busca tarefa")]
        public async Task GetTarefaPorProjeto()
        {
            var projeto = _fixture.Create<Projeto>();
            var listaDeTarefas = _fixture.Create<Task<IEnumerable<TarefaResponse>>>();            
            _mockTarefaService.Setup(mock => mock.GetTarefaByIDProjeto(projeto.Id)).Returns(listaDeTarefas);
            var controller = new TarefaController(_mockTarefaService.Object);
            var result = await controller.GetTarefaIdProjeto(projeto.Id);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Teste pacth tarefa")]
        public async Task PatchEncerrarTarefa()
        {
            var tarefa = _fixture.Create<Tarefa>();
            _mockTarefaService.Setup(mock => mock.PatchTerminarTarefa(tarefa.Id)).Returns(Task.CompletedTask);
            var controller = new TarefaController(_mockTarefaService.Object);
            var result = await controller.TerminaTarefa(tarefa.Id);
            var objectResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Teste pacth tarefa")]
        public async Task Delete()
        {
            var tarefa = _fixture.Create<TarefaResponse>();
            _mockTarefaService.Setup(mock => mock.Delete(tarefa.Id)).Returns(Task.CompletedTask);
            var controller = new TarefaController(_mockTarefaService.Object);
            var result = await controller.Delete(tarefa.Id); 
            var objectResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }



    }
}
