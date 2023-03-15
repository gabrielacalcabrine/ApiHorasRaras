using AutoFixture;
using Loucademy.HorasRaro.Api.Controllers;
using Loucademy.HorasRaro.Domain.Contracts.RelatorioContracts;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
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
    [Trait("Controller", "Controller de Relatórios")]
    public class RelatoriosControllerTestes
    {
        private readonly Mock<IRelatorioService> _mockRelatorioService;
        private readonly Fixture _fixture;

        public RelatoriosControllerTestes()
        {
            _mockRelatorioService = new Mock<IRelatorioService>();
            _fixture = new Fixture();
        }
        [Fact(DisplayName = "Buscar relatorios do dia")]
        public async Task GetAtividadesDoDia()
        {
            var atividades = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            _mockRelatorioService.Setup(mock => mock.MeuRelatorioHoje()).Returns(atividades);
            var controller = new RelatorioController(_mockRelatorioService.Object);
            var result = await controller.MeuRelatorioHoje();
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [Fact(DisplayName = "Buscar relatorios da semana")]
        public async Task GetAtividadesDoMes()
        {
            var atividades = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            _mockRelatorioService.Setup(mock => mock.MeuRelatorioMes()).Returns(atividades);
            var controller = new RelatorioController(_mockRelatorioService.Object);
            var result = await controller.MeuRelatorioMes();
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [Fact(DisplayName = "Buscar relatorios da semana")]
        public async Task GetAtividadesDaSemana()
        {
            var atividades = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            _mockRelatorioService.Setup(mock => mock.MeuRelatorioSemana()).Returns(atividades);
            var controller = new RelatorioController(_mockRelatorioService.Object);
            var result = await controller.MeuRelatorioSemana();
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        [Fact(DisplayName = "Buscar relatorios do Dia- Administrador")]
        public async Task GetAtividadesDoDiaAdm()
        {
            var idUsuario = _fixture.Create<int>();
            var idProjeto = _fixture.Create<int>();
            var atividades = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            _mockRelatorioService.Setup(mock => mock.RelatorioAdminHoje(idUsuario, idProjeto)).Returns(atividades);
            var controller = new RelatorioController(_mockRelatorioService.Object);
            var result = await controller.RelatorioAdminHoje(idUsuario, idProjeto);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Buscar relatorios da Semana - Administrador")]
        public async Task GetAtividadesDaSemanaAdm()
        {
            var idUsuario = _fixture.Create<int>();
            var idProjeto = _fixture.Create<int>();
            var atividades = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            _mockRelatorioService.Setup(mock => mock.RelatorioAdminSemana(idUsuario, idProjeto)).Returns(atividades);
            var controller = new RelatorioController(_mockRelatorioService.Object);
            var result = await controller.RelatorioAdminSemana(idUsuario, idProjeto);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Buscar relatorios da Semana - Administrador")]
        public async Task GetAtividadesDoMesAdm()
        {
            var idUsuario = _fixture.Create<int>();
            var idProjeto = _fixture.Create<int>();
            var atividades = _fixture.Create<Task<IEnumerable<RelatorioResponse>>>();
            _mockRelatorioService.Setup(mock => mock.RelatorioAdminMes(idUsuario, idProjeto)).Returns(atividades);
            var controller = new RelatorioController(_mockRelatorioService.Object);
            var result = await controller.RelatorioAdminMes(idUsuario, idProjeto);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }
    }
}
