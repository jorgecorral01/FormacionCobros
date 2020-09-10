using Charge.Activity.Service.Action;
using System;

namespace Charge.Activity.Service.Factories {
    public class ActionFactory {
        internal static UpdateActivityAction GetUpdateActivityAction(RepositoriesFactory repositoriesFactory) {
            return new UpdateActivityAction(repositoriesFactory.GetRespository());
        }
    }
}
