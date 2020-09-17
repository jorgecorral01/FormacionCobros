//using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpApiClient {
    public class clsHttpApliClient : IHttpApiClient {
        private HttpClient httpClient;

        public clsHttpApliClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent httpContent) {
            try {
                return await httpClient.PostAsync(requestUri, httpContent);
            }
            catch {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent httpContent) {
            try {
                return await httpClient.PutAsync(requestUri, httpContent);
            }
            catch {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}