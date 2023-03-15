using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts
{
    public class UsuarioAlteracaoRequest
    {
        [Required(ErrorMessage = "Nome do usuario é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório.")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
    }
}
