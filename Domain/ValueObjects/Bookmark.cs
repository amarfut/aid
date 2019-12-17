using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class Bookmark
    {

        public Bookmark(string postId, DateTime created)
        {
            PostId = postId;
            Created = created;
        }

        public string PostId { get; set; }

        public DateTime Created { get; set; }
    }
}
