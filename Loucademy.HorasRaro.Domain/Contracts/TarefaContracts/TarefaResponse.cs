using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Contracts.TarefaContracts
{
    public class TarefaResponse 
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public int UsuarioId { get; set; }    
        public string Descricao { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
    }
}
