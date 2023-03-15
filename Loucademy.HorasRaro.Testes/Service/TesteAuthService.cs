using AutoFixture;
using Loucademy.HorasRaro.Domain.Contracts.AuthContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Settings;
using Loucademy.HorasRaro.Service;
using Loucademy.HorasRaro.Testes.Fakers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.Service
{
    public class AuthServiceTeste
    {
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        private readonly Fixture _fixture = FixtureConfig.Get();
        private readonly Mock<IEmailService> _mockEmailService = new Mock<IEmailService>();
        private readonly Mock<AppSettings> _mockAppSetting = new Mock<AppSettings>();
        private readonly Mock<ICodigoConfirmacaoRepository> _mockCodigoConfirmaRepository = new Mock<ICodigoConfirmacaoRepository>();

        [Fact(DisplayName = "Teste envia email usuario")]
        public async Task EnviaEmail()
        {           
            var usuario = _fixture.Create<Usuario>();            
            var service = new AuthService(_mockUsuarioRepository.Object,
                                          _mockAppSetting.Object,
                                          _mockCodigoConfirmaRepository.Object,
                                          _mockEmailService.Object);
            var result = service.EnviaEmailConfirmacao(usuario);
            Assert.NotNull(result);
        }
        [Fact(DisplayName = "Teste autenticar")]
        public async Task Autentica()
        {
            var login = _fixture.Create<LoginResquest>();
            var jwt = _fixture.Create<Task<LoginResponse>>();
            var usuario = _fixture.Create<Task<Usuario>>();
            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(usuario);
            var service = new AuthService(_mockUsuarioRepository.Object,
                                          _mockAppSetting.Object,
                                          _mockCodigoConfirmaRepository.Object,
                                          _mockEmailService.Object);
            var result = service.ConfirmaEmailAsync(jwt.Result.Token);
            Assert.NotNull(result);
        }        
    }
}
