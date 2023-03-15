using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Service
{
    public class BaseService
    {
        public readonly int? UsuarioId;
        public readonly string UsuarioRole;
        //private readonly ILogger<BaseService> _logger;
        private IHttpContextAccessor httpContextAccessor;

        public BaseService(IHttpContextAccessor httpContextAccessor /*ILogger<BaseService> logger*/)
        {
            UsuarioId = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.NameIdentifier).ToInt();
            UsuarioRole = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Role);
            //_logger = logger;
        }
    }
}
