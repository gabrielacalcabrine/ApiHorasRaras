using AutoFixture;
using Loucademy.HorasRaro.Api.Controllers;
using Loucademy.HorasRaro.Domain.Contracts.AuthContracts;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Testes.Fakers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.Controller
{
    [Trait("Controller", "Controller de Usuarios")]
    public class AuthControllerTestes
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Fixture _fixture;

        public AuthControllerTestes()
        {
            _mockAuthService = new Mock<IAuthService>();
            _fixture = FixtureConfig.Get();
        }
        [Fact(DisplayName = "Teste loga usuario e retorno do jwt")]
        public async Task Loga()
        {
            var login = _fixture.Create<LoginResquest>();
            var jwt = _fixture.Create<Task<LoginResponse>>();
            _mockAuthService.Setup(mock => mock.AutenticarAsync(login.Email, login.Senha)).Returns(jwt);
            var controller = new AuthController(_mockAuthService.Object);
            var result = await controller.LogaUsuario(login);
            var objectReult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectReult.StatusCode);
        }

        [Fact(DisplayName = "Confirmação email")]
        public async Task LogaUsuarioConfirmaEmail()
        {
            var token = _fixture.Create<string>();
            var request = _fixture.Create<LoginResquest>();
            _mockAuthService.Setup(mock => mock.ConfirmaEmailAsync(token));
            var controller = new AuthController(_mockAuthService.Object);
            var result = await controller.LogaUsuario(token);
            var objectReult = Assert.IsType<OkResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectReult.StatusCode);
        }

        [Fact(DisplayName = "Teste esqueci senha")]
        public async Task EsqueciSenha()
        {
            var esqueciSenha = _fixture.Create<EsqueciSenhaRequest>();
            var senha = _fixture.Create<Task<string>>();
            _mockAuthService.Setup(mock => mock.EnviaCodigoNovaSenhaAsync(esqueciSenha)).Returns(senha);
            var controller = new AuthController(_mockAuthService.Object);
            var result = await controller.EsqueciSenha(esqueciSenha);
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

    }
}
