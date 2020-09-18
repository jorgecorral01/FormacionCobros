using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace HttpApiClient {
    public interface IHttpApiClient {
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent httpContent);
        Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content);
        Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }
}
