using System.ComponentModel.DataAnnotations;

namespace Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts
{
    public class UsuarioAlteraSenhaRequest
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A nova senha deve ser informada.")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "O código de confirmação é obrigatório.")]
        public int CodigoConfirmacao { get; set; }
    }
}
