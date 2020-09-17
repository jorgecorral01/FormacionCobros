using Charges.Action;
using Cobros.API.Factories;
using NSubstitute;
using System;

namespace Charges.Controllers.Test.mocks {
    public class ActionsFactoryMock {
        public static ActionFactory Instance { get; }

        static ActionsFactoryMock() {
            Instance = Substitute.For<ActionFactory>();
        }

        public static void CreateAddChargeAction(AddChargeAction action) {
            Instance.CreateAddChargeAction().Returns(action);
        }

        internal static void CreateDeleteChargeAction(DeleteChargeAction action) {
            Instance.CreateDeleteChargeAction().Returns(action); 
        }
    }
}
