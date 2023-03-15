using Loucademy.HorasRaro.Domain.Entities;
using Loucademy.HorasRaro.Domain.Interfaces.Repositorys;
using Loucademy.HorasRaro.Domain.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Repository.Repositories
{
    public class TogglRepository : ITogglRepository
    {
        private readonly HttpClient _httpClient;

        public TogglRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Datum>> GetTarefa(string? userAgent, string? since, string? workspace, string? until, string? token)
        {
            var Url = ($"details?&user_agent={userAgent}&workspace_id={workspace}&since={since}&until={until}");
            var autorizacaoBase64 = Convert.ToBase64String(Encoding.Default.GetBytes($"{token}:api_token"));
            var autenticacaoToken = new AuthenticationHeaderValue("Basic", $"{autorizacaoBase64}");
            var request = new HttpRequestMessage(HttpMethod.Get, Url);
            request.Headers.Authorization = autenticacaoToken;
            var response = await _httpClient.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Usuario não autenticado");
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception($"Requisição inválida. ");
                }
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new Exception($"Acesso negado.");
                }
                else throw new Exception($"Erro ao fazer a requisição.");
            }

            var projetos = JsonConvert.DeserializeObject<Toggl>(body);
            var tarefas = new List<Datum>();
            foreach (var item in projetos.Data)
            {
                tarefas.Add(item);
            }
            
            return tarefas;


        }
    }
}

