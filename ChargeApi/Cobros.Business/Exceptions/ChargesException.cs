using System;
using System.Runtime.Serialization;

namespace Charges.Business.Exceptions {
    [Serializable]
    public class ChargesException : Exception {
        public ChargesException(string message) : base(message) {}
    }
}
