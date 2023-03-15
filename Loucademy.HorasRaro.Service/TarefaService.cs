using AutoMapper;
using Loucademy.HorasRaro.Domain.Contracts.TarefaContracts;
using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Loucademy.HorasRaro.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;


namespace Loucademy.HorasRaro.Service
{
    public class TarefaService : BaseService, ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IProjetoUsuarioRepository _projetoUsuarioRepository;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public TarefaService(ITarefaRepository tarefaRepository, 
                             IUsuarioRepository usuarioRepository,
                             IMapper mapper, 
                             IHttpContextAccessor httpContextAccessor,
                             IProjetoRepository projetoRepository,
                             IProjetoUsuarioRepository projetoUsuarioRepository,
                             ILogService logService) : base(httpContextAccessor)

        {
            _usuarioRepository = usuarioRepository;
            _tarefaRepository = tarefaRepository;
            _mapper = mapper;
            _projetoRepository = projetoRepository;
            _projetoUsuarioRepository = projetoUsuarioRepository;
            _logService = logService;
        }

        public async Task<TarefaResponse> Post(TarefaCadastroRequest request)
        {
            var usuario = await _usuarioRepository.FindAsync(x => x.Id == request.UsuarioId && x.Ativo == true);
            var projeto = await _projetoUsuarioRepository.FindAsync(x => x.ProjetoId == request.ProjetoId);

            if (usuario is null)
            {
                throw new Exception("Usuario não existe ou está inativo");
            }
            if (projeto is null || projeto.Ativo == false)
            {
                throw new Exception("Projeto não existe ou está inativo");
            }
            if(UsuarioRole == ConstanteUtil.PerfilUsuarioColaborador && UsuarioId != request.UsuarioId)
            {
                throw new Exception("Você não pode adicionar outro colaborador a tarefa");
            }
            if (UsuarioId == request.UsuarioId || UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin)
            {
                var tarefaEntity = _mapper.Map<Tarefa>(request);
                tarefaEntity.HoraInicio = DateTime.Now;
                tarefaEntity.usuarioCriacao = UsuarioId;
                tarefaEntity.DataCriacao = DateTime.Now;
                var tarefaCadastrada = await _tarefaRepository.AddAsync(tarefaEntity);

                _logService.Log("Tarefa", (int)UsuarioId, new Tarefa() { }, tarefaCadastrada);

                return _mapper.Map<TarefaResponse>(tarefaCadastrada);
            }
            throw new Exception("Você não está logado ou não tem acesso");

        }
        public async Task<TarefaResponse> Post(TarefaCompletaRequest request)
        {
            var usuario = await _usuarioRepository.FindAsync(x => x.Id == request.UsuarioId && x.Ativo == true);
            var projeto = await _projetoRepository.FindAsync(x => x.Id == request.ProjetoId && x.Ativo == true);

            if (usuario is null)
            {
                throw new Exception("Usuario não existe");
            }
            if (projeto is null)
            {
                throw new Exception("Projeto não existe");
            }
            if (!await this.UsuarioPertenceAProjeto(usuario.Id, projeto.Id))
            {
                throw new Exception("Usuario nao pertence ao projeto");
            }

            var tarefaEntity = _mapper.Map<Tarefa>(request);
            tarefaEntity.usuarioCriacao = request.UsuarioId;
            tarefaEntity.DataCriacao = DateTime.Now;
            var tarefaCadastrada = await _tarefaRepository.AddAsync(tarefaEntity);
            _logService.Log("Tarefa", (int)UsuarioId, null, tarefaCadastrada);
            return _mapper.Map<TarefaResponse>(tarefaCadastrada);
        }
        public async Task<IEnumerable<TarefaResponse>> Get() //buscar todos só adm
        {
            if (UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin)
            {
                var lista = await _tarefaRepository.ListAsync(x => x.Ativo == true);
                return _mapper.Map<IEnumerable<TarefaResponse>>(lista);
            }

            throw new Exception("Acesso Negado");
        }

