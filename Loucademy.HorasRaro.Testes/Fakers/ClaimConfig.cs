using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.Fakers
{
    public class ClaimConfig
    {
        public static IEnumerable<Claim> Get(int id, string nome, string email, string perfil)
        {
            return new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Name, nome),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, perfil)
                };
        }
    }
}
