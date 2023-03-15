using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts
{
    public class UsuarioSenhaRequest
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A nova senha deve ser informada.")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "O código de confirmação é obrigatório.")]
        public string CodigoConfirmacao { get; set; }
    }
}
