namespace Loucademy.HorasRaro.Domain.Contracts.RelatorioContracts
{
    public class RelatorioResponse
    {
        public int Id { get; set; }
        public string NomeProjeto { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFim { get; set; }
        public TimeSpan? TotalHoras { get; set; }
        public bool PodeReajuste { get; set; }
    }
}
