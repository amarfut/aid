using Microsoft.AspNetCore.Mvc;
using Services.AppServices;
using Services.DTOs;
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
            var postService = new PostService();

            if (id == "dotnetweb")
            {
                var post = await postService.GetPost("dotnetweb");
                return View("DotNetWeb", post);
            }
            else if (id == "dotnetweb_en")
            {
                var post = await postService.GetPost("dotnetweb");
                return View("DotNetWebEnglish", post);
            }
            //else if (id == "js")
            //{
            //    var post = await postService.GetPost("dotnetweb");
            //    return View("JavaScript", post);
            //}



            return View();
        }
    }
}
