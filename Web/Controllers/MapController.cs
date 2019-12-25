using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class MapController : BaseController
    {
        [HttpGet("map/{id?}/", Name = "Map")]
        public async Task<IActionResult> Map(string id)
        {
            if (id == "dotnetweb")
            {
                return View("DotNetWeb");
            }


            return View();
        }
    }
}
