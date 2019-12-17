using Services;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Views.Components.TopMenu
{
    public class TopMenuViewModel
    {
        public IEnumerable<PostPreviewDto> CareerPosts { get; set; }
        public IEnumerable<PostPreviewDto> ProgrammingPosts { get; set; }
        public IEnumerable<PostPreviewDto> JuniorPosts { get; set; }
    }
}
