using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Views.Shared
{
    public class CommentDetailsViewModel
    {
        public CommentDetailsViewModel(CommentDto dto, bool displayUserName = false)
        {
            CommentDto = dto;
            DisplayUserName = displayUserName;
        }

        public CommentDto CommentDto { get; set; }

        public bool DisplayUserName { get; set; }
    }
}
