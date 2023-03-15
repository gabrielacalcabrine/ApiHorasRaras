using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Contracts.TarefaContracts
{
    public class TarefaCadastroRequest
    {
        [Required(ErrorMessage = "O id do Projeto é obrigatório.")]
        public int ProjetoId { get; set; }

        [Required(ErrorMessage = "O id do Colaborador é obrigatório.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Descrição da tarefa é obrigatório.")]
        public string Descricao { get; set; }
    }
}