        public async Task<TarefaResponse> GetById(int id) // só usuarios que estão na tarefa ou admin
        {
            var tarefaEncontrada = await _tarefaRepository.FindAsync(x => x.Id == id);
            if (tarefaEncontrada == null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            if (UsuarioId == tarefaEncontrada.UsuarioId || UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin)
            {
                return _mapper.Map<TarefaResponse>(tarefaEncontrada);
            }
            throw new Exception("Acesso Negado");
        }

        public async Task<IEnumerable<TarefaResponse>> GetTarefaPorIdUsuario(int id)
        {
            var tarefaEncontrada = await _tarefaRepository.ListAsync(x => x.UsuarioId == id && x.Ativo);
            var tarefaCompara = tarefaEncontrada.FirstOrDefault(x => x.UsuarioId == UsuarioId);
            if (UsuarioRole == ConstanteUtil.PerfilUsuarioColaborador
                && tarefaCompara.UsuarioId == UsuarioId
                || UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin)
            {
                return _mapper.Map<IEnumerable<TarefaResponse>>(tarefaEncontrada);
            }
            if (tarefaEncontrada is null)
            {
                throw new Exception("Tarefa não encontrada");
            }
            throw new Exception("Acesso negado. Você não é administrador ou colaborador da Tarefa");

        }

        public async Task<IEnumerable<TarefaResponse>> GetTarefaByIDProjeto(int id)
        {
            var tarefaEncontrada = await _tarefaRepository.ListAsync(x => x.ProjetoId == id && x.Ativo);
            if (UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin)
            {
                return _mapper.Map<IEnumerable<TarefaResponse>>(tarefaEncontrada);
            }
            if (tarefaEncontrada is null)
            {
                throw new Exception("Tarefa não encontrada");
            }
            throw new Exception("Acesso negado. Você não é administrador ou colaborador da Tarefa");
        }
        public async Task<TarefaResponse> Put(TarefaRequest request, int? id)
        {
            var tarefaEncontrada = await _tarefaRepository.FindAsync((int)id);
            if (tarefaEncontrada == null || tarefaEncontrada.Ativo == false)
            {
                throw new Exception("Tarefa Nula ou deletada logicamente");
            }
            var dataDeSolicitadao = DateTime.Now;
            var dataCriacaoTarefa = tarefaEncontrada.DataCriacao;
            var dataSomada = dataCriacaoTarefa.AddHours(48.0);
            
            if (UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin)
            {
                var refTarefaAntigaAdmin = tarefaEncontrada;
                tarefaEncontrada.DataAlteracao = DateTime.Now;
                tarefaEncontrada.usuarioAlteracao = UsuarioId;
                await _tarefaRepository.EditAsync(tarefaEncontrada);
                
                _logService.Log("Tarefa", (int)UsuarioId, refTarefaAntigaAdmin, tarefaEncontrada);
            
                return _mapper.Map<TarefaResponse>(tarefaEncontrada);
            }
            if (UsuarioId != tarefaEncontrada.UsuarioId)
            {
                throw new Exception("Você não pode alterar essa tarefa");
            }        
            
            if (dataSomada < dataDeSolicitadao)
            {
                throw new Exception("Tarefa não pode ser alterada por colaborador depois das 48 horas");
            }

            if (request.UsuarioId != UsuarioId)
            {
                throw new Exception("Tarefa não pode colocar outro usuário na sua tarefa");
            }
            var refTarefaAntiga = tarefaEncontrada;
            tarefaEncontrada.DataAlteracao = DateTime.Now;
            tarefaEncontrada.usuarioAlteracao = UsuarioId;
            await _tarefaRepository.EditAsync(tarefaEncontrada);
            
            _logService.Log("Tarefa", (int)UsuarioId, refTarefaAntiga, tarefaEncontrada);
            
            return _mapper.Map<TarefaResponse>(tarefaEncontrada);
        }

        public async Task PatchTerminarTarefa(int id)
        {
            var tarefa = await _tarefaRepository.FindAsync(x => x.Id == id && x.Ativo == true);
            var usuarioTarefa = tarefa.UsuarioId;
            if (tarefa is null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            if (usuarioTarefa == UsuarioId || UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin)
            {
                var refTarefaAntiga = tarefa;
                tarefa.usuarioAlteracao = UsuarioId;
                tarefa.HoraFim = DateTime.Now;
                await _tarefaRepository.EditAsync(tarefa);
                _logService.Log("Tarefa", (int)UsuarioId, refTarefaAntiga, tarefa);
            }
            else
            {
                throw new Exception("Você não pode finalizar essa tarefa. Não autorizado");
            }
        }

        public async Task Delete(int id)
        {
            var tarefaEncontrada = await _tarefaRepository.FindAsync(x => x.Id == id);

            if (tarefaEncontrada is null)
            {
                throw new Exception("Tarefa não encontrada");
            }
            if (tarefaEncontrada.Ativo == false)
            {
                throw new Exception("Tarefa já foi deletada logicamente");
            }
            if (tarefaEncontrada.UsuarioId == UsuarioId || UsuarioRole == ConstanteUtil.PerfilUsuarioAdmin)
            {
                tarefaEncontrada.Ativo = false;
                tarefaEncontrada.DataAlteracao = DateTime.Now;
                tarefaEncontrada.usuarioAlteracao = UsuarioId;
                await _tarefaRepository.EditAsync(tarefaEncontrada);
            }
        }

        public async Task<bool> UsuarioPertenceAProjeto(int UsuarioId, int ProjetoId)
        {
            var relacao = await _projetoUsuarioRepository.FindAsync(x => x.UsuarioId == UsuarioId && x.ProjetoId == ProjetoId && x.Ativo);
            if (relacao is not null)
            {
                return true;
            }
            return false;
        }

        public Task<TarefaResponse> Post(TarefaRequest request) // nao apagar. Mudanca de contrato
        {
            throw new NotImplementedException();
        }
    }
}
