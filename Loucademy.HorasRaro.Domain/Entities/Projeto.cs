namespace Loucademy.HorasRaro.Domain.Entities
{
    public class Projeto : BaseEntity
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public virtual ICollection<ProjetoUsuario> ProjetoUsuario { get; set; }
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
