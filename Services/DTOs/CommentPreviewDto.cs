using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class CommentPreviewDto
    {
        public string CommentId { get; set; }

        public string ParentCommentId { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string PostTitle { get; set; }

        public string PostUrl { get; set; }

        public string UserPhotoUrl { get; set; }

        public DateTime Created { get; set; }

        public string CreatedTimeRelative { get; set; }

        public int AnswersCount { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
