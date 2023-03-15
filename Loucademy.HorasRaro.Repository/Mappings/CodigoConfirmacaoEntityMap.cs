using Loucademy.HorasRaro.Domain;
using Loucademy.HorasRaro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loucademy.HorasRaro.Repository.Mappings
{
    public class CodigoConfirmacaoEntityMap
    {
        public void Configure(EntityTypeBuilder<CodigoConfirmacao> builder)
        {
            builder
                .HasOne(u => u.Usuario)
                .WithMany(u => u.CodigoConfirmacao)
                .HasForeignKey(u => u.UsuarioId)
                .IsRequired();
        }

    }   
}
