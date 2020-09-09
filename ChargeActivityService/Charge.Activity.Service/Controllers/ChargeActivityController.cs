using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Charge.Activity.Service.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChargeActivityController : ControllerBase {      
        [Route("add")]
        [HttpPost]
        public ActionResult add(string identifier) {
            return Ok();
        }
    }
}
