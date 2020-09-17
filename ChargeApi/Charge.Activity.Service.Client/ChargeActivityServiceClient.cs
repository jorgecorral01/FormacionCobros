using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Charge.Activity.Service.Client {
    public partial class ChargeActivityServiceClient {
        private HttpClient httpClient;
        private string server = "172.17.0.3:30761";
               
        public ChargeActivityServiceClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public virtual async Task<bool> NotifyNewCharge(ActivityDto identifier) {
            string requestUri = string.Format("http://{0}/api/ChargeActivity/add", server);
            var content = GivenAHttpContent(identifier, requestUri);
            var result = await httpClient.PostAsync(requestUri, content);
            if(result.StatusCode == HttpStatusCode.OK) return true;
            return false;
        }
        
        public virtual async Task<bool> UpdateNotifyCharge(ActivityDto identifierDto) {            
            string requestUri = string.Format("http://{0}/api/ChargeActivity/update", server);
            var content = GivenAHttpContent(identifierDto, requestUri);                        
            var result = await httpClient.PutAsync(requestUri, content);
            if(result.StatusCode == HttpStatusCode.OK) return true;
            return false;
        }

        private static HttpContent GivenAHttpContent(ActivityDto identifierDto, string requestUri) {
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
