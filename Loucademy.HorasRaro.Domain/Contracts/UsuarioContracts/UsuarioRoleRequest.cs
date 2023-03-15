using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts
{
    public class UsuarioRoleRequest
    {
        [Required(ErrorMessage = "Role é obrigatória.")]
        public EnumRole Role { get; set; }
    }
}
