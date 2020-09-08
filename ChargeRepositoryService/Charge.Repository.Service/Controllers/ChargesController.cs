using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charge.Repository.Service.Business.Dtos;
using Charge.Repository.Service.swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace Charge.Repository.Service.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChargesController : ControllerBase {
        public static void Convention(ApiVersioningOptions options) {
            options.Conventions.Controller<ChargesController>().HasApiVersions(ApiVersioning.Versions());
        }
        // POST 
        [Route("add")]
        [HttpPost]
        public ActionResult Add(RepositoryCharge charge) {
            return Ok();
        }
    }
}
