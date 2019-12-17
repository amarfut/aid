using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Answer : BaseComment
    {
        public string ParentCommentId { get; set; }
    }
}
