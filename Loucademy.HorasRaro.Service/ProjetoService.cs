using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts.ProjetoContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Shared;
using Loucademy.HorasRaro.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Serilog.Core;
using System;

namespace Loucademy.HorasRaro.Service
{
    public class ProjetoService : BaseService, IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IProjetoUsuarioRepository _projetoUsuarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public ProjetoService(IHttpContextAccessor httpContextAccessor,
                              IProjetoRepository projetoRepository,
                              IProjetoUsuarioRepository projetoUsuarioRepository,
                              IUsuarioRepository usuarioRepository,
                              IMapper mapper,
                              ILogService logService) : base(httpContextAccessor)
        {
            _projetoRepository = projetoRepository;
            _projetoUsuarioRepository = projetoUsuarioRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _logService = logService;
        }

        public async Task<ProjetoResponse> Post(ProjetoRequest projetoRequest)
        {
            var requestProjetoEntity = _mapper.Map<Projeto>(projetoRequest);
            var usuarios = projetoRequest.Usuarios.UsuarioId;


            if (requestProjetoEntity is null)
            {
                throw new ArgumentException("Projeto não pode ser vazio");
            }
            if (requestProjetoEntity.DataFim < requestProjetoEntity.DataInicio)
            {
                throw new ArgumentException("Data inicial não pode ser superior a final", nameof(requestProjetoEntity.DataFim));
            }

            requestProjetoEntity.usuarioCriacao = UsuarioId;
            requestProjetoEntity.DataCriacao = DateTime.Now;
            var projetoCadastrado = await _projetoRepository.AddAsync(requestProjetoEntity);
            
            _logService.Log("Projeto", (int) UsuarioId, null, projetoCadastrado);

            foreach (var usuario in usuarios)
            {
                var usuarioExiste = await _usuarioRepository.FindAsync(x => x.Id == usuario && x.Ativo);
                if (usuarioExiste is null)
                {
                    throw new Exception($"Usuário {usuario} não está cadastrado, primeiro cadastre o usuário");
                }
                else
                {
                    var ProjetoUsuario = new ProjetoUsuario()
                    {
                        UsuarioId = usuario,
                        ProjetoId = projetoCadastrado.Id,
                        usuarioCriacao = UsuarioId,
                        DataCriacao = DateTime.Now
                    };
                    await _projetoUsuarioRepository.AddAsync(ProjetoUsuario);
                }
            }

            return _mapper.Map<ProjetoResponse>(projetoCadastrado); // retornar response de projeto usuario talvez
        }

        public async Task<ProjetoResponse> Put(ProjetoRequest projetoRequest, int? id)
        {
            var projetoCadastrado = await _projetoRepository.FindAsync(x => x.Id == id && x.Ativo);

            if (projetoCadastrado is null)
            {
                throw new ArgumentException($"Nenhum retorno foi encontrado para o id {id}");
            }
            var refProjetoAntigo = projetoCadastrado;


            projetoCadastrado.Nome = projetoRequest.Nome;
            projetoCadastrado.DataInicio = projetoRequest.DataInicio;
            projetoCadastrado.DataFim = projetoRequest.DataFim;
            projetoCadastrado.usuarioAlteracao = UsuarioId;
            projetoCadastrado.DataAlteracao = DateTime.Now;


            var projetoCadastradoRetorno = await _projetoRepository.EditAsync(projetoCadastrado);

            _logService.Log("Projeto", (int)UsuarioId, refProjetoAntigo, projetoCadastradoRetorno);

            return _mapper.Map<ProjetoResponse>(projetoCadastradoRetorno);
        }

        public async Task<IEnumerable<ProjetoResponse>> Get()
        {
            var listaProjetosCadastrados = await _projetoRepository.ListAsync(x => x.Ativo);
            if (listaProjetosCadastrados is null)
            {
                throw new Exception("Não há nenhum projeto cadastrado");
            }

            return _mapper.Map<IEnumerable<ProjetoResponse>>(listaProjetosCadastrados);
        }

        public async Task<ProjetoResponse> GetById(int id)
        {
            var projetoCadastrado = await _projetoRepository.FindAsync(x => x.Id == id && x.Ativo);
            if (projetoCadastrado is null)
            {
                throw new Exception("Projeto não existe ou está inativo");
            }

            return _mapper.Map<ProjetoResponse>(projetoCadastrado);
        }
        public async Task PatchRemoveUsuario(int IdProjeto, int IdUsuario)
        {
            var projetoUsuario = await _projetoUsuarioRepository.FindAsync(x => x.ProjetoId == IdProjeto && x.UsuarioId == IdUsuario);
            if(projetoUsuario is null)
            {
                throw new Exception("Usuário não pertence a esse projeto ou não existe");
            }
            await _projetoUsuarioRepository.RemoveAsync(projetoUsuario);
        }

        public async Task<ProjetoUsuarioRequest> PatchAdicionarUsuario(ProjetoUsuarioRequest projetoRequest, int? id) // adicionar usuario a projeto
        {

            var projetoEncontrado = await _projetoRepository.FindAsync(x => x.Id == id && x.Ativo);

            if (projetoEncontrado is null)
            {
                throw new ArgumentNullException("Projeto não existe ou está inativo");
            }
            if (projetoRequest.UsuarioId is null)
            {
                throw new ArgumentNullException("Você precisa digitar ao menos um usuario para alterar");
            }

            foreach (var usuario in projetoRequest.UsuarioId)
            {
                if (await _usuarioRepository.FindAsync(usuario) is null)
                {
                    throw new ArgumentNullException("Usuário não existe. Realize o cadastro");
                }
                var usuarioProjeto = await _projetoUsuarioRepository.FindAsync(x => x.ProjetoId == id && x.UsuarioId == usuario);
                if (usuarioProjeto is not null)
                {
                    throw new ArgumentNullException($"Usuário {usuario} já está vinculado ao projeto");
                }
                var ProjetoUsuario = new ProjetoUsuario()
                {
                    UsuarioId = usuario,
                    ProjetoId = projetoEncontrado.Id,
                    usuarioCriacao = UsuarioId,
                    DataCriacao = DateTime.Now
                };
                await _projetoUsuarioRepository.AddAsync(ProjetoUsuario);
            }
            return _mapper.Map<ProjetoUsuarioRequest>(projetoRequest);
        }

        public async Task Delete(int id)
        {
            var projetoCadastrado = await _projetoRepository.FindAsync(id);

            if (projetoCadastrado is null)
            {
                throw new ArgumentException($"Nenhum retorno foi encontrado para o id {id}");
            }

            projetoCadastrado.DataAlteracao = DateTime.Now;
            projetoCadastrado.usuarioAlteracao = UsuarioId;
            projetoCadastrado.Ativo = false;

            await _projetoRepository.EditAsync(projetoCadastrado);
        }

        public Task<ProjetoResponse> Patch(string request, int? id) // nao apagar pq vem da base
        {
            throw new NotImplementedException();
        }
    }
}