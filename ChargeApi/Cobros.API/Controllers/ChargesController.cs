using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charges.Business.Dtos;
using Cobros.API.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChargesController : ControllerBase {

        private readonly ActionFactory actionFactory;

        public ChargesController (ActionFactory actionFactory) {
            this.actionFactory = actionFactory;
        }

        // POST api/charges
        [HttpPost]
        public async Task<ActionResult> Post(Charge charge) {
            bool v = await actionFactory.CreateAddChargeAction().Execute(charge);
            if ( v) {
                return Ok();
            }
            throw new Exception("For TODO");
        }







        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) {
            return "value";
        }        

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
