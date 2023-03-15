using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Loucademy.HorasRaro.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string Contexto { get; set; }
        public int UsuarioId { get; set; }
        public string? DadosAntigos { get; set; }
        public string DadosNovos { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}

