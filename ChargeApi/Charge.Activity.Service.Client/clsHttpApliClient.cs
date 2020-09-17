using System.Net.Http;
using System.Threading.Tasks;

namespace Charge.Activity.Service.Client {
    public class clsHttpApliClient : HttpApiClient {
        private HttpClient httpClient;        

        public clsHttpApliClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent httpContent) {
            throw new System.NotImplementedException();
        }

        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content) {
            throw new System.NotImplementedException();
        }
    }
}