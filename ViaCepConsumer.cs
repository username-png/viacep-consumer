using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViaCepConsumer.Models;

namespace ViaCepConsumer
{
    public class ViaCepConsumer
    {
        private const string BaseUrl = "https://viacep.com.br";

        public SearchResult Search(string zipCode) => SearchAsync(zipCode, CancellationToken.None).GetAwaiter().GetResult();

        public async Task<SearchResult> SearchAsync(string zipCode, CancellationToken token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var response       = await client.GetAsync($"/ws/{zipCode}/json", token).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var @return        = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<SearchResult>(@return);
            }
        }

        public IEnumerable<SearchResult> Search(
            string estado,
            string cidade,
            string endereco) 
            => SearchAsync(estado, cidade, endereco, CancellationToken.None).GetAwaiter().GetResult();

        public async Task<IEnumerable<SearchResult>> SearchAsync(
            string estado,
            string cidade,
            string endereco,
            CancellationToken token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var response       = await client.GetAsync($"/ws/{estado}/{cidade}/{endereco}/json", token).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var @return        = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<SearchResult>>(@return);
            }
        }
    }
}
