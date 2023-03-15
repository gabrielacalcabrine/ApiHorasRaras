using AutoFixture;
using AutoMapper;
using Bogus;
using Loucademy.HorasRaro.Domain;
using Loucademy.HorasRaro.Domain.Contracts.ProjetoContracts;
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
using System.Linq.Expressions;


namespace Loucademy.HorasRaro.Testes.Service
{
    public class ProjetoServiceTestes
    {
        private readonly Mock<IProjetoRepository> _mockProjetoRepository = new Mock<IProjetoRepository>();
        private readonly Mock<IProjetoUsuarioRepository> _mockProjetoUsuarioRepository = new Mock<IProjetoUsuarioRepository>();
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        private readonly Faker _faker = new Faker();
        public IMapper _mapper = new AutoMapperFixture().GetMapper();
        private readonly Fixture _fixture = FixtureConfig.Get();
        private readonly Mock<ILogService> _mockLogService = new Mock<ILogService>();

        [Fact(DisplayName = "Cadastra novo projeto")]
        public async Task Post()
        {
            var usuarioProjetoTask = _fixture.Create<Task<ProjetoUsuario>>();

            var projetoRequest = _fixture.Create<ProjetoRequest>();

            projetoRequest.DataInicio = DateTime.Now;
            projetoRequest.DataFim = DateTime.Now.AddDays(7);

            var projetoUsuarioRequest = _fixture.Create<ProjetoUsuarioRequest>();
            var usuarioList = projetoUsuarioRequest.UsuarioId;

            var usuarioProjeto = _fixture.Create<ProjetoUsuario>();

            var usuarioEntity = _fixture.Create<Task<Usuario>>();

            var projeto = _fixture.Create<Task<Projeto>>();
            projetoRequest.Nome = projeto.Result.Nome;


            _mockProjetoRepository.Setup(mock => mock.AddAsync(It.IsAny<Projeto>())).Returns(projeto);
            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(usuarioEntity);
            _mockProjetoUsuarioRepository.Setup(mock => mock.AddAsync(It.IsAny<ProjetoUsuario>())).Returns(usuarioProjetoTask);
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                             _mockProjetoRepository.Object,
                                             _mockProjetoUsuarioRepository.Object,
                                             _mockUsuarioRepository.Object,
                                             _mapper,
                                             _mockLogService.Object);


            var result = await service.Post(projetoRequest);
            Assert.Equal(result.Nome, projetoRequest.Nome);

        }

