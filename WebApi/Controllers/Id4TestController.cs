using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class Id4TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //return new JsonResult("the id4 test controller return value");
            return new JsonResult(User.Claims.Select(u => new { u.Type, u.Value }));
        }
    }
}