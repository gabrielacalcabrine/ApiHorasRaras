namespace Loucademy.HorasRaro.Domain.Entities
{
    public class CodigoConfirmacao : BaseEntity
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string Codigo { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}
