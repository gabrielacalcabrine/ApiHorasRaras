using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Utils
{
    public static class ConstanteUtil
    {
        public const string PerfilUsuarioAdmin = "Administrador";
        public const string PerfilUsuarioColaborador = "Colaborador";
        public const string PerfilLogadoNome = $"{PerfilUsuarioAdmin}, {PerfilUsuarioColaborador}";

    }
}
