using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Entities
{
    public class Tarefa : BaseEntity
    {
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }        
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFim { get; set; }
        public string Descricao { get; set; }

    }
}
