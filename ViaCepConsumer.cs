using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
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
        private HttpClientHandler _clientHandler;

        private HttpClient GetHttpClient() => _clientHandler == null ? GetHttpClient() : new HttpClient(_clientHandler);

        public ViaCepConsumer() { }
        public ViaCepConsumer(string proxyHost, int proxyPort, string proxyUser, string proxyPassword)
        {
            _clientHandler = new HttpClientHandler
            {
                Proxy = new WebProxy(proxyHost, proxyPort)
                {
                    Credentials = new NetworkCredential(proxyUser, proxyPassword)
                }
            };
        }

        public SearchResult Search(string zipCode) => SearchAsync(zipCode, CancellationToken.None).GetAwaiter().GetResult();

        public async Task<SearchResult> SearchAsync(string zipCode, CancellationToken token)
        {
            using (var client = GetHttpClient())
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
            using (var client = GetHttpClient())
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
