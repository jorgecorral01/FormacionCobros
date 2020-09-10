using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Charge.Activity.Service.Bussines.Dtos;
using Charge.Activity.Service.Factories;
using Charge.Activity.Service.swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace Charge.Activity.Service.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public partial class ChargeActivityController : ControllerBase {
        private readonly RepositoriesFactory repositoriesFactory;

        public static void Convention(ApiVersioningOptions options) {
            options.Conventions.Controller<ChargeActivityController>().HasApiVersions(ApiVersioning.Versions());
        }

        public ChargeActivityController(RepositoriesFactory repositoriesFactory) {
            this.repositoriesFactory = repositoriesFactory;
        }
        [Route("add")]
        [HttpPost]
        public ActionResult add(IdentifierDto identifier) {
            if(repositoriesFactory.GetRespository().Add(identifier.identifier)) { 
                return Ok();
            }
            throw new Exception("TODO");
        }

        // PUT api/<ValuesController>/5
        [Route("update")]
        [HttpPut]
        public ActionResult Put(IdentifierDto identifier) {
            if(ActionFactory.GetUpdateActivityAction(repositoriesFactory).Execute(identifier)) {             
                return Ok();
            }
            throw new Exception("TODO");            
        }
    }
}
