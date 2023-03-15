using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using System.Security.Claims;
using System.Text;
using Loucademy.HorasRaro.Domain.Contracts.AuthContracts;
using Loucademy.HorasRaro.Domain.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Loucademy.HorasRaro.Domain.Settings;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Utils;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Loucademy.HorasRaro.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICodigoConfirmacaoRepository _codigoRespository;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;
        private string _tokenJwt;
        private DateTime? _expiracao;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            AppSettings appSettings,
            ICodigoConfirmacaoRepository codigoRespository,
            IEmailService emailService)
        {
            _usuarioRepository = usuarioRepository;
            _appSettings = appSettings;
            _emailService = emailService;
            _codigoRespository = codigoRespository;
        }

        public async Task EnviaEmailConfirmacao(Usuario usuario)
        {
            await this.MontaToken(usuario);

            await _emailService.ConfiguraEnvio(usuario);

            await _emailService.DefineAssuntoEmail("Clique no link para confirmar seu email");

            await _emailService.DefineCorpoEmail("https://localhost:7162/confirma-email?token=" + _tokenJwt);

            await _emailService.EnviaEmail();
        }

        public async Task<LoginResponse> AutenticarAsync(string email, string senha)
        {
            var usuario = await this.BuscaUsuarioOrFail(email);

            if (!(Criptografia.Encrypt(senha) == usuario.Senha))
            {
                throw new Exception("Usuário ou senha incorreta");
            }

            await this.MontaToken(usuario);

            return new LoginResponse
            {
                Token = _tokenJwt,
                DataExpiracao = _expiracao
            };
        }

        public async Task<string> EnviaCodigoNovaSenhaAsync(EsqueciSenhaRequest request)
        {
            var usuario = await this.BuscaUsuarioOrFail(request.Email);

            var novoCodigo = new CodigoConfirmacao()
            {
                UsuarioId = usuario.Id,
                Codigo = CodigoUtil.GeraCodigo(),
                DataExpiracao = DateTime.Now.AddHours(2)
            };

            var entityCodigo = await _codigoRespository.AddAsync(novoCodigo);

            await _emailService.ConfiguraEnvio(usuario);

            await _emailService.DefineAssuntoEmail("Atendimento ao cliente Loucademy: Esqueci minha senha");

            await _emailService.DefineCorpoEmail(entityCodigo.Codigo);

            await _emailService.EnviaEmail();

            return "Email com código de nova senha enviado";
        }

        public async Task<string> ConfirmaEmailAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            var email = tokenS.Claims.First(claim => claim.Type == "email").Value;

            var entity = await _usuarioRepository.FindAsync(x => x.Email.Equals(email) && x.Ativo == false);

            if (entity is not null)
            {
                entity.Ativo = true;
                entity.usuarioAlteracao = entity.Id;
                entity.DataAlteracao = DateTime.Now;

                await _usuarioRepository.EditAsync(entity);
            }

            return "Email confirmado com sucesso!";
        }

        private async Task MontaToken(Usuario entity)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                    new Claim(ClaimTypes.Name, entity.Nome),
                    new Claim(ClaimTypes.Email, entity.Email),
                    new Claim(ClaimTypes.Role, entity.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.JwtSecurityKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _tokenJwt = tokenHandler.WriteToken(token);
            _expiracao = tokenDescriptor.Expires;
        }

        private async Task<Usuario> BuscaUsuarioOrFail(string email)
        {
            var usuario = await _usuarioRepository.FindAsync(x => x.Email.Equals(email) && x.Ativo);

            if (usuario is null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            return usuario;
        }
    }
}
