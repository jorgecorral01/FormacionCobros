using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charges.API.swagger;
using Charges.Business.Dtos;
using Cobros.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace Cobros.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChargesController : ControllerBase {

        private readonly ActionFactory actionFactory;

        public static void Convention(ApiVersioningOptions options) {
            options.Conventions.Controller<ChargesController>().HasApiVersions(ApiVersioning.Versions());
        }

        public ChargesController (ActionFactory actionFactory) {
            this.actionFactory = actionFactory;
        }

        // POST api/charges
        [HttpPost]
        public async Task<ActionResult> Post(Charges.Business.Dtos.Charge charge) {
            bool v = await actionFactory.CreateAddChargeAction().Execute(charge);
            if ( v) {
                return Ok();
            }
            throw new Exception("For TODO");
        }
    }
}
