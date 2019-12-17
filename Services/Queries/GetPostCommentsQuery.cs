using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries
{
    public class GetPostCommentsQuery : IQuery<List<Comment>>
    {
        public string PostId { get; set; }

        public GetPostCommentsQuery(string postId) => PostId = postId;
    }
}
