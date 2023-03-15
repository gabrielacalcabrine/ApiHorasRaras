namespace Loucademy.HorasRaro.Domain.Contracts.AuthContracts
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime? DataExpiracao { get; set; }
    }
}
