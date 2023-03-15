using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Contracts
{
    public class TooglResponse
    {
        [JsonProperty("project", NullValueHandling = NullValueHandling.Ignore)]
        public string Project { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        //[JsonProperty("project_ids", NullValueHandling = NullValueHandling.Ignore)] // isso permitiria puxar tb os projetos associados a terefa, podemos remover se achar melhor
       // public string projectsId { get; set; }

        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Start { get; set; }

        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? End { get; set; }
    }
}
