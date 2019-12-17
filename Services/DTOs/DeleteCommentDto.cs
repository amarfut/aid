using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class DeleteCommentDto
    {
        public bool TopLevel { get; set; }

        public string CommentId { get; set; }

        public string PostUrl { get; set; }
    }
}
