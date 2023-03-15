using System.ComponentModel.DataAnnotations;

namespace Loucademy.HorasRaro.Domain.Contracts.RelatorioContracts
{
    public class RelatorioHorasProjetoRequest
    {
        [Required(ErrorMessage = "O id do projeto é obrigatório.")]
        public int ProjetoId { get; set; }

        [Required(ErrorMessage = "Data inicial é obrigatória.")]
        [DataType(DataType.DateTime)]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Data final é obrigatória.")]
        [DataType(DataType.DateTime)]
        public DateTime DataFim { get; set; }
    }
}
