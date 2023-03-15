using System.ComponentModel.DataAnnotations;

namespace Loucademy.HorasRaro.Domain.Contracts.AuthContracts
{
    public class LoginResquest
    {
        [Required(ErrorMessage = "O campo 'Email' é obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo 'Senha' é obrigatorio")]
        public string Senha { get; set; }
    }
}
