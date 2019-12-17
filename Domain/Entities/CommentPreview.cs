using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CommentPreview : Entity
    {
        public string Text { get; set; }

        public DateTime Created { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserPhotoUrl { get; set; }

        public string PostId { get; set; }

        public string PostTitle { get; set; }
        public string PostUrl { get; set; }

        public string CommentId { get; set; }
    }
}
