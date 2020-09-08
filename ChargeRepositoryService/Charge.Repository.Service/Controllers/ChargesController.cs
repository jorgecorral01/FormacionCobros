using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charge.Repository.Service.Business.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Charge.Repository.Service.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChargesController : ControllerBase {

        // POST 
        [Route("add")]
        [HttpPost]
        public ActionResult Add(RepositoryCharge charge) {
            return Ok();
        }
    }
}
