using Charges.Business.Dtos;
using HttpApiClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chargues.Repository.Service.Client {
    public class ChargeRepositoryServiceApiClient {
        private IHttpApiClient client;
        private string server = "http://localhost:10001"; // "172.17.0.3:30209";

        public ChargeRepositoryServiceApiClient(IHttpApiClient client) {
            this.client = client;
        }

        public virtual async Task<bool> AddCharge(Charge newCharge) {            
            string requestUri = string.Format("{0}/api/charges/add", server);
            var content = GivenAHttpContent(newCharge, requestUri);            
            var result = await client.PostAsync(requestUri, content);      
            if (result.StatusCode  == HttpStatusCode.OK) return true;
            throw new Exception("TODO");
        }

        public virtual async Task<bool> DeleteCharge(string identifier) {
            string requestUri = string.Format("{0}/api/charges/addcharge/{1}", server, identifier);
            var result = await client.DeleteAsync(requestUri);
            if(result.StatusCode == HttpStatusCode.OK) return true;
            if(result.StatusCode == HttpStatusCode.NotFound) return false;
            throw new Exception("TODO");
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
