using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Views.Shared
{
    public class CommentFormViewModel
    {
        public CommentFormViewModel(string postId, string parentCommentId)
        {
            PostId = postId;
            ParentCommentId = parentCommentId;
        }

        public string ParentCommentId { get; set; }

        public string PostId { get; set; }
    }
}
