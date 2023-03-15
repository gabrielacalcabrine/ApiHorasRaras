using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Contracts.TarefaContracts
{
    public class TarefaRequest : TarefaCadastroRequest
    {        
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
    }
}
