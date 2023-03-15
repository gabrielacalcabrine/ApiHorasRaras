namespace Loucademy.HorasRaro.Domain.Entities
{
    public class ProjetoUsuario
    {
        public ProjetoUsuario()
        {
            Ativo = true;
        }

        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int? usuarioAlteracao { get; set; }
        public int? usuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
