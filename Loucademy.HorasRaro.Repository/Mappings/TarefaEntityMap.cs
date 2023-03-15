using Loucademy.HorasRaro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Loucademy.HorasRaro.Repository.Mappings
{
    public class TarefaEntityMap
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder
                .HasOne(p => p.Usuario)
                .WithMany(p => p.Tarefas)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(p => p.Projeto)
                .WithMany(p => p.Tarefas)
                .HasForeignKey(p => p.ProjetoId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
