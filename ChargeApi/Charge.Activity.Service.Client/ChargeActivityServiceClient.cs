using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Charge.Activity.Service.Client {
    public class ChargeActivityServiceClient {
        private HttpClient httpClient;

        public ChargeActivityServiceClient() { }

        public ChargeActivityServiceClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public virtual bool NotifyNewCharge(string identifier) {
            var TODO = true; // TODO
            return TODO;
        }
    }
}
