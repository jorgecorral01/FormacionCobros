﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cobros.Business.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChargesController : ControllerBase {

        // POST api/charges
        [HttpPost]
        public ActionResult Post( Charge charge) {
            return Ok();
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
