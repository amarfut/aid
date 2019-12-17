using Domain.Entities;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries
{
    public class GetLikedPostsQuery : IQuery<List<PostPreviewDto>>
    {
        public string UserId { get; set; }

        public GetLikedPostsQuery(string userId) => UserId = userId;
    }
}
