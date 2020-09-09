using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Charge.Activity.Service.Client {
    public partial class ChargeActivityServiceClient {
        private HttpClient httpClient;

        public ChargeActivityServiceClient() { }

        public ChargeActivityServiceClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public virtual async Task<bool> NotifyNewCharge(string identifier) {
            string requestUri = "http://localhost:10002/api/ChargeActivity/add";
            var content = GivenAHttpContent(identifier, requestUri);
            var result = await httpClient.PostAsync(requestUri, content);
            if(result.StatusCode == HttpStatusCode.OK) return true;
            return false;
        }

        private static HttpContent GivenAHttpContent(string identifier, string requestUri) {
            var identifierDto = new IdentifierDto { identifier = identifier };
            string json = JsonConvert.SerializeObject(identifierDto, Formatting.Indented);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = content
            };
            return content;
        }
    }
}
