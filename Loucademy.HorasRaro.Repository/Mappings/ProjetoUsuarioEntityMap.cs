using Loucademy.HorasRaro.Domain;
using Loucademy.HorasRaro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loucademy.HorasRaro.Repository.Mappings
{
    public class ProjetoUsuarioEntityMap
    {
        public void Configure(EntityTypeBuilder<ProjetoUsuario> builder)
        {
            builder
                .HasKey(pu => new { pu.ProjetoId, pu.UsuarioId });

            builder
                .HasOne(pu => pu.Usuario)
                .WithMany(u => u.ProjetoUsuario)
                .HasForeignKey(pu => pu.UsuarioId);

            builder
                .HasOne(pu => pu.Projeto)
                .WithMany(p => p.ProjetoUsuario)
                .HasForeignKey(pu => pu.ProjetoId);
        }
    }
}
