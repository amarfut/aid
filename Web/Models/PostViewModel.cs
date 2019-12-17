using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PostMapViewModel
    {
        public List<PostPreviewDto> Programming { get; set; } = new List<PostPreviewDto>();

        public List<PostPreviewDto> Career { get; set; } = new List<PostPreviewDto>();
    }
}
