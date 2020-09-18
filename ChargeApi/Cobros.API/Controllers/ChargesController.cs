using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charges.API.swagger;
using Charges.Business.Dtos;
using Charges.Business.Exceptions;
using Cobros.API.Factories;
using Microsoft.AspNetCore.Internal;
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

        public ChargesController(ActionFactory actionFactory) {
            this.actionFactory = actionFactory;
        }

        [HttpPost]
        public async Task<ActionResult<ChargeResponse>> Post(Charges.Business.Dtos.Charge charge) {
            ChargeResponse result = await actionFactory
                .CreateAddChargeAction()
                .Execute(charge);
            if (result is ChargeAlreadyExist) {
                return BadRequest(new ChargeResponseKO() { Message = "Identifier already exist" }); ;

            }
             return Ok(result);            
        }

        [HttpDelete]
        [Route("charge/{identifier}")]
        public async Task<ActionResult> Delete(string identifier) {
            try {
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
            catch(ChargesException ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
