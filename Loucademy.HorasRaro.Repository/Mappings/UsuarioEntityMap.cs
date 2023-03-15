using Loucademy.HorasRaro.Domain;
using Loucademy.HorasRaro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loucademy.HorasRaro.Repository.Mappings
{
    public class UsuarioEntityMap
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .Property(prop => prop.Role)
                .HasConversion(
                    prop => prop.ToString(),
            prop => (EnumRole)Enum.Parse(typeof(EnumRole), prop)
            );

            builder.HasIndex(prop => prop.Email).IsUnique();
        }

    }
}
