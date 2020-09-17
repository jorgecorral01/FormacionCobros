using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Charge.Activity.Service.Client {
    public interface HttpApiClient {
        Task<HttpResponseMessage> PostAsync(string v, HttpContent httpContent);
    }
}
