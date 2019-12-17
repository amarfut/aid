using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Views.Components.TopMenu
{
    public class TopMenuViewComponent : ViewComponent
    {
        public TopMenuViewComponent() { }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var service = new PostService();
            int limit = 5;
            var model = new TopMenuViewModel
            {
                CareerPosts = await service.GetRandomPostPreviews(PostType.Career, limit),
                ProgrammingPosts = await service.GetRandomPostPreviews(PostType.Programming, limit),
                JuniorPosts = await service.GetRandomPostPreviews(PostType.ForJuniors, limit)
            };

            return View("Default", model);
        }
    }
}
