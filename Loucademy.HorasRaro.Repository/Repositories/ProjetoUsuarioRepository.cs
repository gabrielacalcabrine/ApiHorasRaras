using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Repository.Repositories
{
    public class ProjetoUsuarioRepository : BaseRepository<ProjetoUsuario>, IProjetoUsuarioRepository
    {
        public ProjetoUsuarioRepository(HorasRarasApiContext context) : base(context)
        {
        }
    }
}