        [Fact(DisplayName = "Busca todos os projetos")]
        public async Task Get()
        {
            var projetos = _fixture.Create<Task<IEnumerable<Projeto>>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                            _mockProjetoRepository.Object,
                                            _mockProjetoUsuarioRepository.Object,
                                            _mockUsuarioRepository.Object,
                                            _mapper,
                                            _mockLogService.Object);
            _mockProjetoRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Projeto, bool>>>())).Returns(projetos);
            var result = await service.Get();
            Assert.True(result.Count() > 0);
        }

        [Fact(DisplayName = "Busca de projeto por id")]
        public async Task GetById()
        {
            int id = _fixture.Create<int>();
            var ProjetoEntity = _fixture.Create<Task<Projeto>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Projeto, bool>>>())).Returns(ProjetoEntity);
            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                            _mockProjetoRepository.Object,
                                            _mockProjetoUsuarioRepository.Object,
                                            _mockUsuarioRepository.Object,
                                            _mapper,
                                            _mockLogService.Object);

            var result = await service.GetById(id);
            Assert.Equal(result.Nome, ProjetoEntity.Result.Nome);
        }

        [Fact(DisplayName = "Busca por projeto inválido")]
        public async Task GetByIdProjetoNaoEncontrado()
        {
            int id = _fixture.Create<int>();
            var ProjetoEntity = _fixture.Create<Task<Projeto>>();
            ProjetoEntity.Result.Ativo = false;
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Projeto, bool>>>()));
            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                            _mockProjetoRepository.Object,
                                            _mockProjetoUsuarioRepository.Object,
                                            _mockUsuarioRepository.Object,
                                            _mapper,
                                            _mockLogService.Object);

            var exception = await Record.ExceptionAsync(async () => await service.GetById(ProjetoEntity.Result.Id));

            Assert.NotNull(exception);
        }        

        [Fact(DisplayName = "Pacth remove usuario ")]
        public async Task Pacth()
        {
            var usuario = _fixture.Create<Usuario>();
            var projeto = _fixture.Create<Projeto>();
            var projetoUsuario = _fixture.Create<ProjetoUsuario>();

            _mockProjetoUsuarioRepository.Setup(mock => mock.RemoveAsync((It.IsAny<ProjetoUsuario>()))).Returns(Task.CompletedTask);
            _mockProjetoUsuarioRepository.Setup(mock => mock.FindAsync((It.IsAny<Expression<Func<ProjetoUsuario, bool>>>()))).ReturnsAsync(projetoUsuario);
            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                            _mockProjetoRepository.Object,
                                            _mockProjetoUsuarioRepository.Object,
                                            _mockUsuarioRepository.Object,
                                            _mapper,
                                            _mockLogService.Object);
            var exception = await Record.ExceptionAsync(async () => await service.PatchRemoveUsuario(usuario.Id, projeto.Id));

            Assert.Null(exception);
        }

        [Fact(DisplayName = "Pacth remove usuario pega exception")]
        public async Task PacthException()
        {
            var usuario = _fixture.Create<Usuario>();
            var projeto = _fixture.Create<Projeto>();
            var projetoUsuario = _fixture.Create<ProjetoUsuario>();

            _mockProjetoUsuarioRepository.Setup(mock => mock.RemoveAsync((It.IsAny<ProjetoUsuario>()))).Returns(Task.CompletedTask);
            _mockProjetoUsuarioRepository.Setup(mock => mock.FindAsync((It.IsAny<Expression<Func<ProjetoUsuario, bool>>>())));
            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                            _mockProjetoRepository.Object,
                                            _mockProjetoUsuarioRepository.Object,
                                            _mockUsuarioRepository.Object,
                                            _mapper,
                                            _mockLogService.Object);
            var exception = await Record.ExceptionAsync(async () => await service.PatchRemoveUsuario(usuario.Id, projeto.Id));

            Assert.NotNull(exception);
        }

        [Fact(DisplayName = "Altera projeto")]
        public async Task Put()
        {          

            var projetoRequest = _fixture.Create<ProjetoRequest>();

            projetoRequest.DataInicio = DateTime.Now;
            projetoRequest.DataFim = DateTime.Now.AddDays(7);            
          

            var projeto = _fixture.Create<Task<Projeto>>();
            projetoRequest.Nome = projeto.Result.Nome;


            _mockProjetoRepository.Setup(mock => mock.EditAsync(It.IsAny<Projeto>())).Returns(projeto);
            _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Projeto, bool>>>())).Returns(projeto);

            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                             _mockProjetoRepository.Object,
                                             _mockProjetoUsuarioRepository.Object,
                                             _mockUsuarioRepository.Object,
                                             _mapper,
                                             _mockLogService.Object);

            var result = await service.Put(projetoRequest, projeto.Result.Id);
            Assert.Equal(result.Nome, projetoRequest.Nome);

        }

        [Fact(DisplayName = "Altera projeto pegando exceção")]
        public async Task PutException()
        {
            var projetoRequest = _fixture.Create<ProjetoRequest>();

            projetoRequest.DataInicio = DateTime.Now;
            projetoRequest.DataFim = DateTime.Now.AddDays(7);     
            var projeto = _fixture.Create<Projeto>();             

            _mockProjetoRepository.Setup(mock => mock.EditAsync(It.IsAny<Projeto>()));
            _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Projeto, bool>>>())).ReturnsAsync((Projeto)null);

            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                             _mockProjetoRepository.Object,
                                             _mockProjetoUsuarioRepository.Object,
                                             _mockUsuarioRepository.Object,
                                             _mapper,
                                             _mockLogService.Object);         
            
            var exception = await Record.ExceptionAsync(async () => await service.Put(projetoRequest, projeto.Id));

            Assert.NotNull(exception);

        }

        [Fact(DisplayName = "Adiciona Usuario com exceção projeto nulo")]
        public async Task PacthAdicionaUsuario()
        {
            var projetoRequest = _fixture.Create<ProjetoRequest>();
            var projeto = _fixture.Create<Projeto>();
            var usuario = _fixture.Create<Usuario>();

            _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Projeto, bool>>>())).ReturnsAsync((Projeto)null);
            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).ReturnsAsync(usuario);
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                             _mockProjetoRepository.Object,
                                             _mockProjetoUsuarioRepository.Object,
                                             _mockUsuarioRepository.Object,
                                             _mapper,
                                             _mockLogService.Object);
            var exception = await Record.ExceptionAsync(async () => await service.PatchAdicionarUsuario(projetoRequest.Usuarios, projeto.Id));

            Assert.NotNull(exception);

        }

        [Fact(DisplayName = "Adiciona Usuario com exceção usuario nulo")]
        public async Task PacthAdicionaUsuariocomUsuarioNulo()
        {
            var projetoRequest = _fixture.Create<ProjetoRequest>();
            var projeto = _fixture.Create<Projeto>();
            var usuario = _fixture.Create<Usuario>();

            _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Projeto, bool>>>())).ReturnsAsync(projeto);
            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).ReturnsAsync((Usuario)null);
            _mockProjetoUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoUsuario, bool>>>()));
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                             _mockProjetoRepository.Object,
                                             _mockProjetoUsuarioRepository.Object,
                                             _mockUsuarioRepository.Object,
                                             _mapper,
                                             _mockLogService.Object);
            var exception = await Record.ExceptionAsync(async () => await service.PatchAdicionarUsuario(projetoRequest.Usuarios, projeto.Id));

            Assert.NotNull(exception);

        }

        [Fact(DisplayName = "Teste para pegar a exceção projeto Usuario Nulo")]
        public async Task PacthAdicionaUsuarioProjetoUsuarioNulo()
        {
            var projetoRequest = _fixture.Create<ProjetoRequest>();
            var projeto = _fixture.Create<Projeto>();
            var usuario = _fixture.Create<Usuario>();

            _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Projeto, bool>>>())).ReturnsAsync(projeto);
            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).ReturnsAsync(usuario);
            _mockProjetoUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoUsuario, bool>>>())).ReturnsAsync((ProjetoUsuario)null);
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                             _mockProjetoRepository.Object,
                                             _mockProjetoUsuarioRepository.Object,
                                             _mockUsuarioRepository.Object,
                                             _mapper,
                                             _mockLogService.Object);
            var exception = await Record.ExceptionAsync(async () => await service.PatchAdicionarUsuario(projetoRequest.Usuarios, projeto.Id));

            Assert.NotNull(exception);

        }

        [Fact(DisplayName = "Teste para pegar a exceção Usuario já vinculado ao projeto")]
        public async Task PacthAdicionaUsuarioProjetoUsuarioJavinculado()
        {
            var projetoRequest = _fixture.Create<ProjetoRequest>();
            var projeto = _fixture.Create<Projeto>();
            var usuario = _fixture.Create<Usuario>();
            var projetoUsuario = _fixture.Create<ProjetoUsuario>();
            projetoRequest.Usuarios.UsuarioId[0] = usuario.Id;
            projetoUsuario.UsuarioId = usuario.Id;
            _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Projeto, bool>>>())).ReturnsAsync(projeto);
            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).ReturnsAsync(usuario);
            _mockProjetoUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoUsuario, bool>>>())).ReturnsAsync(projetoUsuario);
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, EnumRole.Administrador.ToString());
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
            var service = new ProjetoService(_mockHttpContextAccessor.Object,
                                             _mockProjetoRepository.Object,
                                             _mockProjetoUsuarioRepository.Object,
                                             _mockUsuarioRepository.Object,
                                             _mapper,
                                             _mockLogService.Object);
            var exception = await Record.ExceptionAsync(async () => await service.PatchAdicionarUsuario(projetoRequest.Usuarios, projeto.Id));

            Assert.NotNull(exception);

        }
       

        }



    }




