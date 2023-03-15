using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Repository.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Repository.Context
{
    public class HorasRarasApiContext : DbContext
    {
        public HorasRarasApiContext() { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<ProjetoUsuario> ProjetosUsuarios { get; set; }
        public DbSet<CodigoConfirmacao> CodigosConfirmacao { get; set; }
        public DbSet<Log> Log { get; set; }
        

        public HorasRarasApiContext(DbContextOptions<HorasRarasApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(new UsuarioEntityMap().Configure);
            modelBuilder.Entity<Projeto>(new ProjetoEntityMap().Configure);
            modelBuilder.Entity<CodigoConfirmacao>(new CodigoConfirmacaoEntityMap().Configure);
            modelBuilder.Entity<ProjetoUsuario>(new ProjetoUsuarioEntityMap().Configure);
            modelBuilder.Entity<Tarefa>(new TarefaEntityMap().Configure);          
            
        }



    }
}
