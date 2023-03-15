namespace Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public EnumRole Role { get; set; }
        public bool Ativo { get; set; }
    }
}
