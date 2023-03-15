using System.ComponentModel.DataAnnotations;

namespace Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts
{
    public class UsuarioRequest : UsuarioAlteracaoRequest
    {
        [Required(ErrorMessage = "Senha do usuario é obrigatória.")]
        [StringLength(18, MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
