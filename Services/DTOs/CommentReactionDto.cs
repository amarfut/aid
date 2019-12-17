using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class AnswerCommentDto
    {
        public string Text { get; set; }

        public string ParentCommentId { get; set; }
    }
}
