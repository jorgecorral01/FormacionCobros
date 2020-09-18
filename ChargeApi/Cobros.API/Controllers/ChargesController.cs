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
            bool result = await actionFactory
                .CreateAddChargeAction()
                .Execute(charge);            
            if( result) {
                return Ok();
            }
            //return BadRequest("dddddd");
            return BadRequest(new BadRequestError("1", "2"));
        }

        [HttpDelete]
        [Route("charge/{identifier}")]
        public async Task<ActionResult> Delete(string identifier) {
            bool result = await actionFactory
                .CreateDeleteChargeAction()
                .Execute(identifier);
            if(result) {
                return Ok();
            }
            else {
                return NotFound();
            }
            
        }

        private class BadRequestError {
            private string v1;
            private string v2;

            public BadRequestError(string v1, string v2) {
                this.v1 = v1;
                this.v2 = v2;
            }
        }
    }
}
