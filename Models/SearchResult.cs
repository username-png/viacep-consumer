using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViaCepConsumer.Models
{
    public class SearchResult
    {
        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("logradouro")]
        public string Rua { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("localidade")]
        public string Cidade { get; set; }

        [JsonProperty("uf")]
        public string EstadoUf { get; set; }

        [JsonProperty("unidade")]
        public string Unidade { get; set; }

        [JsonProperty("ibge")]
        public int CodigoIbge { get; set; }

        [JsonProperty("gia")]
        public int Gia { get; set; }
    }
}
