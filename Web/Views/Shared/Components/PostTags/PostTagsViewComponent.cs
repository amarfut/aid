using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Services.AppServices;
using Services.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Utils;
using MongoDB.Driver;

namespace Web.Views.Shared.Components.MobileMenu
{
    public class TagModel
    {
        public string Name { get; set; }
        public string Display { get; set; }
    }

    public class PostTagsViewComponent : ViewComponent
    {
        public PostTagsViewComponent() { }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DatabaseContext _db = new DatabaseContext();

            var tags = await _db.Tags.Find(x => true).ToListAsync();
            var models = tags.Select(p => new TagModel()
            {
                Display = p.Display,
                Name = p.Name
            }).ToList();

            return View("Default", models);
        }
    }
}
