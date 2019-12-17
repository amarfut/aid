using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Views.Shared.Components.MobileMenu
{
    public class MobileMenuViewComponent : ViewComponent
    {
        public MobileMenuViewComponent() { }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View("Default", "");
        }
    }
}
