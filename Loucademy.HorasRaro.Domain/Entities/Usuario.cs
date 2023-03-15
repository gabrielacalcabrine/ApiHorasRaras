using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public EnumRole Role { get; set; }
        public virtual ICollection<ProjetoUsuario> ProjetoUsuario { get; set; }
        public virtual ICollection<Tarefa> Tarefas { get; set; }
        public virtual ICollection<CodigoConfirmacao> CodigoConfirmacao { get; set; }
    }
}
