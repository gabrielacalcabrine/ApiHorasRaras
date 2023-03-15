
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Loucademy.HorasRaro.Domain.Contracts.ProjetoContracts
{
    public class ProjetoRequest
    {
        [Required(ErrorMessage = "Nome do projeto é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Data inicio é obrigatória.")]
        [DataType(DataType.DateTime)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataFim { get; set; }
        public ProjetoUsuarioRequest Usuarios { get; set; }
    }
}
