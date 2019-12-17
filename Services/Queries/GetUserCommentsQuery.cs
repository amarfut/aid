using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries
{
    public class GetUserCommentsQuery : IQuery<List<CommentPreviewDto>>
    {
        public GetUserCommentsQuery(string userId) => UserId = userId;

        public string UserId { get; set; } 
    }
}
