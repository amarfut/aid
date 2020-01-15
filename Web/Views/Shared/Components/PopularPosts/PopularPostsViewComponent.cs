using Microsoft.AspNetCore.Mvc;
using Services.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Views.Shared.Components.MobileMenu
{
    public class PopularPostsViewComponent : ViewComponent
    {
        public PopularPostsViewComponent() { }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            PostService service = new PostService();
            var posts = await service.GetTopPosts();
            return View("Default", posts);
        }
    }
}
