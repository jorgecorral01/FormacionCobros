//using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpApiClient {
    public class clsHttpApliClient : IHttpApiClient {
        private HttpClient httpClient;

        public clsHttpApliClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri) {
            try {
                return await httpClient.DeleteAsync(requestUri);
            }
            catch {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri) {            
            try {
                return await httpClient.GetAsync(requestUri);
            }
            catch (Exception ex) {                
                return CreateResponse(ex.Message);   
            }
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
        private static HttpResponseMessage CreateResponse(string error) {
            error = "{\"error\":\"" + error + "\"}";
            HttpContent content = new StringContent(error, Encoding.UTF8, "application/json");
            HttpResponseMessage returnThis = new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest, Content = content };
            return returnThis;
        }
    }
}