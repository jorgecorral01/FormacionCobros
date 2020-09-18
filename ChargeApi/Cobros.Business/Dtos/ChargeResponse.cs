namespace Charges.Business.Dtos {
    public class ChargeResponse {
        public string Message { get; set; }
    }
    public class ChargeResponseOK: ChargeResponse {
    }

    public class ChargeResponseKO : ChargeResponse {
    }

    public class ChargeAlreadyExist : ChargeResponse { }
}