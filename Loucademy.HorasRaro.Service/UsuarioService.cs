using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts.UsuarioContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Shared;
using Loucademy.HorasRaro.Domain.Utils;
using Loucademy.HorasRaro.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Loucademy.HorasRaro.Service
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly ICodigoConfirmacaoRepository _codigoConfirmacaoRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository,
                              IMapper mapper,
                              IHttpContextAccessor httpContextAccessor,
                              IAuthService authService,
                              ICodigoConfirmacaoRepository codigoConfirmacaoRepository,
                              ILogService logService) : base(httpContextAccessor)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
            _mapper = mapper;
            _codigoConfirmacaoRepository = codigoConfirmacaoRepository;
            _logService = logService;
        }

        public async Task<UsuarioResponse> Post(UsuarioRequest request)
        {
            var usuario = await _usuarioRepository.FindAsync(x => x.Email.Equals(request.Email));
            if (usuario is not null)
            {
                throw new Exception("Este email já está em uso!");
            }
            var requestUsuarioEntity = _mapper.Map<Usuario>(request);

            requestUsuarioEntity.Senha = Criptografia.Encrypt(requestUsuarioEntity.Senha);
            requestUsuarioEntity.DataCriacao = DateTime.Now;
            requestUsuarioEntity.Role = EnumRole.Colaborador;
            requestUsuarioEntity.Ativo = false;

            var usuarioCadastrado = await _usuarioRepository.AddAsync(requestUsuarioEntity);

            _logService.Log("Usuario", usuarioCadastrado.Id, null, usuarioCadastrado);

            await _authService.EnviaEmailConfirmacao(usuarioCadastrado);

            return _mapper.Map<UsuarioResponse>(usuarioCadastrado);
        }

        public async Task<UsuarioResponse> GetById(int id)
        {
            var userBase = await _usuarioRepository.FindAsync(x => x.Ativo && x.Id == id);
            if (userBase == null)
            {
                throw new Exception("Usuário não existe ou está inativo");
            }
            if (UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin || UsuarioId == id)
            {                
                return _mapper.Map<UsuarioResponse>(userBase);
            }

            throw new Exception("Acesso Negado");
        }

        public async Task<IEnumerable<UsuarioResponse>> Get()
        {
            var lista = await _usuarioRepository.ListAsync(x => x.Ativo);
            if (UsuarioRole != ConstanteUtil.PerfilUsuarioAdmin)
            {
                throw new Exception("Você não tem acesso ou não está logado");
            }
            return _mapper.Map<IEnumerable<UsuarioResponse>>(lista);
            
        }

        public async Task<UsuarioResponse> PutUsuario(UsuarioAlteracaoRequest request, int? id)
        {
            var usuario = await _usuarioRepository.FindAsync(x => x.Id == id && x.Ativo);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado ou inativo");
            }
            if (UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin || UsuarioId == id)
            {
                var refUsuarioAntigo = usuario;

                usuario.usuarioAlteracao = UsuarioId;
                usuario.Nome = request.Nome;
                usuario.Email = request.Email;
                usuario.DataAlteracao = DateTime.Now;
                await _usuarioRepository.EditAsync(usuario);

                _logService.Log("Usuario", UsuarioId ?? usuario.Id, refUsuarioAntigo, usuario);
            }

            return _mapper.Map<UsuarioResponse>(usuario);
        }

        public async Task Delete(int id)
        {
            var usuario = await _usuarioRepository.FindAsync(id);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            if(usuario.Ativo == false)
            {
                throw new Exception("Usuario já foi deletado Logicamente");
            }
            if (UsuarioId == id || UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin)
            {
                usuario.usuarioAlteracao = UsuarioId;
                usuario.DataAlteracao = DateTime.Now;
                usuario.Ativo = false;
                await _usuarioRepository.EditAsync(usuario);
            }

            throw new Exception("Você não tem acesso a delecao desse usuario");
        }

        public async Task<UsuarioResponse> Patch(UsuarioSenhaRequest request)
        {
            var usuario = await _usuarioRepository.FindAsync(x => x.Email.Equals(request.Email) && x.Ativo);
            if (usuario is null)
            {
                throw new Exception("Usuario não encontrado");
            }
            var confirmacaoNovaSenha = await _codigoConfirmacaoRepository.FindAsync(
                 x => x.UsuarioId.Equals(usuario.Id) && 
                 x.Codigo.Equals(request.CodigoConfirmacao) &&
                 DateTime.Compare(x.DataExpiracao, DateTime.Now) < 0 &&
                 x.Ativo
                 );
            if (confirmacaoNovaSenha is null)
            {
                throw new Exception("Seu código de alteração de senha é inválido ou expirou, solicite a alteração de senha novamente!");
            }
            var refUsuarioAntigo = usuario;
 
            usuario.Senha = Criptografia.Encrypt(request.NovaSenha);
            usuario.usuarioAlteracao = usuario.Id;
            usuario.DataAlteracao = DateTime.Now;
            await _usuarioRepository.EditAsync(usuario);

            confirmacaoNovaSenha.Ativo = false;
            await _codigoConfirmacaoRepository.EditAsync(confirmacaoNovaSenha);

            _logService.Log("Usuario", UsuarioId ?? usuario.Id, refUsuarioAntigo, usuario);

            return _mapper.Map<UsuarioResponse>(usuario);
        }

        public async Task<UsuarioResponse> PatchRole(int id, UsuarioRoleRequest usuarioRole)
        {
            var usuario = await _usuarioRepository.FindAsync(id);
            if (usuario == null || usuario.Ativo == false)
            {
                throw new Exception("Usuario não encontrado");
            }
            if (UsuarioRole != ConstanteUtil.PerfilUsuarioAdmin)
            {
                throw new Exception("Você não pode mudar a Role desse usuário");
            }
            var refUsuarioAntigo = usuario;

            usuario.Role = usuarioRole.Role;
            usuario.usuarioAlteracao = UsuarioId;
            usuario.DataAlteracao = DateTime.Now;

            await _usuarioRepository.EditAsync(usuario);

            _logService.Log("Usuario", UsuarioId ?? usuario.Id, refUsuarioAntigo, usuario);

            return _mapper.Map<UsuarioResponse>(usuario);
        }

        public Task<UsuarioResponse> Put(UsuarioAlteracaoRequest request) // nao apagar
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioResponse> Put(UsuarioRequest request, int? id) // nao apagar
        {
            throw new NotImplementedException();
        }
    }
}
