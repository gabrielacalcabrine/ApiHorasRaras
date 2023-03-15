using AutoFixture;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Repository.Context;
using Loucademy.HorasRaro.Repository.Repositories;
using Loucademy.HorasRaro.Testes.Fakers;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.Repository
{
    public class ProjetoRpositoryTeste
    {
        private readonly Mock<HorasRarasApiContext> _mockContext = new Mock<HorasRarasApiContext>();
        private readonly Fixture _fixture = FixtureConfig.Get();

        [Fact(DisplayName = "Lista Projetos")]
        public async Task Get()
        {
            var entities = _fixture.Create<IEnumerable<Projeto>>();

            _mockContext.Setup(mock => mock.Set<Projeto>()).ReturnsDbSet(entities);

            var repository = new ProjetoRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca projeto Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Projeto>();

            _mockContext.Setup(mock => mock.Set<Projeto>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new ProjetoRepository(_mockContext.Object);

            var response = await repository.FindAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Projeto")]
        public async Task Post()
        {
            var entity = _fixture.Create<Projeto>();

            _mockContext.Setup(mock => mock.Set<Projeto>()).ReturnsDbSet(new List<Projeto> { new Projeto() });

            var repository = new ProjetoRepository(_mockContext.Object);

            try
            {
                await repository.AddAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Edita Projeto Existente")]
        public async Task Put()
        {
            var entity = _fixture.Create<Projeto>();

            _mockContext.Setup(mock => mock.Set<Projeto>()).ReturnsDbSet(new List<Projeto> { new Projeto() });

            var repository = new ProjetoRepository(_mockContext.Object);

            try
            {
                await repository.EditAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Deleta logicamente projeto Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Projeto>();

            _mockContext.Setup(mock => mock.Set<Projeto>()).ReturnsDbSet(new List<Projeto> { entity });

            var repository = new ProjetoRepository(_mockContext.Object);

            try
            {
                await repository.RemoveAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}
