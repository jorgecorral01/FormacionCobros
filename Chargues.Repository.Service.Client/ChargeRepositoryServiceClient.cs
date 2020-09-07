using Cobros.Business.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chargues.Repository.Service.Client {
    public class ChargeRepositoryServiceClient {
        private HttpClient client;

        public ChargeRepositoryServiceClient(HttpClient client) {
            this.client = client;
        }

        public virtual async Task<bool> AddCharge(Charge newCharge) {
            string requestUri = "http://localhost:10001/api/charges/add";
            var content = GivenAHttpContent(newCharge, requestUri);
            var aa = true;
            var bbb = await client.PostAsync(requestUri, content);
            var ccc = bbb.Content;
            return  aa ;
        }

        private static HttpContent GivenAHttpContent(Charge charge2, string requestUri) {
            string jsonVideo = JsonConvert.SerializeObject(charge2, Formatting.Indented);
            HttpContent content = new StringContent(jsonVideo, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = content
            };
            return content;
        }
    }
}
