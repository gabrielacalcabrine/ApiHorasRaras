using AutoFixture;
using Loucademy.HorasRaro.Api.Controllers;
using Loucademy.HorasRaro.Domain.Contracts;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Service;
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
    [Trait("Controller", "Controller Toggl")]
    public class TogglControllerTeste
    {
        private readonly Mock<ITogglService> _mockTogglService;
        private readonly Fixture _fixture;

        public TogglControllerTeste()
        {
            _mockTogglService = new Mock<ITogglService>();
            _fixture = new Fixture();
        }
        [Fact(DisplayName = "Buscar tarefas por meio da integração com toggl")]
        public async Task Get()
        {
            var userAgent = _fixture.Create<string>();
            var since = _fixture.Create<string>();
            var workspace = _fixture.Create<string>();
            var until = _fixture.Create<string>();
            var token = _fixture.Create<string>();
            var usuarioId = _fixture.Create<int>();
            var togglResponse = _fixture.Create<Task<IEnumerable<TooglResponse>>>();
            _mockTogglService.Setup(mock => mock.GetTarefa(userAgent, since, workspace, until, token, usuarioId)).Returns(togglResponse);
            var controller = new TogglController(_mockTogglService.Object);
            var result = await controller.GetTarefas(userAgent, since, workspace, until, token, usuarioId);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        }
    }
}
