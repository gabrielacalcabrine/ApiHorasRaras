namespace Loucademy.HorasRaro.Domain.Contracts.AuthContracts
{
    public class InformacaoResponse
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public List<string> Mensagens { get; set; }
        public string Detalhe { get; set; }
    }
}
